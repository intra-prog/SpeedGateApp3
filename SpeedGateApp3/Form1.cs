using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // El paquete que acabas de instalar

// [FPGA] 1. Librería del puerto serial (Descomentar después)
// using System.IO.Ports; 

namespace SpeedGateApp3
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=bibliotecaetda;Uid=root;Pwd=admin;";
        // [FPGA] private SerialPort fpgaPort;

        // Variables Anti-Doble Escaneo (Imprevisto 1)
        private string ultimaMatriculaEscaneada = "";
        private DateTime tiempoUltimoEscaneo = DateTime.MinValue;

        public Form1()
        {
            InitializeComponent();
            cbMotivo.SelectedIndex = 0;
            txtMatricula.KeyDown += TxtMatricula_KeyDown;

            // (Imprevisto 3): Forzar el Focus siempre a la caja de texto
            this.Activated += (s, e) => { txtMatricula.Focus(); };
            this.Click += (s, e) => { txtMatricula.Focus(); };

            // [FPGA] InicializarPuertoSerial();
        }

        private void Form1_Load(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }

        // [FPGA] Función de conexión... (Comentada)

        private void TxtMatricula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                string matriculaLeida = txtMatricula.Text.Trim();
                txtMatricula.Clear();

                // (Imprevisto 4): Bloquear basura o QRs gigantes
                if (matriculaLeida.Length > 20)
                {
                    MessageBox.Show("Código no válido. Longitud excedida.", "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // (Imprevisto 1): Bloqueo por "Efecto Metralleta" (Enfriamiento de 3 segundos)
                if (matriculaLeida == ultimaMatriculaEscaneada && (DateTime.Now - tiempoUltimoEscaneo).TotalSeconds < 3)
                {
                    return; // Ignora la lectura si pasó muy rápido
                }

                // Guarda el registro en memoria para el cooldown
                ultimaMatriculaEscaneada = matriculaLeida;
                tiempoUltimoEscaneo = DateTime.Now;

                // Si pasa todos los filtros, va a la Base de Datos
                BuscarAlumnoBD(matriculaLeida);
            }
        }

        private void BuscarAlumnoBD(string matricula)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string queryAlumno = @"SELECT a.nombre, a.apellido_paterno, c.nombre_carrera 
                                           FROM alumnos a 
                                           JOIN carreras c ON a.id_carrera = c.id_carrera 
                                           WHERE a.matricula = @mat";

                    using (MySqlCommand cmd = new MySqlCommand(queryAlumno, conn))
                    {
                        cmd.Parameters.AddWithValue("@mat", matricula);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nombreCompleto = reader["nombre"].ToString() + " " + reader["apellido_paterno"].ToString();
                                string carrera = reader["nombre_carrera"].ToString();
                                reader.Close();

                                int ultimoMovimiento = ObtenerUltimoMovimiento(matricula, conn);

                                if (ultimoMovimiento == 1)
                                {
                                    // ADENTRO -> SALE
                                    RegistrarMovimiento(matricula, 2, conn);
                                    MostrarPantallaSalida(nombreCompleto, carrera);
                                }
                                else
                                {
                                    // AFUERA -> ENTRA
                                    RegistrarMovimiento(matricula, 1, conn);
                                    MostrarPantallaEntrada(nombreCompleto, carrera);

                                    // [FPGA] if (fpgaPort != null && fpgaPort.IsOpen) { fpgaPort.Write("1"); }
                                }
                            }
                            else
                            {
                                reader.Close();
                                MostrarVentanaRegistro(matricula);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("XAMPP apagado o error de BD: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtMatricula.Focus(); // Imprevisto 3: Recuperar Focus al terminar
            }
        }

        private int ObtenerUltimoMovimiento(string matricula, MySqlConnection conn)
        {
            // (Imprevisto 2): Se lee la fecha del último movimiento además del motivo
            string query = "SELECT motivo, fecha_hora FROM registros_acceso WHERE matricula = @mat ORDER BY fecha_hora DESC LIMIT 1";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@mat", matricula);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int motivoUltimo = Convert.ToInt32(reader["motivo"]);
                        DateTime fechaHoraUltimo = Convert.ToDateTime(reader["fecha_hora"]);

                        // Si su último movimiento fue "Entrada" pero pasó hace más de 12 horas,
                        // asumimos que el alumno es un "Fantasma" y se fue sin registrar salida.
                        if (motivoUltimo == 1 && (DateTime.Now - fechaHoraUltimo).TotalHours > 12)
                        {
                            return 2; // Lo reportamos como si estuviera afuera
                        }

                        return motivoUltimo;
                    }
                }
            }
            return 2;
        }

        // ... Mismos métodos de RegistrarMovimiento, Pantallas Verde/Azul ...
        private void RegistrarMovimiento(string matricula, int tipoMovimiento, MySqlConnection conn)
        {
            string insert = "INSERT INTO registros_acceso (matricula, motivo) VALUES (@mat, @tipo)";
            using (MySqlCommand cmd = new MySqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@mat", matricula);
                cmd.Parameters.AddWithValue("@tipo", tipoMovimiento);
                cmd.ExecuteNonQuery();
            }
        }

        private void MostrarPantallaEntrada(string nombre, string carrera)
        {
            pnlStatus.BackColor = Color.FromArgb(209, 231, 221);
            lblStatusMsg1.Text = "¡ACCESO CORRECTO - ENTRADA!";
            lblStatusMsg1.ForeColor = Color.FromArgb(15, 81, 50);
            lblStatusMsg2.Text = nombre;
            lblStatusMsg3.Text = carrera;
            lblStatusMsg4.Text = "Hora de entrada: " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void MostrarPantallaSalida(string nombre, string carrera)
        {
            pnlStatus.BackColor = Color.FromArgb(204, 229, 255);
            lblStatusMsg1.Text = "¡SALIDA REGISTRADA, VUELVE PRONTO!";
            lblStatusMsg1.ForeColor = Color.FromArgb(0, 64, 133);
            lblStatusMsg2.Text = nombre;
            lblStatusMsg3.Text = carrera;
            lblStatusMsg4.Text = "Hora de salida: " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void MostrarVentanaRegistro(string matriculaPendiente)
        {
            Form formRegistro = new Form();
            formRegistro.Text = "Primer Registro - Biblioteca";
            formRegistro.Size = new Size(400, 380);
            formRegistro.StartPosition = FormStartPosition.CenterParent;
            formRegistro.FormBorderStyle = FormBorderStyle.FixedDialog;
            formRegistro.MaximizeBox = false;

            Label lblTit = new Label { Text = "Registro Único del Sistema", Font = new Font("Arial", 12, FontStyle.Bold), AutoSize = false, Size = new Size(400, 25), Location = new Point(0, 15), TextAlign = ContentAlignment.MiddleCenter };
            Label lblMat = new Label { Text = "Identificador: " + matriculaPendiente, Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.RoyalBlue, AutoSize = false, Size = new Size(400, 20), Location = new Point(0, 40), TextAlign = ContentAlignment.MiddleCenter };
            Label lblSub = new Label { Text = "Solo tendrás que registrarte esta primera vez.", Font = new Font("Arial", 8), ForeColor = Color.Gray, AutoSize = false, Size = new Size(400, 20), Location = new Point(0, 60), TextAlign = ContentAlignment.MiddleCenter };

            Label lblN1 = new Label { Text = "Nombre(s):", Location = new Point(30, 100), AutoSize = true };
            TextBox txtNom = new TextBox { Location = new Point(150, 98), Size = new Size(200, 20) };
            Label lblN2 = new Label { Text = "Primer Apellido:", Location = new Point(30, 130), AutoSize = true };
            TextBox txtAp1 = new TextBox { Location = new Point(150, 128), Size = new Size(200, 20) };
            Label lblN3 = new Label { Text = "Segundo Apellido:", Location = new Point(30, 160), AutoSize = true };
            TextBox txtAp2 = new TextBox { Location = new Point(150, 158), Size = new Size(200, 20) };
            Label lblN4 = new Label { Text = "Rol / Carrera:", Location = new Point(30, 190), AutoSize = true };

            ComboBox cbCarrera = new ComboBox { Location = new Point(150, 188), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cbCarrera.Items.AddRange(new string[] {
                "LSC - Licenciatura en Sistemas Computacionales",
                "LIDTS - Licenciatura en Ingeniería y Desarrollo de Tecnología de Software",
                "LIS - Licenciatura en Ingeniería en Semiconductores",
                "LIACD - Licenciatura en Inteligencia Artificial y Ciencia de Datos",
                "LASBIAG - Licenciatura en Arquitectura de Sistemas basados en IA Generativa",
                "PROFESOR / DOCENTE"
            });
            cbCarrera.SelectedIndex = 0;

            Button btnGuardar = new Button { Text = "Guardar y Registrar Acceso", Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.White, BackColor = Color.SeaGreen, Size = new Size(220, 35), Location = new Point(90, 260), FlatStyle = FlatStyle.Flat };

            btnGuardar.Click += (s, ev) => {
                if (string.IsNullOrWhiteSpace(txtNom.Text) || string.IsNullOrWhiteSpace(txtAp1.Text))
                {
                    MessageBox.Show("Llena los campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbCarrera.SelectedIndex < 5 && matriculaPendiente.Length != 9)
                {
                    MessageBox.Show("Las matrículas de estudiantes deben tener exactamente 9 dígitos.\n\nSi usted es docente, seleccione la opción 'PROFESOR / DOCENTE'.", "Identificador Inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string insertAlumno = "INSERT INTO alumnos (matricula, nombre, apellido_paterno, apellido_materno, id_carrera) VALUES (@m, @n, @ap, @am, @idc)";
                    using (MySqlCommand cmd = new MySqlCommand(insertAlumno, conn))
                    {
                        cmd.Parameters.AddWithValue("@m", matriculaPendiente);
                        cmd.Parameters.AddWithValue("@n", txtNom.Text.Trim());
                        cmd.Parameters.AddWithValue("@ap", txtAp1.Text.Trim());
                        cmd.Parameters.AddWithValue("@am", txtAp2.Text.TrimX());
                        cmd.Parameters.AddWithValue("@idc", cbCarrera.SelectedIndex + 1);
                        cmd.ExecuteNonQuery();
                    }
                }
                formRegistro.Close();
                BuscarAlumnoBD(matriculaPendiente);
            };

            formRegistro.Controls.AddRange(new Control[] { lblTit, lblMat, lblSub, lblN1, txtNom, lblN2, txtAp1, lblN3, txtAp2, lblN4, cbCarrera, btnGuardar });

            formRegistro.FormClosed += (s, ev) => { txtMatricula.Focus(); }; // Recuperar Focus al cerrar el popup

            formRegistro.ShowDialog();
        }
    }
}
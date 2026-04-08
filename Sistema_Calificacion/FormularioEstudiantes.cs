using iText.Kernel.Pdf;
using iText.Layout.Element;
using Sistema_Calificacion.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace Sistema_Calificacion
{
    public partial class FormularioEstudiantes : Form
    {
        private SistemaCalificacionesDBEntities28 db;
        public FormularioEstudiantes()
        {
            db = new SistemaCalificacionesDBEntities28();
            InitializeComponent();
        }

        private void FormularioEstudiantes_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string txtID = txtInsertEstudianteID.Text.Trim();
            string nombre = txtInsertNombre.Text.Trim();
            string apellido = txtInsertApellido.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1) 
            {
                MessageBox.Show("Llenar el campo EstudianteID con un número positivo");
                txtInsertEstudianteID.Focus();
                return;
            }

            if (db.Estudiantes.Any(E => E.EstudianteID == id))
            {
                MessageBox.Show($"Ya Existe un estudiante registrado con ID: {id}");
                txtInsertEstudianteID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Llena el campo Nombre");
                txtInsertNombre.Focus();
                return;
            }

            if (nombre.Length < 1 || nombre.Length > 100) 
            {
                MessageBox.Show("El campo Nombre deber tener entre 1 hasta 100 caracteres");
                txtInsertNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("Llena el campo Apellido");
                txtInsertApellido.Focus();
                return;
            }

            if (apellido.Length < 1 || apellido.Length > 100)
            {
                MessageBox.Show("El campo Apellido deber tener entre 1 hasta 100 caracteres");
                txtInsertApellido.Focus();
                return;
            }

            try
            {
                var estudiante = new Estudiante()
                {
                    EstudianteID = id,
                    Nombre = nombre,
                    Apellido = apellido
                };
                db.Estudiantes.Add(estudiante);
                db.SaveChanges();

                MessageBox.Show($"Éxito al insertar estudiante con ID: {id}");

                cargarDatos();

                txtInsertEstudianteID.Clear();
                txtInsertNombre.Clear();
                txtInsertApellido.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar estudiante " + ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string txtID = txtElimEstudianteID.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id))
            {
                MessageBox.Show("Llenar el campo EstudianteID con un número positivo");
                txtElimEstudianteID.Focus();
                return;
            }


            try
            {
                var estudiante = db.Estudiantes.Find(id);

                if (estudiante == null) 
                {
                    MessageBox.Show($"No Existe estudiante registrado con ID {id}");
                    txtElimEstudianteID.Focus();
                    return;
                }

                if (db.Calificaciones.Any(c => c.EstudianteID == id))
                {
                    MessageBox.Show("No puedes eliminar este estudiante porque tiene calificaciones registradas");
                    return;
                }

                db.Estudiantes.Remove(estudiante);

                db.SaveChanges();

                MessageBox.Show($"Éxito al eliminar estudiante con ID: {id}");

                cargarDatos();

                txtElimEstudianteID.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al eliminar estudiante " + ex.Message);
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string txtID = txtActEstudianteID.Text.Trim();
            string nombre = txtActNombre.Text.Trim();
            string apellido = txtActApellido.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1) 
            {
                MessageBox.Show("Llenar el campo EstudianteID con un numero positivo");
                txtActEstudianteID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre)) 
            {
                MessageBox.Show("Llenar el campo nombre");
                txtActNombre.Focus();
                return;
            }

            if (nombre.Length < 1 || nombre.Length > 100)
            {
                MessageBox.Show("El campo Nombre deber tener entre 1 hasta 100 caracteres");
                txtActNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido)) 
            {
                MessageBox.Show("Llenar el campo apellido");
                txtActApellido.Focus();
                return;
            }

            if (apellido.Length < 1 || apellido.Length > 100)
            {
                MessageBox.Show("El campo Apellido deber tener entre 1 hasta 100 caracteres");
                txtActApellido.Focus();
                return;
            }

            try
            {
                var estudiante = db.Estudiantes.Find(id);

                if (estudiante == null)
                {
                    MessageBox.Show($"No existe estudiante registrado con ID: {id}");
                    return;
                }

                estudiante.Nombre = nombre;
                estudiante.Apellido = apellido;

                db.SaveChanges();

                MessageBox.Show($"Éxito al actualizar el estudiante registrado con ID: {id}");
                
                cargarDatos();

                txtActEstudianteID.Clear();
                txtActNombre.Clear();
                txtActApellido.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al actualizar estudiante " + ex.Message);
            }

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            var estudiantes = db.Estudiantes.OrderBy(es => es.EstudianteID).ToList();


            using (SaveFileDialog cuadroGuardar = new SaveFileDialog())
            {
                cuadroGuardar.Filter = "CSV (*.csv)|*.csv|PDF (*.pdf)|*.pdf";
                cuadroGuardar.FileName = "Estudiantes";
               
                if (cuadroGuardar.ShowDialog() != DialogResult.OK) return;
                
                try
                {
                    if (cuadroGuardar.FilterIndex == 1)
                    {
                        ExportarCSV(cuadroGuardar.FileName, estudiantes);
                    }
                    else
                    {
                        ExportarPDF(cuadroGuardar.FileName, estudiantes);
                    }
                    MessageBox.Show("Éxito al exportar estudiantes registrados");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ExportarCSV(string rutaArchivo, List<Estudiante> estudiantes)
        {
            var contenidoCSV = new StringBuilder();

            contenidoCSV.AppendLine("ID,Nombre,Apellido");

            foreach (var estudiante in estudiantes)
            {
                string nombre = estudiante.Nombre.Replace("\"", "\"\"");
                string apellido = estudiante.Apellido.Replace("\"", "\"\"");
                contenidoCSV.AppendLine($"{estudiante.EstudianteID},\"{nombre}\",\"{apellido}\"");
            }

            File.WriteAllText(rutaArchivo, contenidoCSV.ToString(), new UTF8Encoding(true));
        }

        private void ExportarPDF(string rutaArchivo, List<Estudiante> estudiantes)
        {
            using (var writer = new PdfWriter(rutaArchivo))
            using (var documentoPDF = new PdfDocument(writer))
            using (var documento = new Document(documentoPDF))
            {
           
                var tabla = new Table(3);
               
                tabla.AddHeaderCell("ID");
                tabla.AddHeaderCell("Nombre");
                tabla.AddHeaderCell("Apellido");
               
                foreach (var es in estudiantes)
                {
                    tabla.AddCell(es.EstudianteID.ToString());
                    tabla.AddCell(es.Nombre);
                    tabla.AddCell(es.Apellido);
                }
               
                documento.Add(tabla);   
            }
        }

        private void cargarDatos()
        {
            var Estudiantes = db.Estudiantes.OrderBy( e => e.EstudianteID).Select( e => new 
            {
                ID = e.EstudianteID,
                Nombre = e.Nombre,
                Apellido = e.Apellido
            }).ToList();

            tablaContenido.DataSource = Estudiantes;

            tablaContenido.Columns["ID"].Width = 100;
            tablaContenido.Columns["Nombre"].Width = 254;
            tablaContenido.Columns["Apellido"].Width = 254;
        }
    }
}

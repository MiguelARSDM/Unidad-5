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

            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("Llena el campo Apellido");
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

                MessageBox.Show($"Exito al insertar estudiante con ID: {id}");

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

                db.Estudiantes.Remove(estudiante);

                db.SaveChanges();

                MessageBox.Show($"Exito al eliminar estudiante con ID: {id}");

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

            if (string.IsNullOrWhiteSpace(apellido)) 
            {
                MessageBox.Show("Llenar el campo apellido");
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
                cargarDatos();

                txtActEstudianteID.Clear();
                txtActNombre.Clear();
                txtActApellido.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al actualizar " + ex.Message);
            }

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            var estudiantes = db.Estudiantes.OrderBy(es => es.EstudianteID)
         .Select(es => new 
         { 
             ID = es.EstudianteID, Nombre = es.Nombre, Apellido = es.Apellido 
         })
         .ToList();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV (*.csv)|*.csv|PDF (*.pdf)|*.pdf";
                sfd.FileName = "Estudiantes";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    if (sfd.FilterIndex == 1)
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine("EstudianteID,Nombre,Apellido");
                        foreach (var es in estudiantes)
                            sb.AppendLine($"{es.ID},{es.Nombre},{es.Apellido}");
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    }
                    else
                    {
                        using (var writer = new PdfWriter(sfd.FileName))
                        using (var pdf = new PdfDocument(writer))
                        {
                            var doc = new Document(pdf);
                            var tabla = new Table(3);
                            tabla.AddHeaderCell("EstudianteID");
                            tabla.AddHeaderCell("Nombre");
                            tabla.AddHeaderCell("Apellido");
                            foreach (var es in estudiantes)
                            {
                                tabla.AddCell(es.ID.ToString());
                                tabla.AddCell(es.Nombre);
                                tabla.AddCell(es.Apellido);
                            }
                            doc.Add(tabla);
                            doc.Close();
                        }
                    }
                    MessageBox.Show("Exportado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
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

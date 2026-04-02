using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Calificacion.Modelo;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace Sistema_Calificacion
{
    public partial class FormularioMaterias : Form
    {
        private SistemaCalificacionesDBEntities28 db;
        public FormularioMaterias()
        {
            db = new SistemaCalificacionesDBEntities28();
            InitializeComponent();
        }

        private void FormularioMaterias_Load(object sender, EventArgs e)
        {
            cargarDatos();        
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string txtID = txtInsertMateriaID.Text.Trim();
            string nombre = txtInsertNombre.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1) 
            {
                MessageBox.Show("Llenar el campo MateriaID con un número positivo");
                txtInsertMateriaID.Focus();
                return;
            }

            if (db.Materias.Any(m => m.MateriaID == id)) 
            {
                MessageBox.Show($"Ya existe una materia registrada con el ID: {id}");
                txtInsertMateriaID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Llenar el campo Nombre");
                txtInsertNombre.Focus();
                return;
            }

            if (nombre.Length < 1 || nombre.Length > 100)
            {
                MessageBox.Show("El campo Nombre deber tener entre 1 hasta 100 caracteres");
                txtInsertNombre.Focus();
                return;
            }

            try
            {
                var materia = new Materia()
                {
                    MateriaID = id,
                    Nombre = nombre
                };

                db.Materias.Add(materia);
                db.SaveChanges();

                MessageBox.Show($"Exito al insertar la materia con ID: {id}");

                cargarDatos();

                txtInsertMateriaID.Clear();
                txtInsertNombre.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar materia " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string txtID = txtElimMateriaID.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1)
            {
                MessageBox.Show("Llenar el campo ID con un número positivo");
                txtElimMateriaID.Focus();
                return;
            }

            try
            {
                var materia = db.Materias.Find(id);

                if (materia == null)
                {
                    MessageBox.Show($"No hay materia registrada con el ID: {id}");
                    txtElimMateriaID.Focus();
                    return;
                }

                /*var confirmacion = MessageBox.Show($"Deseas Eliminar La Materia {materia.Nombre}?","Confirmar",MessageBoxButtons.YesNo);

                if (confirmacion == DialogResult.No) 
                {
                    return;
                }*/

                db.Materias.Remove(materia);
                db.SaveChanges();

                MessageBox.Show($"Exito al eliminar la materia registrada con el ID: {id}");

                cargarDatos();
                
                txtElimMateriaID.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al eliminar Materia " + ex.Message);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string txtID = txtActMateriaID.Text.Trim();
            string nombre = txtActNombre.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1)
            {
                MessageBox.Show("Llenar el campo MateriaID con un número positivo");
                txtActMateriaID.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Llenar el campo Nombre");
                txtActNombre.Focus();
                return;
            }

            if (nombre.Length < 1 || nombre.Length > 100)
            {
                MessageBox.Show("El campo Nombre deber tener entre 1 hasta 100 caracteres");
                txtActNombre.Focus();
                return;
            }

            try
            {
                var materia = db.Materias.Find(id);

                if ( materia == null)
                {
                    MessageBox.Show($"No existe materia registrada con ID: {id}");
                    txtActMateriaID.Focus();
                    return;
                }

                materia.Nombre = nombre;

                MessageBox.Show($"Exito al actualizar materia registrada con ID: {id}");

                db.SaveChanges();

                cargarDatos();

                txtActMateriaID.Clear();
                txtActNombre.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar materia " + ex.Message );
            }


        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            var materias = db.Materias.OrderBy(m => m.MateriaID)
        .Select(m => new { ID = m.MateriaID, Nombre = m.Nombre })
        .ToList();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV (*.csv)|*.csv|PDF (*.pdf)|*.pdf";
                sfd.FileName = "Materias";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    if (sfd.FilterIndex == 1)
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine("MateriaID,Nombre");
                        foreach (var m in materias)
                            sb.AppendLine($"{m.ID},{m.Nombre}");
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    }
                    else
                    {
                        using (var writer = new PdfWriter(sfd.FileName))
                        using (var pdf = new PdfDocument(writer))
                        {
                            var doc = new Document(pdf);
                            var tabla = new Table(2);
                            tabla.AddHeaderCell("MateriaID");
                            tabla.AddHeaderCell("Nombre");
                            foreach (var m in materias)
                            {
                                tabla.AddCell(m.ID.ToString());
                                tabla.AddCell(m.Nombre);
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
            var Materias = db.Materias.OrderBy(m => m.MateriaID)
                .Select( m => new 
                {
                    ID = m.MateriaID,
                    Nombre = m.Nombre            
                }).ToList();

            tablaContenido.DataSource = Materias;

            tablaContenido.Columns["ID"].Width = 100;
            tablaContenido.Columns["Nombre"].Width = 520;
        }

    }
}

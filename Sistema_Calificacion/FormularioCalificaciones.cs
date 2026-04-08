using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Sistema_Calificacion.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Sistema_Calificacion
{
    public partial class FormularioCalificaciones : Form
    {
        private SistemaCalificacionesDBEntities28 db;
        public FormularioCalificaciones()
        {
            db = new SistemaCalificacionesDBEntities28();
            InitializeComponent();
        }

        private void FormularioCalificaciones_Load(object sender, EventArgs e)
        {
            cargarDatos();
            cargarEstudiantes();
            cargarMaterias();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string txtCalificacionID = txtInsertCalificacionID.Text.Trim();
            string calificacion1 = txtInsertCalificacion1.Text.Trim();
            string calificacion2 = txtInsertCalificacion2.Text.Trim();
            string calificacion3 = txtInsertCalificacion3.Text.Trim();
            string calificacion4 = txtInsertCalificacion4.Text.Trim();
            string Examen = txtInsertExamen.Text.Trim();



            if (string.IsNullOrEmpty(txtCalificacionID) || !int.TryParse(txtCalificacionID, out int calID) || calID < 1)
            {
                MessageBox.Show("Llenar el campo CalificacionID con un numero positivo");
                txtInsertCalificacionID.Focus();
                return;
            }

            if (db.Calificaciones.Any(c => c.CalificacionID == calID))
            {
                MessageBox.Show($"Ya hay una calificacion regitrada con el ID: {calID}");
                txtInsertCalificacionID.Focus();
                return;
            }

            if (cmbInsertEstudianteID.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un estudiante registrado");
                return;
            }
            int estID = Convert.ToInt32(cmbInsertEstudianteID.SelectedValue);

            if (cmbInsertMateriaID.SelectedValue == null)
            {
                MessageBox.Show("Selecciona una materia registrada");
                return;
            }
            int matID = Convert.ToInt32(cmbInsertMateriaID.SelectedValue);

            if (db.Calificaciones.Any(c => c.EstudianteID == estID && c.MateriaID == matID))
                     
            {
                MessageBox.Show($"Ya {db.Estudiantes.Find(estID).Nombre} {db.Estudiantes.Find(estID).Apellido} tiene otra calificación de la materia {db.Materias.Find(matID).Nombre}");
                return;
            }

            if (string.IsNullOrEmpty(calificacion1) || !decimal.TryParse(calificacion1, out decimal cal1) || cal1 < 0 || cal1 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 1 con un numero positivo entre 0 hasta 100");
                txtInsertCalificacion1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(calificacion2) || !decimal.TryParse(calificacion2, out decimal cal2) || cal2 < 0 || cal2 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 2 con un numero positivo entre 0 hasta 100");
                txtInsertCalificacion2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(calificacion3) || !decimal.TryParse(calificacion3, out decimal cal3) || cal3 < 0 || cal3 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 3 con un numero positivo entre 0 hasta 100");
                txtInsertCalificacion3.Focus();
                return;
            }

            if (string.IsNullOrEmpty(calificacion4) || !decimal.TryParse(calificacion4, out decimal cal4) || cal4 < 0 || cal4 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 4 con un numero positivo entre 0 hasta 100");
                txtInsertCalificacion4.Focus();
                return;
            }

            if (string.IsNullOrEmpty(Examen) || !decimal.TryParse(Examen, out decimal exam) || exam < 0 || exam > 100)
            {
                MessageBox.Show("Llenar el campo Examen con un numero positivo entre 0 hasta 100");
                txtInsertExamen.Focus();
                return;
            }

            try
            {
                decimal totalC = CalcularTotal(cal1, cal2, cal3, cal4, exam);

                var calificaciones = new Calificacione()
                {
                    CalificacionID = calID,
                    EstudianteID = estID,
                    MateriaID = matID,
                    Calificacion1 = cal1,
                    Calificacion2 = cal2,
                    Calificacion3 = cal3,
                    Calificacion4 = cal4,
                    Examen = exam,
                    Total = totalC,
                    Clasificacion = CalcularClasificacion(totalC),
                    Estado = CalcularEstado(totalC),
                };
                db.Calificaciones.Add(calificaciones);
                db.SaveChanges();

                MessageBox.Show($"Éxito al insertar la calificación con ID: {calID}");

                cargarDatos();

                txtInsertCalificacionID.Clear();
                txtInsertCalificacion1.Clear();
                txtInsertCalificacion2.Clear();
                txtInsertCalificacion3.Clear();
                txtInsertCalificacion4.Clear();
                txtInsertExamen.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al insertar calificación: " + ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string txtID = txtElimCalificacionID.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1)
            {
                MessageBox.Show("Llenar el campo CalificacionID con un número positivo");
                txtElimCalificacionID.Focus();
                return;
            }

            try
            {
                var califacion = db.Calificaciones.Find(id);

                if (califacion == null)
                {
                    MessageBox.Show($"No Existe una calificación registrada con ID {id}");
                    txtElimCalificacionID.Focus();
                    return;
                }

                db.Calificaciones.Remove(califacion);
                db.SaveChanges();

                MessageBox.Show($"Éxito al eliminar calificación registrada con ID: {id}");

                cargarDatos();

                txtElimCalificacionID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar calificación: " + ex.Message);
                return;
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string txtCalificacionID = txtActCalificacionID.Text.Trim();
            string calificacion1 = txtActCalificacion1.Text.Trim();
            string calificacion2 = txtActCalificacion2.Text.Trim();
            string calificacion3 = txtActCalificacion3.Text.Trim();
            string calificacion4 = txtActCalificacion4.Text.Trim();
            string Examen = txtActExamen.Text.Trim();

            if (string.IsNullOrEmpty(txtCalificacionID) || !int.TryParse(txtCalificacionID, out int calID) || calID < 1)
            {
                MessageBox.Show("Llenar el campo CalificacionID con un número positivo");
                return;
            } 

            if (cmbActEstudianteID.SelectedValue == null)
            {
                MessageBox.Show("Seleccciona un estudiante registrado");
                return;
            }
            int estID = Convert.ToInt32(cmbActEstudianteID.SelectedValue);

            if (cmbActMateriaID.SelectedValue == null)
            {
                MessageBox.Show("Selecciona una materia registrada");
                return;
            }
            int matID = Convert.ToInt32(cmbActMateriaID.SelectedValue);

            if (db.Calificaciones.Any(c => c.EstudianteID == estID && c.MateriaID == matID && c.CalificacionID != calID))
            {
                MessageBox.Show($"Ya {db.Estudiantes.Find(estID).Nombre} {db.Estudiantes.Find(estID).Apellido} tiene otra calificación de la materia {db.Materias.Find(matID).Nombre}");
                return;
            }

            if (string.IsNullOrEmpty(calificacion1) || !decimal.TryParse(calificacion1, out decimal cal1) || cal1 < 0 || cal1 > 100)
            {
                MessageBox.Show("Llenar el campo Calificación 1 con un número positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion2) || !decimal.TryParse(calificacion2, out decimal cal2) || cal2 < 0 || cal2 > 100)
            {
                MessageBox.Show("Llenar el campo Calificación 2 con un número positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion3) || !decimal.TryParse(calificacion3, out decimal cal3) || cal3 < 0 || cal3 > 100)
            {
                MessageBox.Show("Llenar el campo Calificación 3 con un número positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion4) || !decimal.TryParse(calificacion4, out decimal cal4) || cal4 < 0 || cal4 > 100)
            {
                MessageBox.Show("Llenar el campo Calificación 4 con un número positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(Examen) || !decimal.TryParse(Examen, out decimal exam) || exam < 0 || exam > 100)
            {
                MessageBox.Show("Llenar el campo Examen con un número positivo entre 0 hasta 100");
                return;
            }

            try
            {
                var calificaciones = db.Calificaciones.Find(calID);

                if (calificaciones == null)
                {
                    MessageBox.Show($"No existe calificación registrada con ID: {calID}");
                    return;
                }

                calificaciones.EstudianteID = estID;
                calificaciones.MateriaID = matID;

                calificaciones.Calificacion1 = cal1;
                calificaciones.Calificacion2 = cal2;
                calificaciones.Calificacion3 = cal3;
                calificaciones.Calificacion4 = cal4;
                calificaciones.Examen = exam;
                
                
                decimal totalC = CalcularTotal(cal1, cal2, cal3, cal4, exam);

                calificaciones.Total = totalC;
                calificaciones.Clasificacion = CalcularClasificacion(totalC);
                calificaciones.Estado = CalcularEstado(totalC);

                db.SaveChanges();

                MessageBox.Show($"Éxito al actualizar la calificación registrada con ID: {calID}");
                
                cargarDatos();

                txtActCalificacionID.Clear();
                txtActCalificacion1.Clear();
                txtActCalificacion2.Clear();
                txtActCalificacion3.Clear();
                txtActCalificacion4.Clear();
                txtActExamen.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar calificación: " + ex.Message);
            }

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            var calificaciones = db.Calificaciones.OrderBy(c => c.CalificacionID).ToList();

            using (SaveFileDialog cuadroGuardar = new SaveFileDialog())
            {
                cuadroGuardar.Filter = "CSV (*.csv)|*.csv|PDF (*.pdf)|*.pdf";
                cuadroGuardar.FileName = "Calificaciones";
               
                if (cuadroGuardar.ShowDialog() != DialogResult.OK) return;

                try
                {
                    if (cuadroGuardar.FilterIndex == 1)
                    {
                        ExportarCSV(cuadroGuardar.FileName, calificaciones);
                    }
                    else
                    {
                        ExportarPDF(cuadroGuardar.FileName, calificaciones);
                    }
                    MessageBox.Show("Éxito al exportar calificaciones registradas");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar calificaciones: " + ex.Message);
                }
            }
        }

        private void ExportarCSV(string rutaArchivo, List<Calificacione> calificaciones)
        {
            var contenidoCSV = new StringBuilder();

            contenidoCSV.AppendLine("CalificaciónID,Estudiante,Materia,Calificación 1,Calificación 2,Calificación 3,Calificación 4,Examen,Total,Clasificación,Estado");

            foreach (var c in calificaciones)
            {
                string estudiante = (c.Estudiante.Nombre + " " + c.Estudiante.Apellido).Replace("\"", "\"\"");
                string materia = c.Materia.Nombre.Replace("\"", "\"\"");
                string clasificacion = c.Clasificacion.Replace("\"", "\"\"");
                string estado = c.Estado.Replace("\"", "\"\"");

                contenidoCSV.AppendLine(
                    $"{c.CalificacionID},\"{estudiante}\",\"{materia}\"," +
                    $"{c.Calificacion1},{c.Calificacion2},{c.Calificacion3},{c.Calificacion4}," +
                    $"{c.Examen},{c.Total},\"{clasificacion}\",\"{estado}\""
                );
            }
            File.WriteAllText(rutaArchivo, contenidoCSV.ToString(), new UTF8Encoding(true));
        }

        private void ExportarPDF(string rutaArchivo, List<Calificacione> calificaciones)
        {

            using (var writerPDF = new PdfWriter(rutaArchivo))
            using (var documentoPDF = new PdfDocument(writerPDF))
            using (var documento = new Document(documentoPDF))
            {
                documentoPDF.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A4.Rotate());

                var tabla = new Table(11);
                tabla.SetFontSize(8);

                tabla.AddHeaderCell("ID");
                tabla.AddHeaderCell("Estudiante");
                tabla.AddHeaderCell("Materia");
                tabla.AddHeaderCell("Calificación 1");
                tabla.AddHeaderCell("Calificación 2");
                tabla.AddHeaderCell("Calificación 3");
                tabla.AddHeaderCell("Calificación 4");
                tabla.AddHeaderCell("Examen");
                tabla.AddHeaderCell("Total");
                tabla.AddHeaderCell("Clasificación.");
                tabla.AddHeaderCell("Estado");

                foreach (var calificacion in calificaciones)
                {
                    tabla.AddCell(calificacion.CalificacionID.ToString());
                    tabla.AddCell(calificacion.Estudiante.Nombre + " " + calificacion.Estudiante.Apellido);
                    tabla.AddCell(calificacion.Materia.Nombre);
                    tabla.AddCell(calificacion.Calificacion1.ToString());
                    tabla.AddCell(calificacion.Calificacion2.ToString());
                    tabla.AddCell(calificacion.Calificacion3.ToString());
                    tabla.AddCell(calificacion.Calificacion4.ToString());
                    tabla.AddCell(calificacion.Examen.ToString());
                    tabla.AddCell(calificacion.Total.ToString());
                    tabla.AddCell(calificacion.Clasificacion);
                    tabla.AddCell(calificacion.Estado);
                }

                documento.Add(tabla);
            }
        }

        private void cargarDatos()
        {
            var Calificaciones = db.Calificaciones.OrderBy(c => c.CalificacionID).Select(c => new
            {
                CalificacionID = c.CalificacionID,
                EstudianteID = c.Estudiante.Nombre + " " + c.Estudiante.Apellido,
                MateriaID = c.Materia.Nombre,
                Calificacion1 = c.Calificacion1,
                Calificacion2 = c.Calificacion2,
                Calificacion3 = c.Calificacion3,
                Calificacion4 = c.Calificacion4,
                Examen = c.Examen,
                Total = c.Total,
                Clasificación = c.Clasificacion,
                Estado = c.Estado
            }).ToList();

            tablaContenido.DataSource = Calificaciones;

            tablaContenido.Columns["CalificacionID"].Width = 80;
            tablaContenido.Columns["EstudianteID"].Width = 180;
            tablaContenido.Columns["MateriaID"].Width = 150;
            tablaContenido.Columns["Calificacion1"].Width = 74;
            tablaContenido.Columns["Calificacion2"].Width = 74;
            tablaContenido.Columns["Calificacion3"].Width = 74;
            tablaContenido.Columns["Calificacion4"].Width = 74;
            tablaContenido.Columns["Examen"].Width = 52;
            tablaContenido.Columns["Total"].Width = 52;
            tablaContenido.Columns["Clasificación"].Width = 70;
            tablaContenido.Columns["Estado"].Width = 70;
        }

        private void cargarEstudiantes() 
        {
            var estudiantes = db.Estudiantes.OrderBy(e => e.EstudianteID).Select(e => new
            {
                ID = e.EstudianteID,
                NombreCompleto = e.Nombre + " " + e.Apellido

            }).ToList();

            cmbInsertEstudianteID.DataSource = estudiantes;
            cmbInsertEstudianteID.DisplayMember = "NombreCompleto";
            cmbInsertEstudianteID.ValueMember = "ID";
            cmbInsertEstudianteID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbInsertEstudianteID.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbActEstudianteID.DataSource = estudiantes;
            cmbActEstudianteID.DisplayMember = "NombreCompleto";
            cmbActEstudianteID.ValueMember = "ID";
            cmbActEstudianteID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbActEstudianteID.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cargarMaterias()
        {
            var materias = db.Materias.OrderBy(m => m.MateriaID).Select(m => new
            {
                ID = m.MateriaID,
                Nombre = m.Nombre
            }).ToList();

            cmbInsertMateriaID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbInsertMateriaID.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbInsertMateriaID.DataSource = materias;
            cmbInsertMateriaID.DisplayMember = "Nombre";
            cmbInsertMateriaID.ValueMember = "ID";
            

            cmbActMateriaID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbActMateriaID.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbActMateriaID.DataSource = materias;
            cmbActMateriaID.DisplayMember = "Nombre";
            cmbActMateriaID.ValueMember = "ID";
            
        }


        private decimal CalcularTotal(decimal cal1, decimal cal2, decimal cal3, decimal cal4, decimal exam)
        {
            decimal totalCal = (cal1 + cal2 + cal3 + cal4) / 4; 
            totalCal = totalCal * 0.70m;

            decimal totalExam = exam * 0.30m;

            return Math.Round(totalCal + totalExam, 2);
        }
        private string CalcularClasificacion(decimal total)
        {
            if (total >= 90)
               return "A";
            if (total >= 80)
                return "B";
            if (total >= 70)
                return "C";

            return "F";
        }
        private string CalcularEstado(decimal total) 
        {
            return total >= 70 ? "Aprobado" : "Reprobado";
        }

    }
}

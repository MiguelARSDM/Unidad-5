using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            string txtEstudianteID = cmbInsertEstudianteID.SelectedValue.ToString();
            string txtMateriaID = cmbInsertMateriaID.SelectedValue.ToString();
            string calificacion1 = txtInsertCalificacion1.Text.Trim();
            string calificacion2 = txtInsertCalificacion2.Text.Trim();
            string calificacion3 = txtInsertCalificacion3.Text.Trim();
            string calificacion4 = txtInsertCalificacion4.Text.Trim();
            string Examen = txtInsertExamen.Text.Trim();

            

            if (string.IsNullOrEmpty(txtCalificacionID) || !int.TryParse(txtCalificacionID, out int calID) || calID < 0 || calID > 100)
            {
                MessageBox.Show("Llenar el campo CalificacionID con un numero positivo");
                return;
            }

            if (db.Calificaciones.Any(c => c.CalificacionID == calID)) 
            {
                MessageBox.Show($"Ya hay una calificacion regitrada con el ID: {calID}");
                return;
            }

            if (string.IsNullOrEmpty(txtEstudianteID) || !int.TryParse(txtEstudianteID, out int estID) || estID < 0 || estID > 100)
            {
                MessageBox.Show("Llenar el campo EstudianteID con un numero positivo");
                return;
            }

            if (string.IsNullOrEmpty(txtMateriaID) || !int.TryParse(txtMateriaID, out int matID) || matID < 0 || matID > 100)
            {
                MessageBox.Show("Llenar el campo CalificacionID con un numero positivo");
                return;
            }

            if(db.Calificaciones.Any(c => c.EstudianteID == estID && c.MateriaID == matID)
               )      
            {
                MessageBox.Show($"Ya {db.Estudiantes.Find(estID).Nombre} {db.Estudiantes.Find(estID).Apellido} tiene la calificaciones de la materia {db.Materias.Find(matID).Nombre}");
                return;
            }

            if (string.IsNullOrEmpty(calificacion1) || !int.TryParse(calificacion1, out int cal1) || cal1 < 0 || cal1 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 1 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion2) || !int.TryParse(calificacion2, out int cal2) || cal2 < 0 || cal2 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 2 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion3) || !int.TryParse(calificacion3, out int cal3) || cal3 < 0 || cal3 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 3 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion4) || !int.TryParse(calificacion4, out int cal4) || cal4 < 0 || cal4 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 4 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(Examen) || !int.TryParse(Examen, out int exam) || exam < 0 || exam > 100)
            {
                MessageBox.Show("Llenar el campo Examen con un numero positivo entre 0 hasta 100");
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
                cargarDatos();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al insertar calificacion " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string txtID = txtElimCalificacionID.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1)
            {
                MessageBox.Show($"No Hay Calicacion Con ese ID");
                return;
            }

            try
            {
                var califaciones = db.Calificaciones.Find(id);

                db.Calificaciones.Remove(califaciones);
                db.SaveChanges();
                cargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar");
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

            string txtEstudianteID = cmbInsertEstudianteID.SelectedValue.ToString();
            string txtMateriaID = cmbInsertMateriaID.SelectedValue.ToString();


            if (string.IsNullOrEmpty(txtCalificacionID) || !int.TryParse(txtCalificacionID, out int calID) || calID < 0 || calID > 100)
            {
                MessageBox.Show("Llenar el campo CalificacionID con un numero positivo");
                return;
            } 

            if (string.IsNullOrEmpty(txtEstudianteID) || !int.TryParse(txtEstudianteID, out int estID) || estID < 0 || estID > 100)
            {
                MessageBox.Show("Llenar el campo EstudianteID con un numero positivo");
                return;
            }

            if (string.IsNullOrEmpty(txtMateriaID) || !int.TryParse(txtMateriaID, out int matID) || matID < 0 || matID > 100)
            {
                MessageBox.Show("Llenar el campo CalificacionID con un numero positivo");
                return;
            }

            if (string.IsNullOrEmpty(calificacion1) || !int.TryParse(calificacion1, out int cal1) || cal1 < 0 || cal1 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 1 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion2) || !int.TryParse(calificacion2, out int cal2) || cal2 < 0 || cal2 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 2 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion3) || !int.TryParse(calificacion3, out int cal3) || cal3 < 0 || cal3 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 3 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(calificacion4) || !int.TryParse(calificacion4, out int cal4) || cal4 < 0 || cal4 > 100)
            {
                MessageBox.Show("Llenar el campo Calificacion 4 con un numero positivo entre 0 hasta 100");
                return;
            }

            if (string.IsNullOrEmpty(Examen) || !int.TryParse(Examen, out int exam) || exam < 0 || exam > 100)
            {
                MessageBox.Show("Llenar el campo Examen con un numero positivo entre 0 hasta 100");
                return;
            }

            try
            {
                var calificaciones = db.Calificaciones.Find(calID);

                if (calificaciones == null)
                {
                    MessageBox.Show("No Existe");
                    return;
                }

                calificaciones.Calificacion1 = cal1;
                calificaciones.Calificacion2 = cal2;
                calificaciones.Calificacion3 = cal3;
                calificaciones.Calificacion4 = cal4;
                calificaciones.Examen = exam;

                
                decimal totalC = CalcularTotal(cal1, cal2, cal3, cal4, exam);

                calificaciones.Total = totalC;
                calificaciones.Clasificacion = CalcularClasificacion(totalC);
                calificaciones.Estado = CalcularEstado(totalC);

                MessageBox.Show("Exito");
                db.SaveChanges();
                cargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }



        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            var calificaciones = db.Calificaciones.OrderBy(c => c.CalificacionID).Select(c => new
            {
                c.CalificacionID,
                Estudiante = c.Estudiante.Nombre + " " + c.Estudiante.Apellido,
                Materia = c.Materia.Nombre,
                c.Calificacion1,
                c.Calificacion2,
                c.Calificacion3,
                c.Calificacion4,
                c.Examen,
                c.Total,
                c.Clasificacion,
                c.Estado
            }).ToList();

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV (*.csv)|*.csv|PDF (*.pdf)|*.pdf";
                sfd.FileName = "Calificaciones";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    if (sfd.FilterIndex == 1)
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine("CalificaciónID, Estudiante, Materia, Calificación 1, Calificacion 2, Calificación 3,Calificación 4, Examen, Total, Clasificación, Estado");
                        foreach (var c in calificaciones)
                            sb.AppendLine($"{c.CalificacionID}, {c.Estudiante}, {c.Materia}, {c.Calificacion1}, {c.Calificacion2}, {c.Calificacion3}, dedcx6e{c.Calificacion4},{c.Examen},{c.Total},{c.Clasificacion},{c.Estado}");
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    }
                    else
                    {
                        using (var writer = new PdfWriter(sfd.FileName))
                        using (var pdf = new PdfDocument(writer))
                        {
                            pdf.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A4.Rotate());
                            var doc = new Document(pdf);
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
                            tabla.AddHeaderCell("Clasif.");
                            tabla.AddHeaderCell("Estado");
                            foreach (var c in calificaciones)
                            {
                                tabla.AddCell(c.CalificacionID.ToString());
                                tabla.AddCell(c.Estudiante);
                                tabla.AddCell(c.Materia);
                                tabla.AddCell(c.Calificacion1.ToString());
                                tabla.AddCell(c.Calificacion2.ToString());
                                tabla.AddCell(c.Calificacion3.ToString());
                                tabla.AddCell(c.Calificacion4.ToString());
                                tabla.AddCell(c.Examen.ToString());
                                tabla.AddCell(c.Total.ToString());
                                tabla.AddCell(c.Clasificacion);
                                tabla.AddCell(c.Estado);
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
                Clasificacion = c.Clasificacion,
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
            tablaContenido.Columns["Clasificacion"].Width = 70;
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

            cmbInsertMateriaID.DataSource = materias;
            cmbInsertMateriaID.DisplayMember = "Nombre";
            cmbInsertMateriaID.ValueMember = "ID";
            cmbActMateriaID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbActMateriaID.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbActMateriaID.DataSource = materias;
            cmbActMateriaID.DisplayMember = "Nombre";
            cmbActMateriaID.ValueMember = "ID";
            cmbActMateriaID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbActMateriaID.AutoCompleteSource = AutoCompleteSource.ListItems;
        }


        private decimal CalcularTotal(decimal cal1, decimal cal2, decimal cal3, decimal cal4, decimal exam)
        {
            decimal totalCal = (cal1 + cal2 + cal3 + cal4) / 4; 
            totalCal = totalCal * 0.70m;

            decimal totalExam = exam * 0.30m;

            return totalCal + totalExam;
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

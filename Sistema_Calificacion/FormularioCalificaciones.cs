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

            string txtEstudianteID = cmbInsertEstudianteID.SelectedValue.ToString();
            string txtMateriaID = cmbInsertMateriaID.SelectedValue.ToString();

            //Calcular Total Calificaciones

            decimal cal1 = Convert.ToDecimal(calificacion1);
            decimal cal2 = Convert.ToDecimal(calificacion2);
            decimal cal3 = Convert.ToDecimal(calificacion3);
            decimal cal4 = Convert.ToDecimal(calificacion4);
            decimal exam = Convert.ToDecimal(Examen);

            decimal totalCal = (cal1 + cal2 + cal3 + cal4) / 4;
            totalCal = totalCal * 0.70m;
            decimal totalExam = exam * 0.30m;

            decimal total = totalCal + totalExam;

            //Calcular Calificacion A,B,C,F

            char calificacion = 'F';
            if(total >= 90) 
            {
                calificacion = 'A';
            }
            else if (total >= 80 & total < 90)
            {
                calificacion = 'B';
            }
            else if (total >= 70 & total < 80)
            {
                calificacion = 'C';
            }
            else if (total > 0 & total < 69)
            {
                calificacion = 'F';
            }

            //Calcular Estado 

            string estado = "Reprobado";
            if (total >= 70) 
            {
                estado = "Aprobado";
            }
            else
            {
                estado = "Reprobado";
            }

            var calificaciones = new Calificacione()
            {
                CalificacionID = Convert.ToInt32(txtCalificacionID),
                EstudianteID = Convert.ToInt32(txtEstudianteID),
                MateriaID = Convert.ToInt32(txtMateriaID),
                Calificacion1 = Convert.ToDecimal(calificacion1),
                Calificacion2 = Convert.ToDecimal(calificacion2),
                Calificacion3 = Convert.ToDecimal(calificacion3),
                Calificacion4 = Convert.ToDecimal(calificacion4),
                Examen = Convert.ToDecimal(Examen),
                Total = total,
                Clasificacion = Convert.ToString(calificacion),
                Estado = estado,
            };
            db.Calificaciones.Add(calificaciones);
            db.SaveChanges();
            cargarDatos();

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
                MessageBox.Show("");
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

            if (string.IsNullOrEmpty(txtCalificacionID) || !int.TryParse(txtCalificacionID, out int id) || id < 1)
            {
                MessageBox.Show("EL Id debe ser un numero positivo");
                return;
            }

            try
            {
                var calificaciones = db.Calificaciones.Find(id);

                if (calificaciones == null)
                {
                    MessageBox.Show("No Existe");
                    return;
                }

                calificaciones.Calificacion1 = Convert.ToDecimal(calificacion1);
                calificaciones.Calificacion2 = Convert.ToDecimal(calificacion2);
                calificaciones.Calificacion3 = Convert.ToDecimal(calificacion3);
                calificaciones.Calificacion4 = Convert.ToDecimal(calificacion4);
                calificaciones.Examen = Convert.ToDecimal(Examen);

                string txtEstudianteID = cmbInsertEstudianteID.SelectedValue.ToString();
                string txtMateriaID = cmbInsertMateriaID.SelectedValue.ToString();

                //Calcular Total Calificaciones

                decimal cal1 = Convert.ToDecimal(calificacion1);
                decimal cal2 = Convert.ToDecimal(calificacion2);
                decimal cal3 = Convert.ToDecimal(calificacion3);
                decimal cal4 = Convert.ToDecimal(calificacion4);
                decimal exam = Convert.ToDecimal(Examen);

                decimal totalCal = (cal1 + cal2 + cal3 + cal4) / 4;
                totalCal = totalCal * 0.70m;
                decimal totalExam = exam * 0.30m;

                decimal total = totalCal + totalExam;

                //Calcular Calificacion A,B,C,F

                char calificacion = 'F';
                if (total >= 90)
                {
                    calificacion = 'A';
                }
                else if (total >= 80 & total < 90)
                {
                    calificacion = 'B';
                }
                else if (total >= 70 & total < 80)
                {
                    calificacion = 'C';
                }
                else if (total > 0 & total < 69)
                {
                    calificacion = 'F';
                }

                //Calcular Estado 

                string estado = "Reprobado";
                if (total >= 70)
                {
                    estado = "Aprobado";
                }
                else
                {
                    estado = "Reprobado";
                }

                calificaciones.Total = total;
                calificaciones.Clasificacion = Convert.ToString(calificacion);
                calificaciones.Estado = estado;

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

        }

        private void cargarDatos()
        {
            var Calificaciones = db.Calificaciones.OrderBy( c => c.CalificacionID).Select( c => new 
            {
                CalificacionID =c.CalificacionID,
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

      
    }
}

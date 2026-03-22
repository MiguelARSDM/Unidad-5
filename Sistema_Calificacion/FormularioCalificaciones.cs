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

namespace Sistema_Calificacion
{
    public partial class FormularioCalificaciones : Form
    {
        private SistemaCalificacionesDBEntities1 db;
        public FormularioCalificaciones()
        {
            db = new SistemaCalificacionesDBEntities1();
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

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }

        private void cargarDatos()
        {
            var Calificaciones = db.Calificaciones.OrderBy( c => c.CalificacionID).Select( c => new 
            {
                CalificacionID =c.CalificacionID,
                EstudianteID = c.EstudianteID,
                MateriaID = c.MateriaID,
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

            tablaContenido.Columns["CalificacionID"].Width = 84;
            tablaContenido.Columns["EstudianteID"].Width = 84;
            tablaContenido.Columns["MateriaID"].Width = 68;
            tablaContenido.Columns["Calificacion1"].Width = 82;
            tablaContenido.Columns["Calificacion2"].Width = 82;
            tablaContenido.Columns["Calificacion3"].Width = 82;
            tablaContenido.Columns["Calificacion4"].Width = 82;
            tablaContenido.Columns["Examen"].Width = 60;
            tablaContenido.Columns["Total"].Width = 60;
            tablaContenido.Columns["Clasificacion"].Width = 84;
            tablaContenido.Columns["Estado"].Width = 80;
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

            cmbActEstudianteID.DataSource = estudiantes;
            cmbActEstudianteID.DisplayMember = "NombreCompleto";
            cmbActEstudianteID.ValueMember = "ID";
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

            cmbActMateriaID.DataSource = materias;
            cmbActMateriaID.DisplayMember = "Nombre";
            cmbActMateriaID.ValueMember = "ID";
        }

    }
}

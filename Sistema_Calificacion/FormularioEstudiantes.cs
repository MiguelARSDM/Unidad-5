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

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
    public partial class FormularioMaterias : Form
    {
        private SistemaCalificacionesDBEntities1 db;
        public FormularioMaterias()
        {
            db = new SistemaCalificacionesDBEntities1();
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

        private void cargarDatos() //

        {
            //Crear la lista de materias
            var Materias = db.Materias.OrderBy(m => m.MateriaID)
                .Select( m => new 
                {
                    ID = m.MateriaID,
                    Nombre = m.Nombre
                    
                }).ToList();
            //Llenar la tabla con el contenido
            tablaContenido.DataSource = Materias;

            //Mejor Formato de la tablas
            tablaContenido.Columns["ID"].Width = 100;
            tablaContenido.Columns["Nombre"].Width = 520;
        }

    }
}

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
        private SistemaCalificacionesDBEntities1 db;
        public FormularioEstudiantes()
        {
            db = new SistemaCalificacionesDBEntities1();
            InitializeComponent();
        }
    }
}

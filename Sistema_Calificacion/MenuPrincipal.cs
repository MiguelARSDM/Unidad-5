using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Sistema_Calificacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void CalificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioCalificaciones form = new FormularioCalificaciones();
            form.MdiParent = this;
            form.Show();
        }

        private void EstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioEstudiantes form = new FormularioEstudiantes();
            form.MdiParent = this;
            form.Show();
        }

        private void MateriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioMaterias form = new FormularioMaterias();
            form.MdiParent = this;
            form.Show();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}

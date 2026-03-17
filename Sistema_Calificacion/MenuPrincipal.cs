using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Calificacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void estudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioCalificaciones form = new FormularioCalificaciones();
            form.MdiParent = this;
            form.Show();
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioEstudiantes form = new FormularioEstudiantes();
            form.MdiParent = this;
            form.Show();
        }

        private void materiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormularioMaterias form = new FormularioMaterias();
            form.MdiParent = this;
            form.Show();
        }
    }
}

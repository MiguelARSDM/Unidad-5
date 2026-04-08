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

        private void calificacionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new FormularioCalificaciones());
        }

        private void EstudiantesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new FormularioEstudiantes());
        }

        private void MateriasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new FormularioMaterias());
        }

        private void SalirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CerrarFormulariosAbiertos()
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }
        }

        private void AbrirFormulario(Form form)
        {
            CerrarFormulariosAbiertos();
            
            panelBienvenida.Visible = false;

            form.MdiParent = this;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(0, 0);
            form.Show();
        }

    }
}

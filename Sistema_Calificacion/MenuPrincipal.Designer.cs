namespace Sistema_Calificacion
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.calificaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CalificacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EstudiantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MateriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SalirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calificaciónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1596, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // calificaciónToolStripMenuItem
            // 
            this.calificaciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CalificacionesToolStripMenuItem,
            this.EstudiantesToolStripMenuItem,
            this.MateriasToolStripMenuItem,
            this.SalirToolStripMenuItem});
            this.calificaciónToolStripMenuItem.Name = "calificaciónToolStripMenuItem";
            this.calificaciónToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.calificaciónToolStripMenuItem.Text = "Opciones";
            // 
            // CalificacionesToolStripMenuItem
            // 
            this.CalificacionesToolStripMenuItem.Name = "CalificacionesToolStripMenuItem";
            this.CalificacionesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.CalificacionesToolStripMenuItem.Text = "📝Calificaciones";
            this.CalificacionesToolStripMenuItem.Click += new System.EventHandler(this.CalificacionesToolStripMenuItem_Click);
            // 
            // EstudiantesToolStripMenuItem
            // 
            this.EstudiantesToolStripMenuItem.Name = "EstudiantesToolStripMenuItem";
            this.EstudiantesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.EstudiantesToolStripMenuItem.Text = "🧑‍🎓Estudiantes";
            this.EstudiantesToolStripMenuItem.Click += new System.EventHandler(this.EstudiantesToolStripMenuItem_Click);
            // 
            // MateriasToolStripMenuItem
            // 
            this.MateriasToolStripMenuItem.Name = "MateriasToolStripMenuItem";
            this.MateriasToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.MateriasToolStripMenuItem.Text = "📖Materia";
            this.MateriasToolStripMenuItem.Click += new System.EventHandler(this.MateriasToolStripMenuItem_Click);
            // 
            // SalirToolStripMenuItem
            // 
            this.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem";
            this.SalirToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.SalirToolStripMenuItem.Text = "❌Salir";
            this.SalirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1596, 732);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuPrincipal";
            this.Text = "Gestión de Calificaciones";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem calificaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CalificacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EstudiantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MateriasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SalirToolStripMenuItem;
    }
}


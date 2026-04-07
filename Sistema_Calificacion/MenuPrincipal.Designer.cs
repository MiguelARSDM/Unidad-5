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
            this.calificacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EstudiantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MateriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SalirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBienvenida = new System.Windows.Forms.Panel();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.lbSubtitulo = new System.Windows.Forms.Label();
            this.lbNombre = new System.Windows.Forms.Label();
            this.lbBienvenido = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.lbMatricula = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panelBienvenida.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calificacionesToolStripMenuItem,
            this.EstudiantesToolStripMenuItem,
            this.MateriasToolStripMenuItem,
            this.SalirToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1596, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // calificacionesToolStripMenuItem
            // 
            this.calificacionesToolStripMenuItem.Name = "calificacionesToolStripMenuItem";
            this.calificacionesToolStripMenuItem.Size = new System.Drawing.Size(135, 24);
            this.calificacionesToolStripMenuItem.Text = "📝Calificaciones";
            this.calificacionesToolStripMenuItem.Click += new System.EventHandler(this.calificacionesToolStripMenuItem_Click_1);
            // 
            // EstudiantesToolStripMenuItem
            // 
            this.EstudiantesToolStripMenuItem.Name = "EstudiantesToolStripMenuItem";
            this.EstudiantesToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.EstudiantesToolStripMenuItem.Text = "🧑‍🎓Estudiantes";
            this.EstudiantesToolStripMenuItem.Click += new System.EventHandler(this.EstudiantesToolStripMenuItem_Click_1);
            // 
            // MateriasToolStripMenuItem
            // 
            this.MateriasToolStripMenuItem.Name = "MateriasToolStripMenuItem";
            this.MateriasToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.MateriasToolStripMenuItem.Text = "📖Materia";
            this.MateriasToolStripMenuItem.Click += new System.EventHandler(this.MateriasToolStripMenuItem_Click_1);
            // 
            // SalirToolStripMenuItem1
            // 
            this.SalirToolStripMenuItem1.Name = "SalirToolStripMenuItem1";
            this.SalirToolStripMenuItem1.Size = new System.Drawing.Size(73, 24);
            this.SalirToolStripMenuItem1.Text = "❌Salir";
            this.SalirToolStripMenuItem1.Click += new System.EventHandler(this.SalirToolStripMenuItem1_Click);
            // 
            // panelBienvenida
            // 
            this.panelBienvenida.AutoSize = true;
            this.panelBienvenida.BackColor = System.Drawing.SystemColors.Menu;
            this.panelBienvenida.Controls.Add(this.lbMatricula);
            this.panelBienvenida.Controls.Add(this.Logo);
            this.panelBienvenida.Controls.Add(this.lbBienvenido);
            this.panelBienvenida.Controls.Add(this.lbNombre);
            this.panelBienvenida.Controls.Add(this.lbSubtitulo);
            this.panelBienvenida.Controls.Add(this.lbTitulo);
            this.panelBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBienvenida.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelBienvenida.Location = new System.Drawing.Point(0, 28);
            this.panelBienvenida.Name = "panelBienvenida";
            this.panelBienvenida.Size = new System.Drawing.Size(1596, 704);
            this.panelBienvenida.TabIndex = 2;
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(487, 333);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(634, 50);
            this.lbTitulo.TabIndex = 0;
            this.lbTitulo.Text = "Sistema de Gestión de Calificaciones";
            // 
            // lbSubtitulo
            // 
            this.lbSubtitulo.AutoSize = true;
            this.lbSubtitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubtitulo.Location = new System.Drawing.Point(577, 494);
            this.lbSubtitulo.Name = "lbSubtitulo";
            this.lbSubtitulo.Size = new System.Drawing.Size(447, 28);
            this.lbSubtitulo.TabIndex = 1;
            this.lbSubtitulo.Text = "Selecciona una opción del menú para comenzar";
            // 
            // lbNombre
            // 
            this.lbNombre.AutoSize = true;
            this.lbNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombre.Location = new System.Drawing.Point(674, 572);
            this.lbNombre.Name = "lbNombre";
            this.lbNombre.Size = new System.Drawing.Size(292, 28);
            this.lbNombre.TabIndex = 2;
            this.lbNombre.Text = "Miguel Angel Ramirez Sanchez";
            // 
            // lbBienvenido
            // 
            this.lbBienvenido.AutoSize = true;
            this.lbBienvenido.Font = new System.Drawing.Font("Segoe UI", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBienvenido.Location = new System.Drawing.Point(682, 408);
            this.lbBienvenido.Name = "lbBienvenido";
            this.lbBienvenido.Size = new System.Drawing.Size(272, 62);
            this.lbBienvenido.TabIndex = 3;
            this.lbBienvenido.Text = "Bienvenido";
            // 
            // Logo
            // 
            this.Logo.BackgroundImage = global::Sistema_Calificacion.Properties.Resources.image1;
            this.Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Logo.Location = new System.Drawing.Point(582, 30);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(442, 281);
            this.Logo.TabIndex = 4;
            this.Logo.TabStop = false;
            // 
            // lbMatricula
            // 
            this.lbMatricula.AutoSize = true;
            this.lbMatricula.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMatricula.Location = new System.Drawing.Point(754, 632);
            this.lbMatricula.Name = "lbMatricula";
            this.lbMatricula.Size = new System.Drawing.Size(128, 23);
            this.lbMatricula.TabIndex = 5;
            this.lbMatricula.Text = "MT-2024-00210";
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1596, 732);
            this.Controls.Add(this.panelBienvenida);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuPrincipal";
            this.Text = "Gestión de Calificaciones";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelBienvenida.ResumeLayout(false);
            this.panelBienvenida.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem calificacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EstudiantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MateriasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SalirToolStripMenuItem1;
        private System.Windows.Forms.Panel panelBienvenida;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbSubtitulo;
        private System.Windows.Forms.Label lbBienvenido;
        private System.Windows.Forms.Label lbNombre;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label lbMatricula;
    }
}


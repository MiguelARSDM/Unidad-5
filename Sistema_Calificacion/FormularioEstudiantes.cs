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

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1) 
            {
                MessageBox.Show("El ID Deber Ser Un Numero Positivo");
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Llena El Campo Nombre");
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido))
            {
                MessageBox.Show("Llena El Campo Apellido");
                return;
            }

            if (db.Estudiantes.Any( E => E.EstudianteID == id)) 
            {
                MessageBox.Show($"Ya Existe Un Estudiante con el {id}");
                return;
            }

            try
            {
                var estudiante = new Estudiante()
                {
                    EstudianteID = Convert.ToInt32(txtID),
                    Nombre = nombre,
                    Apellido = apellido
                };
                db.Estudiantes.Add(estudiante);
                db.SaveChanges();

                MessageBox.Show("Exito Al Insertar Estudiante");

                cargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Al Insertar Estudiante");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string txtID = txtElimEstudianteID.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id))
            {
                MessageBox.Show("El ID debe ser un Numero Positivo");
                return;
            }   
            

            try
            {
                var estudiante = db.Estudiantes.Find(id);

                if (estudiante == null) 
                {
                    MessageBox.Show($"No Existe Estudiante Con ID {id}");
                    return;
                }

                db.Estudiantes.Remove(estudiante);

                db.SaveChanges();

                cargarDatos();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("");
            }


        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string txtID = txtActEstudianteID.Text.Trim();
            string nombre = txtActNombre.Text.Trim();
            string apellido = txtActApellido.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1) 
            {
                MessageBox.Show("El ID debe ser un numero Positiov");
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre)) 
            {
                MessageBox.Show("Llenar el campo nombre");
                return;
            }

            if (string.IsNullOrWhiteSpace(apellido)) 
            {
                MessageBox.Show("Llenar el campo apellido");
                return;
            }

         

            try
            {
                var estudiante = db.Estudiantes.Find(id);

                if (estudiante == null) 
                {
                    MessageBox.Show($"No Existe Estudiante con ID {id}");
                    return;
                }

                estudiante.Nombre = nombre;
                estudiante.Apellido = apellido;

                db.SaveChanges();
                cargarDatos();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error al actualizar");
            }

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

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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace Sistema_Calificacion
{
    public partial class FormularioMaterias : Form
    {
        private SistemaCalificacionesDBEntities28 db;
        public FormularioMaterias()
        {
            db = new SistemaCalificacionesDBEntities28();
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
            string txtID = txtInsertMateriaID.Text.Trim();
            string nombre = txtInsertNombre.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1) 
            {
                MessageBox.Show("Llenar El Campo ID Con Un Numero Positivo");
                return;
            }

            if (db.Materias.Any(m => m.MateriaID == id)) 
            {
                MessageBox.Show($"Ya Existe Una Materia Con El ID {id}");
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Llenar El Campo Nombre");
                return;
            }

            if (nombre.Length < 1 || nombre.Length > 100)
            {
                MessageBox.Show("El Campo Nombre Deber Tener Entre 1 Hasta 100 Caracteres");
                return;
            }

            try
            {
                var materia = new Materia()
                {
                    MateriaID = id,
                    Nombre = nombre
                };

                db.Materias.Add(materia);
                db.SaveChanges();

                MessageBox.Show("Exito Al Insertar");

                cargarDatos();

                txtInsertMateriaID.Clear();
                txtInsertNombre.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Al Insertar");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string txtID = txtElimMateriaID.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1)
            {
                MessageBox.Show("Debes Introducir Un ID Valido");
                return;
            }

            try
            {
                var materia = db.Materias.Find(id);

                if (materia == null)
                {
                    MessageBox.Show($"No Hay Materia Con EL ID {id}");
                    return;
                }

                var confirmacion = MessageBox.Show($"Deseas Eliminar La Materia {materia.Nombre}?","Confirmar",MessageBoxButtons.YesNo);

                if (confirmacion == DialogResult.No) 
                {
                    return;
                }

                db.Materias.Remove(materia);
                db.SaveChanges();

                MessageBox.Show("Exito Al Eliminar Materia");

                cargarDatos();
                
                txtElimMateriaID.Clear();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error Al Eliminar Materia");
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string txtID = txtActMateriaID.Text.Trim();
            string nombre = txtActNombre.Text.Trim();

            if (string.IsNullOrEmpty(txtID) || !int.TryParse(txtID, out int id) || id < 1)
            {
                MessageBox.Show("Debes Introducir Un ID Valido");
                return;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Llenar El Campo Nombre");
                return;
            }

            if (nombre.Length < 1 || nombre.Length > 100)
            {
                MessageBox.Show("El Campo Nombre Deber Tener Entre 1 Hasta 100 Caracteres");
                return;
            }

            try
            {
                var materia = db.Materias.Find(id);

                if ( materia == null)
                {
                    MessageBox.Show($"No Existe Una Materia Con El ID {id}");
                    return;
                }
                materia.Nombre = nombre;

                MessageBox.Show("Exito Al Actualizar");

                db.SaveChanges();

                cargarDatos();

                txtActMateriaID.Clear();
                txtActNombre.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Al Actualizar");
            }


        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //Exporta a PDF y CSV

        }

        private void cargarDatos() 
        {
            var Materias = db.Materias.OrderBy(m => m.MateriaID)
                .Select( m => new 
                {
                    ID = m.MateriaID,
                    Nombre = m.Nombre            
                }).ToList();

            tablaContenido.DataSource = Materias;

            tablaContenido.Columns["ID"].Width = 100;
            tablaContenido.Columns["Nombre"].Width = 520;
        }

        

    }
}

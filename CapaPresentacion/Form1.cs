using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaDatos;
using System.Diagnostics.Contracts;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            // Crear un objeto Contacto con los datos ingresados por el usuario
            Contacto contacto = new Contacto
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Direccion = txtDireccion.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Celular = txtCelular.Text
            };

            // Llamar al método InsertarContacto del GestorContactos para insertar el contacto en la base de datos
            try
            {
                GestorContactos.InsertarContacto(contacto);
                MessageBox.Show("Contacto insertado correctamente");
                LimpiarCampos();

                CargarDatos();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar el contacto: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un contacto de la DataGridView
            if (dataGridViewContactos.SelectedRows.Count > 0)
            {
                // Obtener el ID del contacto seleccionado
                int id = Convert.ToInt32(dataGridViewContactos.SelectedRows[0].Cells["ID"].Value);

                // Crear un objeto Contacto con los datos ingresados por el usuario
                Contacto contacto = new Contacto
                {
                    ID = id,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Direccion = txtDireccion.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Celular = txtCelular.Text
                };

                // Llamar al método ModificarContacto del GestorContactos para modificar el contacto en la base de datos
                try
                {
                    GestorContactos.ModificarContacto(contacto);
                    MessageBox.Show("Contacto modificado correctamente");
                    LimpiarCampos();

                    CargarDatos();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al modificar el contacto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un contacto de la lista para modificarlo");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Validar que se haya ingresado un ID de contacto para buscar
            if (!string.IsNullOrWhiteSpace(txtID.Text))
            {
                // Obtener el ID ingresado por el usuario
                int id = Convert.ToInt32(txtID.Text);

                // Llamar al método BuscarContacto del GestorContactos para buscar el contacto en la base de datos
                try
                {
                    Contacto contacto = GestorContactos.BuscarContacto(id);
                    if (contacto != null)
                    {
                        // Mostrar los datos del contacto encontrado en los campos de texto
                        txtNombre.Text = contacto.Nombre;
                        txtApellido.Text = contacto.Apellido;
                        txtDireccion.Text = contacto.Direccion;
                        dtpFechaNacimiento.Value = contacto.FechaNacimiento;
                        txtCelular.Text = contacto.Celular;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún contacto con el ID especificado");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al buscar el contacto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ingresa un ID de contacto para buscar");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un contacto de la DataGridView
            if (dataGridViewContactos.SelectedRows.Count > 0)
            {
                // Obtener el ID del contacto seleccionado
                int id = Convert.ToInt32(dataGridViewContactos.SelectedRows[0].Cells["ID"].Value);

                // Confirmar la eliminación del contacto mediante un cuadro de diálogo
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este contacto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Llamar al método EliminarContacto del GestorContactos para eliminar el contacto de la base de datos
                    try
                    {
                        GestorContactos.EliminarContacto(id);
                        MessageBox.Show("Contacto eliminado correctamente");
                        LimpiarCampos();

                        CargarDatos();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error al eliminar el contacto: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un contacto de la lista para eliminarlo");
            }
        }

        private void LimpiarCampos()
        {
            txtID.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            dtpFechaNacimiento.Value = DateTime.Now;
            txtCelular.Text = string.Empty;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cadenaConexion = ConexionBD.cadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = "SELECT * FROM Contactos";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                DataTable tablaContactos = new DataTable();
                adaptador.Fill(tablaContactos);

                // Asignar los datos al BindingSource
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = tablaContactos;

                // Asignar el BindingSource al DataGridView
                dataGridViewContactos.DataSource = bindingSource;
            }
        }
        private void CargarDatos()
        {
            string cadenaConexion = ConexionBD.cadenaConexion;
            // Obtener los datos de la base de datos y asignarlos al DataGridView
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = "SELECT * FROM Contactos";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                DataTable tablaContactos = new DataTable();
                adaptador.Fill(tablaContactos);

                dataGridViewContactos.DataSource = tablaContactos;

            }
        }

        private void lblDireccion_Click(object sender, EventArgs e)
        {

        }
    }
}

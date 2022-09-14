﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace WindowsFormsApp_TP
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();   
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar articulo";
            lbTitulo.Text = "Update";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.Codigo = txbCodigo.Text;
                articulo.Nombre = txbNombre.Text;
                articulo.Descripcion = txbDescripcion.Text;

                //Recibo del comboBox el item seleccionado y hago un casteo del objeto.
                articulo.marca = (Marca)cboIdmarca.SelectedItem;
                articulo.categoria = (Categoria)cboIdcategoria.SelectedItem;

                articulo.ImagenUrl = txbURL.Text;
                // Validar si el formato del precio es vacio o es 0
              

                if (Convert.ToDecimal(txbPrecio.Text) > 0)
                {
                    articulo.Precio = Convert.ToDecimal(txbPrecio.Text);
                }
                else
                {
                    //si el precio es menor a 0 o vacio, entonces ir directamente al catch
                  
                        throw new Exception();
                }


                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Articulo modificado con exito");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Articulo agregado con exito");
                }
 
                this.Close();

            }
            catch (Exception ex)
            {
                if (txbPrecio.Text == ""|| Convert.ToDecimal(txbPrecio.Text)==0)
                    MessageBox.Show("El precio debe ser mayor a cero");

                DialogResult respuesta = MessageBox.Show("¿Desea intentar de nuevo?", "No es posible la operacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.No)
                {
                    Close();
                }
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                cboIdmarca.DataSource = marcaNegocio.listar();
                cboIdmarca.ValueMember = "Id";
                cboIdmarca.DisplayMember = "DescripcionMarca";
                cboIdcategoria.DataSource = categoriaNegocio.listar();
                cboIdcategoria.ValueMember = "Id";
                cboIdcategoria.DisplayMember = "Descripcion";

                if (articulo != null) 
                {
                     txbCodigo.Text = articulo.Codigo;
                     txbNombre.Text = articulo.Nombre;
                     txbDescripcion.Text = articulo.Descripcion;
                    // cargamos tambien la imagen del articulo?
                    // cargarImagen(articulo.Urlimagen);
                     cboIdmarca.SelectedValue = articulo.marca.Id;
                     cboIdcategoria.SelectedValue = articulo.categoria.Id;
                     txbURL.Text = articulo.ImagenUrl;
                     txbPrecio.Text = articulo.Precio.ToString();
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

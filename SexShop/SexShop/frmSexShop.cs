using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SexShop
{
    public partial class frmSexShop : Form
    {
        Datos odatos = new Datos();
        const int tam = 100000000;
        Juguetes[] aJuguetes = new Juguetes[tam];
        private bool crearjuguetenuevo = false;
        int c;

        public frmSexShop()
        {
            InitializeComponent();
            for (int i = 0; i < tam; i++)
            {
                aJuguetes[i] = null;
            }
        }

        private void frmSexShop_Load(object sender, EventArgs e)
        {
            Habilitar(false);
            CargarCombo(cboColor, "colores");
            CargarCombo(cboTipo, "tipos");
            CargarLista("Juguetes");
        }

        private void CargarLista(string nombreTabla)
        {
            odatos.leerTabla(nombreTabla);
            c = 0;
            while (odatos.pLector.Read())
            {
                Juguetes j = new Juguetes();
                if (!odatos.pLector.IsDBNull(0))
                    j.pCodigo = odatos.pLector.GetInt32(0);
                if (!odatos.pLector.IsDBNull(1))
                    j.pDescripcion = odatos.pLector.GetString(1);
                if (!odatos.pLector.IsDBNull(2))
                    j.pTipo = odatos.pLector.GetInt32(2);
                if (!odatos.pLector.IsDBNull(3))
                    j.pPrecio = Convert.ToDouble(odatos.pLector.GetDecimal(3));
                if (!odatos.pLector.IsDBNull(4))
                    j.pMedida = odatos.pLector.GetInt32(4);
                if (!odatos.pLector.IsDBNull(5))
                    j.pColor = odatos.pLector.GetInt32(5);

                aJuguetes[c] = j;
                c++;
            }
            odatos.pLector.Close();
            odatos.desconectar();

            lstJuguetes.Items.Clear();

            for (int i = 0; i < c; i++)
            {
                lstJuguetes.Items.Add(aJuguetes[i].ToString());
            }
        }
        private void Habilitar(bool x)
        {
            rbtEstandar.Enabled = x;
            rbtGrande.Enabled = x;
            txtDescrip.Enabled = x;
            txtPrecio.Enabled = x;
            cboTipo.Enabled = x;
            cboColor.Enabled = x;
            btnCancelar.Enabled = x;
            btnGrabar.Enabled = x;
            btnNuevo.Enabled = !x;
            btnEliminar.Enabled = x;
            btnEditar.Enabled = !x;
            lstJuguetes.Enabled = x;
        }

        private void Limpiar()
        {
            txtDescrip.Clear();
            cboTipo.SelectedIndex = -1;
            txtPrecio.Clear();
            cboColor.SelectedIndex = -1;
        }
        private bool validarCampos()
        {
            bool ok = true;

            if(txtDescrip.Text=="")
            {
                ok = false;
                errorProvider1.SetError(txtDescrip, "Ingresar una Descripcion");
            }

            if(cboTipo.SelectedItem==null)
            {
                ok = false;
                errorProvider1.SetError(cboTipo, "Elegi un Tipo");
            }

            if(txtPrecio.Text=="")
            {
                ok = false;
                errorProvider1.SetError(txtPrecio, "Pone un precio");
            }

            if(cboColor.SelectedItem==null)
            {
                ok = false;
                errorProvider1.SetError(cboColor, "Elegi un Color Sexy");
            }
            return ok;
        }

        private void BorrarMensajeError()
        {
            errorProvider1.SetError(txtDescrip, "");
            errorProvider1.SetError(cboTipo, null);
            errorProvider1.SetError(txtPrecio, "");
            errorProvider1.SetError(cboColor, null);
        }
        private void CargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable tabla = new DataTable();
            tabla = odatos.consultartabla(nombreTabla);
            combo.DataSource = tabla;
            combo.ValueMember = tabla.Columns[0].ColumnName;
            combo.DisplayMember = tabla.Columns[1].ColumnName;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            BorrarMensajeError();
            if (validarCampos())
            {
                string consultaSQL = "";
                if (crearjuguetenuevo)
                {
                    Juguetes j = new Juguetes();
                    j.pDescripcion = txtDescrip.Text;
                    j.pPrecio = int.Parse(txtPrecio.Text);
                    j.pTipo = (int)cboTipo.SelectedValue;//
                    j.pColor = (int)cboColor.SelectedValue;//
                    if (rbtEstandar.Checked)
                    {
                        j.pMedida = 1;
                    }
                    if (rbtGrande.Checked)
                    {
                        j.pMedida = 2;
                    }

                    //if (!existe(j.pCodigo))
                    //{
                    consultaSQL = $"INSERT INTO juguetes (descripcion,id_tipo,precio,medida,id_color)VALUES('{j.pDescripcion}',{j.pTipo},{j.pPrecio},{j.pMedida},{j.pColor})";
                    //}
                    odatos.actualizar(consultaSQL);
                }
                //boton editar
                else
                {
                    int i = lstJuguetes.SelectedIndex;
                    aJuguetes[i].pDescripcion = txtDescrip.Text;
                    aJuguetes[i].pPrecio = int.Parse(txtPrecio.Text);
                    aJuguetes[i].pTipo = (int)cboTipo.SelectedValue;
                    aJuguetes[i].pColor = (int)cboColor.SelectedValue;

                    if (rbtEstandar.Checked)
                    {
                        aJuguetes[i].pMedida = 1;
                    }

                    if (rbtGrande.Checked)
                    {
                        aJuguetes[i].pMedida = 2;
                    }

                    consultaSQL = $"UPDATE juguetes SET descripcion='{aJuguetes[i].pDescripcion}',id_tipo={aJuguetes[i].pTipo},precio={aJuguetes[i].pPrecio},medida={aJuguetes[i].pMedida},id_color={aJuguetes[i].pColor}" + $"WHERE cod_juguete={aJuguetes[i].pCodigo}";

                    odatos.actualizar(consultaSQL);
                }
                CargarLista("Juguetes");
            }   //Habilitar(false);
            //crearjuguetenuevo = false;
            //}
        }
            //private bool existe(int pk)
            //{
            //    bool x = false;
            //    for (int i = 0; i < c; i++)
            //    {

            //        if (aJuguetes[i].pCodigo == pk)
            //            x = true;

            //    }
            //    return x;
            //}

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            crearjuguetenuevo = true;
            rbtEstandar.Checked = true;
            Limpiar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            crearjuguetenuevo = false;
        }

        private void txtDescrip_KeyPress(object sender, KeyPressEventArgs e)
        {
            //>32 para que me tome el espacio
            if ((e.KeyChar > 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se pueden ingresar letras", "Advertencia", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo se pueden ingresar numeros", "Advertencia", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            crearjuguetenuevo = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Habilitar(false);
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de eliminar este producto?", "eliminando",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string consultaSQL = $"DELETE FROM juguetes WHERE cod_juguete={aJuguetes[lstJuguetes.SelectedIndex].pCodigo}";
                odatos.actualizar(consultaSQL);
                CargarLista("juguetes");
                Habilitar(false);
            }
        }
    }
}

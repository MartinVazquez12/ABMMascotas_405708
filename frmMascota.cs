using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

//CURSO – LEGAJO – APELLIDO – NOMBRE

namespace ABMMascotas
{
    public partial class frmMascota : Form
    {

        public AcessoDatos oBD;
        List<Mascota> lMascotas;

        public frmMascota()
        {
            InitializeComponent();
            oBD= new AcessoDatos();
            lMascotas = new List<Mascota>(); 

        }

        private void frmMascota_Load(object sender, EventArgs e)
        {
            habilitar(true);

            CargarCombo(cboEspecie, "Especies");
            CargarLista(lstMascotas, "Mascotas");
        }

        private void CargarLista(ListBox lista, string nombreTabla)
        {
            List<Mascota> mst = new List<Mascota>();
            DataTable dt = oBD.ConsultarBD(nombreTabla);

            Mascota oMasc = null;
            foreach (DataRow fila in dt.Rows)
            {
                oMasc = new Mascota();
                oMasc.Codigo = Convert.ToInt32(fila[0]);
                oMasc.Nombre = fila[1].ToString();
                oMasc.Especie = Convert.ToInt32(fila[2]);
                oMasc.Sexo = Convert.ToInt32(fila[3]);
                oMasc.FechaNacimiento = Convert.ToDateTime(fila[4]);

                lista.Items.Add(oMasc);
            }


            //lista.Items.Clear();
            //lista.DataSource = lstMascotas;

            
            
        }

        private void CargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable tabla = oBD.ConsultarBD(nombreTabla);
            combo.DataSource = tabla;
            combo.ValueMember = tabla.Columns[0].ColumnName;
            combo.DisplayMember = tabla.Columns[1].ColumnName;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public void habilitar(bool x)
        {
            txtCodigo.Enabled= !x;
            txtNombre.Enabled= !x;
            cboEspecie.Enabled= !x;
            rbtMacho.Enabled= !x;
            rbtHembra.Enabled= !x;
            dtpFechaNacimiento.Enabled= !x;
            lstMascotas.Enabled= !x;
            btnGrabar.Enabled= !x;
            btnNuevo.Enabled= x;
            btnSalir.Enabled= x;
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ESTA SEGURO DE SALIR?", "SALIR", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();   
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitar(false);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            habilitar(true);
            //valida datos...
            if (ValidarDatos())
            {
                Mascota mascota = new Mascota();
                mascota.Nombre = txtNombre.Text;
                mascota.Codigo = Convert.ToInt32(txtCodigo.Text);
                mascota.Especie = (int)cboEspecie.SelectedIndex;
                if (rbtMacho.Checked)
                    mascota.Sexo = 1;
                else
                    mascota.Sexo = 2;
                mascota.FechaNacimiento = Convert.ToDateTime(dtpFechaNacimiento.Value);

                List<Parametros> lParametros = new List<Parametros>();
                lParametros.Add(new Parametros("@codigo", mascota.Codigo));
                lParametros.Add(new Parametros("@nombre", mascota.Nombre));
                lParametros.Add(new Parametros("@especie", mascota.Especie));
                lParametros.Add(new Parametros("@sexo", mascota.Sexo));
                lParametros.Add(new Parametros("@fechanacimiento", mascota.FechaNacimiento));

                oBD.InsertarNuevo("mascotas", lParametros);

                CargarLista(lstMascotas, "Mascotas");
            }

        }

        private bool ValidarDatos()
        {
            if (txtCodigo.Text == string.Empty)
            {
                MessageBox.Show("Falta insertar el codigo");
                txtCodigo.Focus();
                return false;
            }
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Falta insertar el nombre");
                txtNombre.Focus();
                return false;
            }
            if (cboEspecie.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una especie");
                cboEspecie.Focus();
                return false;
            }


            return true;
        }

        private void lstMascotas_SelectedIndexChanged(object sender, EventArgs e)
        {
     
        }

    }
}

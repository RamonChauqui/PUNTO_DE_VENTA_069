using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUNTO_DE_VENTA_069
{
    public partial class UsuariosOK : Form
    {
        public UsuariosOK()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" )
            {

                try
                {
            SqlConnection con = new SqlConnection();

                con.ConnectionString = Conexion.ConexionMaestra.conexion;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insertar_Usuarios", con );
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombres",txtNombre.Text);
                cmd.Parameters.AddWithValue("@Login", txtLogin.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);   
                
                cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Rol", txtRol.Text);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();/*Paquete para transformar de tipos de datos binarios a tipos de datos Sql Server */
                ICONO.Image.Save(ms, ICONO.Image.RawFormat);
                
                cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());/*transformacion a binaro y me agrega la imagen a la base de datos*/
                cmd.Parameters.AddWithValue("@Nombre_de_Icono", lblNumeroIcono.Text);

                cmd.ExecuteNonQuery();
                con.Close();

                mostrar();
                panel3.Visible = false;

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

    

            }

        }

        private void mostrar()
        {
            try
            {
    
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Conexion.ConexionMaestra.conexion;
                con.Open();           

            da = new SqlDataAdapter("mostrar_suario", con);
                da.Fill(dt);
                dataListado.DataSource = dt;
                con.Close();
                dataListado.Columns[1].Visible = false;
                dataListado.Columns[5].Visible = false;
                dataListado.Columns[6].Visible = false;
                dataListado.Columns[7].Visible = false;
                dataListado.Columns[8].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }                    
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox3.Image;
            lblNumeroIcono.Text = "1";
            lblNumeroIcono.Visible = false;
            panelIcono.Visible = false;
        }

        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            panelIcono.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            ICONO.Image = pictureBox4.Image;
            lblNumeroIcono.Text = "2";
            lblNumeroIcono.Visible = false;
            panelIcono.Visible = false;
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox5.Image;
            lblNumeroIcono.Text = "3";
            lblNumeroIcono.Visible = false;
            panelIcono.Visible = false;
            
        }
    
        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            ICONO.Image = pictureBox6.Image;
            lblNumeroIcono.Text = "5";
            lblNumeroIcono.Visible = false;
            panelIcono.Visible = false;

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void UsuariosOK_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panelIcono.Visible = false;

            mostrar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            lblAnuncioIcono.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            lblIdUsuario.Text = dataListado.SelectedCells[1].Value.ToString();
            txtNombre.Text = dataListado.SelectedCells[2].Value.ToString();
            txtLogin.Text = dataListado.SelectedCells[3].Value.ToString();
            
            txtPassword.Text = dataListado.SelectedCells[4].Value.ToString();

            ICONO.BackgroundImage = null;
            byte[] b = (Byte[])dataListado.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);               
            ICONO.Image =  Image.FromStream(ms);

            lblAnuncioIcono.Visible = false;

            lblNumeroIcono.Text = dataListado.SelectedCells[6].Value.ToString();
            txtCorreo.Text = dataListado.SelectedCells[7].Value.ToString();
            txtRol.Text = dataListado.SelectedCells[8].Value.ToString();

            panel3.Visible = true;


        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
    }
}

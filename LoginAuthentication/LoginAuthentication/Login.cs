using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LoginAuthentication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMenssage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("SERVER=BOGPB15S0612HPF;DATABASE=LoginAuthentication;integrated security=true;");

        public void ingreso(string usuario, string contrasena)
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserName,Password FROM Users WHERE UserName = @usuario AND Password = @pas ", con);
                cmd.Parameters.AddWithValue("usuario", txtUser.Text);
                cmd.Parameters.AddWithValue("pas", txtPass.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Ingresado");

                 

                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña invalida, intente de nuevo");
                    txtUser.Text = "Username";
                    txtPass.UseSystemPasswordChar = false;
                    txtPass.Text = "Password";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Register().Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUser_Enter_1(object sender, EventArgs e)
        {
            if (txtUser.Text == "Username")
            {
                txtUser.Text = "";

            }
        }

        private void txtUser_Leave_1(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "Username";

            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Password")
            {
                txtPass.Text = "";
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                
                txtPass.Text = "Password";

            }
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMenssage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ingreso(this.txtUser.Text, this.txtPass.Text);
        }
    }
}

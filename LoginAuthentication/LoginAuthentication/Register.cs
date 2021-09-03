using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LoginAuthentication
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtLastName_Enter(object sender, EventArgs e)
        {
            if (txtLastName.Text == "Last Name")
            {
                txtLastName.Text = "";

            }
        }

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            if (txtLastName.Text == "")
            {
                txtLastName.Text = "Last Name";

            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Username")
            {
                txtUserName.Text = "";
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Username";
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Email";
            }
        }

        private void txtFirtsName_Enter(object sender, EventArgs e)
        {
            if (txtFirtsName.Text == "Firts Name")
            {
                txtFirtsName.Text = "";
            }
        }

        private void txtFirtsName_Leave(object sender, EventArgs e)
        {
            if (txtFirtsName.Text == "")
            {
                txtFirtsName.Text = "Firts Name";
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Password")
            {
                txtPass.Text = "";
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Password";
            }
        }

        private void txtRepass_Enter(object sender, EventArgs e)
        {
            if (txtRepass.Text == "Re - enter Password")
            {
                txtRepass.Text = "";
            }
        }

        private void txtRepass_Leave(object sender, EventArgs e)
        {
            if (txtRepass.Text == "")
            {
                txtRepass.Text = "Re - enter Password";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Login().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("SERVER=BOGPB15S0612HPF;DATABASE=LoginAuthentication;integrated security=true;");
            con.Open();

            if (txtUserName.Text == "Username" || txtPass.Text == "Password" || txtFirtsName.Text == "Firts Name" || txtLastName.Text == "Last Name" || txtRepass.Text == "Re - enter Password" || txtEmail.Text == "Email")
            {
                MessageBox.Show("Por favor ingrese todos los datos");
            }
            else
            {

                SqlCommand cmd = new SqlCommand("SELECT UserName FROM Users WHERE UserName = @usuario ", con);
                cmd.Parameters.AddWithValue("usuario", txtUserName.Text);
                cmd.Parameters.AddWithValue("pas", txtPass.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
       
                sda.Fill(dt);
              
                    if (dt.Rows.Count == 0)
                    {
                        if (Email_Valido(txtEmail.Text))
                        {

                            if (txtPass.Text == txtRepass.Text)
                            {
                                string insertar = "INSERT INTO Users(Username ,Password ,FirstName ,LastName ,EnterPassword ,Email)VALUES(@Username,@Password,@FirstName,@LastName,@Repass,@Email) ";
                                SqlCommand cmd2 = new SqlCommand(insertar, con);

                                cmd2.Parameters.AddWithValue("@Username", txtUserName.Text);
                                cmd2.Parameters.AddWithValue("@Password ", txtPass.Text);
                                cmd2.Parameters.AddWithValue("@FirstName ", txtFirtsName.Text);
                                cmd2.Parameters.AddWithValue("@LastName ", txtLastName.Text);
                                cmd2.Parameters.AddWithValue("@Repass", txtRepass.Text);
                                cmd2.Parameters.AddWithValue("@Email ", txtEmail.Text);


                                cmd2.ExecuteNonQuery();

                                MessageBox.Show("You are registered, please confirm your email");

                                this.Hide();
                                new Login().Show();
                            }
                            else
                            {
                                MessageBox.Show("Passwords do not match");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El email es incorrecto");
                        }

                    }
                    else
                    {
                        MessageBox.Show("This user already exists, try another");
                    }
                
            }
        }

        private void button1_Enter(object sender, EventArgs e)
        {

        }

        private void txtFirtsName_TextChanged(object sender, EventArgs e)
        {

        }
        //VALIDAR LAS LETRAS
        private void txtFirtsName_KeyPress(object sender, KeyPressEventArgs v)
        {
            SoloLetras(v);
        }


        public static void SoloLetras(KeyPressEventArgs v)
        {
            if (Char.IsLetter(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (Char.IsSeparator(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (Char.IsControl(v.KeyChar))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Solo Letras");
            }
        }
        public static bool Email_Valido(String email)
        {

            return Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

        }
        private void txtLastName_KeyPress(object sender, KeyPressEventArgs v)
        {
            SoloLetras(v);
        }
    }
}

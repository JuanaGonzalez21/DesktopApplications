using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendEmail
{
    public partial class SendEmail : Form
    {
        public SendEmail()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add(txtPara.Text);
            msg.Subject = txtasunto.Text;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
           // msg.Bcc.Add(txtcc.Text);

            msg.Body = txtcuerpo.Text;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new System.Net.Mail.MailAddress("pruebasgonzalezjuana@gmail.com");

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            cliente.Credentials = new System.Net.NetworkCredential("pruebasgonzalezjuana@gmail.com", "Pruebas123");

            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";


            try
            {
                cliente.Send(msg);
                MessageBox.Show("Correo enviado");

            }catch (Exception)
            {
                MessageBox.Show("Error al enviar");
            }
        }
    }
}

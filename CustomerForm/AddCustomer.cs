using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace CustomerForm
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            button1.Enabled = false;
            label6.Visible = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)  
        {
            try
            {
                ErrorProvider err = new ErrorProvider();
                if (IsValidEmail(textBox3.Text) == false)
                {
                    err.SetError(textBox3, "Invalid Email");
                    textBox3.Text = string.Empty;
                }

                if (IsValidPhone(textBox4.Text) == false)
                {
                    err.SetError(textBox4, "Invalid Phone number");
                    textBox4.Text = string.Empty;
                }

                if ((textBox1.Text.Length > 0) && (textBox2.Text.Length > 0)
                    && (textBox3.Text.Length > 0) && (textBox4.Text.Length > 0)
                    && (richTextBox1 .Text.Length > 0))
                {
                    err.Clear();
                    string DQ = "\"";
                    MySqlConnection conn = new MySqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
                    string strmycomd = "insert into  customer(firstname,lastname,email,phone,address) " +
                                   "values (" + DQ + textBox1.Text + DQ + ", " + DQ + textBox2.Text + DQ + ", " + DQ + textBox3.Text + DQ +
                                   ", " + DQ + textBox4.Text + DQ + ", " + DQ + richTextBox1.Text + DQ + " );";
                    MySqlCommand mycmd = new MySqlCommand(strmycomd, conn);
                    try
                    {
                        conn.Open();
                        mycmd.ExecuteNonQuery();
                        
                        label6.Text = "Data Succesfully addded in database";
                        label6.BackColor = Color.White;
                        label6.ForeColor = Color.Green;
                        
                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox4.Text = string.Empty;
                        richTextBox1.Text = string.Empty;
                        button1.Enabled = false;
                        label6.Visible = true;

                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            catch (Exception e2)
            {
                label2.Visible = true;
                label2.Text = "Erorr: " + e2.Message;
            }
                     
        
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsValidEmail(string stremail)
        {
            bool valid = false;

            int posatcchar = stremail.IndexOf('@');
            int posdotcchar = stremail.LastIndexOf('.');
            if (posatcchar > 2 && posdotcchar > posatcchar + 2)
            {
                valid = true;
            }
            return valid;
        }

        private bool IsValidPhone(string strphone)
        {
            bool valid = false;
            char[] numchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int charmatchcount = 0;
            if (strphone.Length == 10 )
            {
                foreach (char c in strphone)
                {
                    for (int i = 0; i < numchars.Length; i++)
                    {
                        if (c == numchars[i])
                        {
                            //charmatch = true;
                            charmatchcount++;
                            break;
                        }
                    }
                }

                if (charmatchcount == strphone.Length)
                {
                    valid = true;
                }
            }
            return valid;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
            {
            if (label6.Visible == true)
            {
                label6.Visible = false;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

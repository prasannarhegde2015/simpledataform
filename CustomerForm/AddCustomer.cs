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
            textBox3.Validated += new System.EventHandler(this.textBox3_Validated);
            textBox4.Validated += new System.EventHandler(this.textBox4_Validated);
            this.FormClosed += AddCustomer_FormClosed;
        
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

              //  ValidateEmail();
              //  ValidatePhone();
            

                if ((textBox1.Text.Length > 0) && (textBox2.Text.Length > 0)
                    && (textBox3.Text.Length > 0) && (textBox4.Text.Length > 0)
                    && (richTextBox1.Text.Length > 0)
                    && IsValidEmail(textBox3.Text)
                    && IsValidPhone(textBox4.Text)
                    )
                {

                    
                    
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

                        textBox1.ReadOnly = true;
                        textBox2.ReadOnly = true;
                        textBox3.ReadOnly = true;
                        textBox4.ReadOnly = true;
                        richTextBox1.ReadOnly = true;
    
                        button1.Enabled = false;
                        label6.Visible = true;

                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter All the Details", "Add Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception e2)
            {
                label2.Visible = true;
                label2.Text = "Erorr: " + e2.Message;
            }
                     
        
        }

        private void textextBox3(object sender, EventArgs e)
        {

        }
        private void textBox3_Validated(object sender, EventArgs e)
        {
            ValidateEmail();
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            ValidatePhone();
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

        

        public bool IsValidPhone(string strphone)
        {
            bool valid = false;
            strphone = strphone.Replace(" ", "");
            char[] numchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' ,'+'};
            int charmatchcount = 0;
            if (strphone.Length >= 10 && strphone.Length <= 20)
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

        private void ValidateEmail()
        {
            
            if (IsValidEmail(textBox3.Text) == false)
            {  errorProvider1.SetError(textBox3, "Invalid Email");
              //  textBox3.Text = string.Empty;
              //  errorProvider1.Icon = new Icon(@"e:\fl.ico");
            }
            else
            {
                // err.Clear();
                errorProvider1.SetError(textBox3, "");
              //  errorProvider1.Icon = new Icon(@"e:\fc.ico");

            }
        }

        private void ValidatePhone()
        {
            
            
            if (IsValidPhone(textBox4.Text) == false)
            {
                errorProvider1.SetError(textBox4, "Invalid Phone");
              //  errorProvider1.Icon = new Icon(@"e:\fl.ico");
              //  textBox4.Text = string.Empty;
            }
            else
            {
                // err.Clear();
                errorProvider1.SetError(textBox4, "");
             //   errorProvider1.Icon = new Icon(@"e:\fc.ico");
            }
        }

        private void AddCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  MessageBox.Show("event fired!!");
            Form1 fm1 = new Form1();
            fm1.refreshgrid();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

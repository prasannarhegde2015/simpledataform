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
    public partial class EditCustomer : Form
    {
        private string _holder;
        private string _fn, _ln, _em, _ph, _ad;
        public string holder
        {
            get
            {
                return _holder;
            }
            set
            {
                _holder = value;
            }
        }
        public string ad
        {
            get
            {
                return _ad;
            }
            set
            {
                _ad = value;
            }
        }
        public string fn
        {
            get
            {
                return _fn;
            }
            set
            {
                _fn = value;
            }
        }
        public string ln
        {
            get
            {
                return _ln;
            }
            set
            {
                _ln = value;
            }
        }
        public string em
        {
            get
            {
                return _em;
            }
            set
            {
                _em = value;
            }
        }
        public string ph
        {
            get
            {
                return _ph;
            }
            set
            {
                _ph = value;
            }
        }

        public EditCustomer(string st)
        {
            this.holder = st;
            InitializeComponent();
            label6.Visible = false;
            Form1 fm1 = new Form1();
            this.MaximizeBox = false;
            DataTable dtedt = fm1.getSelecteddatafromDB("email", this.holder);
            this.fn = dtedt.Rows[0]["firstname"].ToString();
            this.ln = dtedt.Rows[0]["lastname"].ToString();
            this.em = dtedt.Rows[0]["email"].ToString();
            this.ph = dtedt.Rows[0]["phone"].ToString();
            this.ad = dtedt.Rows[0]["address"].ToString();
            textBox1.Text = this.fn;
            textBox2.Text = this.ln;
            textBox3.Text = this.em;
            textBox3.ReadOnly = true;
            textBox4.Text = this.ph;
            richTextBox1.Text = this.ad;
        }
        public EditCustomer()
        {
            InitializeComponent();
            Form1 fm1 = new Form1();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (textBox1.Text != fn)
            {
                updatedata("firstname", textBox1.Text);
                sb.Append("FirstName");
                sb.Append("\t");
            }
            if (textBox2.Text != ln)
            {
                updatedata("lastname", textBox2.Text);
                sb.Append("LastName");
                sb.Append("\t");
            }
            if (textBox4.Text != ph)
            {
                updatedata("phone", textBox4.Text);
                sb.Append("Phone");
                sb.Append("\t");
            }
            if (richTextBox1.Text != ad)
            {
                updatedata("address", richTextBox1.Text);
                sb.Append("Address");
                sb.Append("\t");
            }

            label6.Text = "Data Succesfully updated for " + sb.ToString() + " in database";
            label6.BackColor = Color.White;
            label6.ForeColor = Color.Green;
            label6.Visible = true;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            button1.Enabled = false;
            label1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updatedata(string colname , string colval)
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
                    && (richTextBox1.Text.Length > 0))
                {

                  //  string DQ = "\"";
                    MySqlConnection conn = new MySqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
                    string strmycomd = "update  customer " +
                                   "set " + colname + "='" + colval + "' where email='" + this.holder + "';";
                    MySqlCommand mycmd = new MySqlCommand(strmycomd, conn);
                    try
                    {
                        conn.Open();
                        mycmd.ExecuteNonQuery();

                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            catch (Exception e2)
            {
                label6.Visible = true;
                label6.Text = "Erorr: " + e2.Message;
            }
                     
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
            if (strphone.Length == 10)
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
    }
}




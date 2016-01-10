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
    public partial class DeleteCustomer : Form
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


        public DeleteCustomer(string st)
        {
            this.holder = st;
            InitializeComponent();
            this.MaximizeBox = false;
            label6.Visible = false;
            Form1 fm1 = new Form1();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           DialogResult  result = MessageBox.Show("Delete Data For " + holder, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
           if (result == DialogResult.Yes)
           {
               deletedata();
               label6.Text = "Data Succesfully Deleted for " + this.holder + " in database";
               label6.BackColor = Color.White;
               label6.ForeColor = Color.Green;
               label6.Visible = true;
               textBox1.Text = string.Empty;
               textBox2.Text = string.Empty;
               textBox3.Text = string.Empty;
               textBox4.Text = string.Empty;
               richTextBox1.Text = string.Empty;
               button1.Enabled = false;
               
           }
           else
           {
               MessageBox.Show("Delete Cancelled By user for : " + holder, "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
        }

        private void deletedata()
        {
            try
            {
                

                if ((textBox1.Text.Length > 0) && (textBox2.Text.Length > 0)
                    && (textBox3.Text.Length > 0) && (textBox4.Text.Length > 0)
                    && (richTextBox1.Text.Length > 0))
                {
                    //  string DQ = "\"";
                    MySqlConnection conn = new MySqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
                    string strmycomd = "delete from   customer " +
                                      " where email='" + this.holder + "';";
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

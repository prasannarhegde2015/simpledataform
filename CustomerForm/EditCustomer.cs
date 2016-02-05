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
        private string _fn, _ln, _em, _ph, _ad, _colname;
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
        private string _dataupdated;
        public string dataupdated
        {
            get{return _dataupdated;}
            set { _dataupdated = value; }

        }
        public string colname1
        {
            get { return _colname; }
            set { _colname = value; }
        }

        public EditCustomer(string st)
        {
            this.holder = st;
            InitializeComponent();
            label6.Visible = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            pouplateFields(this.holder);
            textBox3.Validated +=  new System.EventHandler(this.textBox3_Validated);
            textBox4.Validated += new System.EventHandler(this.textBox4_Validated);
            this.FormClosed += new FormClosedEventHandler(EditCustomer_FormClosed);
            
        }
        public EditCustomer()
        {
            InitializeComponent();
            Form1 fm1 = new Form1();


        }

        public void pouplateFields(string strip)
        {
             DataTable dtedt = new DataTable();
            AddCustomer addcus = new AddCustomer();
            Form1 fm1 = new Form1();
            if (strip.Length > 0)
            {
                if (strip.Contains("@") == true)
                {
                    dtedt = fm1.getSelecteddatafromDB("email", this.holder);
                    this.colname1 = "email";

                }
                else if (addcus.IsValidPhone(strip))
                {
                    dtedt = fm1.getSelecteddatafromDB("phone", this.holder);
                    this.colname1 = "phone";
                }
                else
                {
                    dtedt = fm1.getSelecteddatafromDB("custid", this.holder);
                    this.colname1 = "custid";
                }
                if (dtedt.Rows.Count == 0)
                {
                    MessageBox.Show("No Records Found for " + this.colname1 + " value " + this.holder);
                    return;
                }
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
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox4.ReadOnly = true;
                richTextBox1.ReadOnly = true;
                textBox1.BackColor = Color.Green;
                textBox1.ForeColor = Color.White;
                textBox2.BackColor = Color.Green;
                textBox2.ForeColor = Color.White;
                textBox3.BackColor = Color.Green;
                textBox3.ForeColor = Color.White;
                textBox4.BackColor = Color.Green;
                textBox4.ForeColor = Color.White;
                richTextBox1.BackColor = Color.Green;
                richTextBox1.ForeColor = Color.White;
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Bold);
                button1.Enabled = false;
                btnCancelEdit.Enabled = false;
            }
            else
            {
                MessageBox.Show("Selected Record did not get value for Column", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            if (textBox1.Text != fn)
            {
                updatedata("firstname", textBox1.Text);
                sb.Append("FirstName");
                sb.Append(", ");
            }
            if (textBox2.Text != ln)
            {
                updatedata("lastname", textBox2.Text);
                sb.Append("LastName");
                sb.Append(", ");
            }
            if (textBox3.Text != em && IsValidEmail(textBox3.Text) )
            {
                updatedata("email", textBox3.Text);
                sb.Append("Email");
                sb.Append(",");
            }
            if (textBox4.Text != ph  && IsValidEmail(textBox4.Text))
            {
                updatedata("phone", textBox4.Text);
                sb.Append("Phone");
                sb.Append(", ");
            }
            if (richTextBox1.Text != ad)
            {
                updatedata("address", richTextBox1.Text);
                sb.Append("Address");
                sb.Append(", ");
            }
            if (this.dataupdated == "yes")
            {
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
                btnEdit.Enabled = true;
                btnCancelEdit.Enabled = false;

                pouplateFields(this.holder);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox3_Validated(object sender, EventArgs e)
        {
            ValidateEmail();
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            ValidatePhone();
        }

        private void ValidateEmail()
        {

            if (IsValidEmail(textBox3.Text) == false)
            {
                errorProvider1.SetError(textBox3, "Invalid Email");
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
        private void updatedata(string colname , string colval)
        {
            try
            {
                

                if ((textBox1.Text.Length > 0) && (textBox2.Text.Length > 0)
                    && (textBox4.Text.Length > 0)
                    && (richTextBox1.Text.Length > 0))
                {

                    //  string DQ = "\"";
                    MySqlConnection conn = new MySqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
                    string strmycomd = "update  customer " +
                                   "set " + colname + "='" + colval + "' where " + this.colname1 + "='" + this.holder + "';";
                    MySqlCommand mycmd = new MySqlCommand(strmycomd, conn);
                    try
                    {
                        conn.Open();
                        mycmd.ExecuteNonQuery();
                        this.dataupdated = "yes";

                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    this.dataupdated = "no";
                    MessageBox.Show("Data Not updated...First Name , Last Name Phone Address Are mandatory for update", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            char[] numchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' ,'+',')','(','-'};
            int charmatchcount = 0;
            if (strphone.Length >= 10 && strphone.Length <= 15)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            richTextBox1.ReadOnly = false;

            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;

            button1.Enabled = true;
            btnEdit.Enabled = false;
            btnCancelEdit.Enabled = true;
        }


        private void EditCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fm1 = new Form1();
            fm1.refreshgrid();
        }
        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            button1.Enabled = false;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox3.ReadOnly = true;
            richTextBox1.ReadOnly = true;
            btnCancelEdit.Enabled = false;
           // label6.Visible = true;
           //   label6.Text = "Save Changes Cancelled by User....";
            MessageBox.Show("Save Changes Cancelled by User...", "Edit cancl", MessageBoxButtons.OK, MessageBoxIcon.Information);

            pouplateFields(this.holder);
        }
    }
}




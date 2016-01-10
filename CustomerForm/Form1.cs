using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace CustomerForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
            dataGridView1.ReadOnly = true;
            label3.Visible = false;
            button2.Enabled = false;
            button3.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedItem = "email";
            
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCustomer add = new AddCustomer();
            add.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getdatafromDB();
            button3.Enabled = true;
            button2.Enabled = true;
            dataGridView1.Visible = true;
            dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(OnRowHeaderMouseClick);
        }

        public DataTable getdatafromDB()
        {
            try
            {
                DataTable dtble = new DataTable();


                MySqlConnection oconn = new MySqlConnection();
                oconn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
                string odbccmdtext = "Select * from customer;";
                MySqlCommand ocmd = new MySqlCommand(odbccmdtext, oconn);
                oconn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(ocmd);
                da.Fill(dtble);
                oconn.Close();
                return dtble;
            }
            catch
            {
                throw new Exception();
            }

        }

        public DataTable getSelecteddatafromDB(string columnname, string columnvalue)
        {
            try
            {
                DataTable dtble = new DataTable();


                MySqlConnection oconn = new MySqlConnection();
                oconn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
                string odbccmdtext = "Select * from customer where "+columnname+"='"+columnvalue+"';";
                MySqlCommand ocmd = new MySqlCommand(odbccmdtext, oconn);
                oconn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(ocmd);
                da.Fill(dtble);
                oconn.Close();
                return dtble;
            }
            catch
            {
                throw new Exception();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                if (getSelecteddatafromDB(comboBox1.SelectedItem.ToString(), textBox2.Text).Rows.Count == 0)
                {
                    MessageBox.Show("No Mathing Records found", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dataGridView1.DataSource = getSelecteddatafromDB(comboBox1.SelectedItem.ToString(), textBox2.Text);
                dataGridView1.Visible = true;
                button3.Enabled = true;
                button2.Enabled = true;
                dataGridView1.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(OnRowHeaderMouseClick);
                
            }
            else
            {
                MessageBox.Show("Please Provide a Value to search", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Visible = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }


        private void OnRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.editcustid = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
            label3.Text = this.editcustid;
            
            // then perform your select statement according to that label
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ( editcustid != null)
            {
                EditCustomer edtcustm = new EditCustomer(this.editcustid);
                edtcustm.Show();
            }
            else
            {
                MessageBox.Show("Please Select a Grid Row by clicking on Row Hader", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            

        }
        private string _editcustid;
        public string editcustid
        {
            get
            {
                return _editcustid;
            }
            
            set 
            {
                _editcustid = value;
                
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteCustomer delcust = new DeleteCustomer(this.editcustid);
            delcust.Show();
        }
    }
}

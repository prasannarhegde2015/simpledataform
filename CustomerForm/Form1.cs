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

        private string _impfilename;
        private int _clicknum = 0;
        public string impfilename
        {
            get { return _impfilename; }
            set { _impfilename = value; }
        }
        public int clickNumbers
        {
            get { return _clicknum; }
            set { _clicknum = value; }

        }
        public Form1()
        {
            InitializeComponent();
            this.Enter +=Form1_GotFocus;
            dataGridView1.Visible = false;
            dataGridView1.ReadOnly = true;
            label3.Visible = false;
            button2.Enabled = false;
            button3.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedItem = "email";
            dataGridView1.BackgroundColor = Color.AliceBlue;
            dataGridView1.ForeColor = Color.BlueViolet;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            btnRefresh.MouseHover += btnRefresh_MouseHover;
       //     AddCustomer.ActiveForm.FormClosed += AddCustomer_FormClosed;
            btnRefresh.Visible = false;
            lblPageSize.Visible = false;
            btnFirstPage.Visible = false;
            btnLastPage.Visible = false;
            btnNextPage.Visible = false;
            btnPreviousPage.Visible = false;
            txtSize.Visible = false;
            lblcount.Visible = false;
            txtSize.Text = "10";
            txtSize.ReadOnly = true;
          

        
            
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCustomer add = new AddCustomer();
            add.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label3.Text = "Fetching Records from Database Please Wait";
            label3.ForeColor = Color.Blue;
            label3.BackColor = Color.White;
            label3.Visible = true;
            dataGridView1.DataSource = getdatafromDB();
            
            lblcount.Text = "Total Number of records: "+getdatafromDB().Rows.Count;
            lblcount.BackColor = Color.Red;
            lblcount.ForeColor = Color.White;
            label3.Visible = false;
            lblcount.Visible = true;
            button3.Enabled = true;
            button2.Enabled = true;
            dataGridView1.Visible = true;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            btnRefresh.Visible = true;
            lblPageSize.Visible = true;
            txtSize.Visible = true;
            btnFirstPage.Visible = true;
          //  btnLastPage.Visible = true;
            //btnNextPage.Visible = true;
           //   btnPreviousPage.Visible = true;
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
            btnRefresh.Visible = false;
            btnFirstPage.Visible = false;
            btnNextPage.Visible = false;
            btnLastPage.Visible = false;
            btnPreviousPage.Visible = false;
            txtSize.Visible = false;
            lblcount.Visible = false;
            lblPageSize.Visible = false;
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
                dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
                
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
            btnRefresh.Visible = false;
            btnFirstPage.Visible = false;
            btnNextPage.Visible = false;
            btnLastPage.Visible = false;
            btnPreviousPage.Visible = false;
            txtSize.Visible = false;
            lblcount.Visible = false;
            lblPageSize.Visible = false;
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                this.editcustid = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
                AddCustomer addc = new AddCustomer();
                if (this.editcustid.Length == 0)
                {
                    if (addc.IsValidPhone(dataGridView1.Rows[e.RowIndex].Cells["phone"].Value.ToString()))
                    {
                        this.editcustid = dataGridView1.Rows[e.RowIndex].Cells["phone"].Value.ToString();
                    }
                }
                if (this.editcustid.Length == 0)
                {
                    this.editcustid = dataGridView1.Rows[e.RowIndex].Cells["custid"].Value.ToString();
                }
                label3.Text = this.editcustid;
            }
            
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
            if (editcustid != null)
            {
            DeleteCustomer delcust = new DeleteCustomer(this.editcustid);
            delcust.Show();
            }
            else
            {
                MessageBox.Show("Please Select a Grid Row by clicking on Row Hader", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Excel file to import";
            openFileDialog1.Filter = "Excel Files(*.xls)|*.xls";
            openFileDialog1.ShowDialog();
            if  (openFileDialog1.FileName.Length > 0)
            {
                importfromExcelToDB(openFileDialog1.FileName);
            }

        }

        private void importfromExcelToDB(string filepath)
        {
            CustomerForm.Helper hl = new CustomerForm.Helper();
            if (System.IO.File.Exists(filepath) == false)
            {
                return;
            }
            DataTable dtimport = hl.dtFromExcelFile(filepath, "Contacts");
            int reccount = 0;
             string DQ = "\"";
             MySqlConnection conn = new MySqlConnection();
             label3.Text = "Importing Records to Database Please Wait...";
             label3.ForeColor = Color.Blue;
             label3.BackColor = Color.White;
             label3.Visible = true;
             try
             {
                 
                 conn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();

                 foreach (DataRow dr in dtimport.Rows)
                 {
                     string ph = dr["phone"].ToString().Replace(" ", "");
                  
                     string strmycomd = "insert into  customer(firstname,lastname,email,phone,address) " +
                                    "values (" + DQ + dr["firstname"] + DQ + ", " + DQ + dr["lastname"] + DQ + ", " + DQ + dr["email"] + DQ +
                                    ", " + DQ + ph + DQ + ", " + DQ + dr["address"] + DQ + " );";
                     MySqlCommand mycmd = new MySqlCommand(strmycomd, conn);

                     conn.Open();
                     mycmd.ExecuteNonQuery();
                     conn.Close();
                     reccount++;
                 }
             }
             catch (MySqlException mex)
             {
                 MessageBox.Show("Error " + mex);
             }
             finally
             {
                 conn.Close();
             }
             label3.Text = "Importing Records  Complete..("+reccount+")";
             label3.ForeColor = Color.Blue;
             label3.BackColor = Color.White;
             label3.Visible = true;
             
        }

        public void refreshgrid()
        {

            dataGridView1.DataSource = getdatafromDB();
            dataGridView1.Refresh();
        }


        private void AddCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("event fired!!");
            Form1 fm1 = new Form1();
            fm1.refreshgrid();
        }
        private void DeleteCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  MessageBox.Show("indise refs");
            refreshgrid();
        }

        private void Form1_GotFocus(object sender, EventArgs e)
        {
            MessageBox.Show("got focus");
            refreshgrid();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            refreshgrid();
            lblcount.Text = "Total Number of records: " + getdatafromDB().Rows.Count;
        }

        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Refresh", btnRefresh);
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {

            clickNumbers = 0;
            label3.Text = "Fetching Records from Database Please Wait";
            label3.ForeColor = Color.Blue;
            label3.BackColor = Color.White;
            label3.Visible = true;
            int pgsz = -1;
            if (txtSize.Text.Length > 0 && IsValidNumber(txtSize.Text))
            {
                pgsz = Int32.Parse(txtSize.Text);
            }
            else
            {
                MessageBox.Show("Please Enter Valid Page number Size", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (pgsz == -1)
            {
                return;
            }
            dataGridView1.DataSource = getdatafromDBPageSize(pgsz, 1);
            label3.Visible = false;
            button3.Enabled = true;
            button2.Enabled = true;
            btnLastPage.Visible = true;
            btnNextPage.Visible = true;
            btnPreviousPage.Visible = true;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            btnRefresh.Visible = true;

        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            this.clickNumbers++;
            label3.Text = "Fetching Records from Database Please Wait";
            label3.ForeColor = Color.Blue;
            label3.BackColor = Color.White;
            label3.Visible = true;
            int pgsz = -1;
            if (txtSize.Text.Length > 0 && IsValidNumber(txtSize.Text))
            {
                pgsz = Int32.Parse(txtSize.Text);
            }
            else
            {
                MessageBox.Show("Please Enter Valid Page number Size", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (pgsz == -1)
            {
                return;
            }
            dataGridView1.DataSource = getdatafromDBPageSize(pgsz, clickNumbers+1);
            label3.Visible = false;
            button3.Enabled = true;
            button2.Enabled = true;
            dataGridView1.Visible = true;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            btnRefresh.Visible = true;

        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if ((clickNumbers > 1) == false)
            {
                return;
            }
            this.clickNumbers--;
            label3.Text = "Fetching Records from Database Please Wait";
            label3.ForeColor = Color.Blue;
            label3.BackColor = Color.White;
            label3.Visible = true;
            int pgsz = -1;
            if (txtSize.Text.Length > 0 && IsValidNumber(txtSize.Text))
            {
                pgsz = Int32.Parse(txtSize.Text);
            }
            else
            {
                MessageBox.Show("Please Enter Valid Page number Size", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (pgsz == -1)
            {
                return;
            }
            dataGridView1.DataSource = getdatafromDBPageSize(pgsz, clickNumbers + 1);
            label3.Visible = false;
            button3.Enabled = true;
            button2.Enabled = true;
            dataGridView1.Visible = true;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            btnRefresh.Visible = true;
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            
            label3.Text = "Fetching Records from Database Please Wait";
            label3.ForeColor = Color.Blue;
            label3.BackColor = Color.White;
            label3.Visible = true;
            int pgsz = -1;
            if (txtSize.Text.Length > 0 && IsValidNumber(txtSize.Text))
            {
                pgsz = Int32.Parse(txtSize.Text);
            }
            else
            {
                MessageBox.Show("Please Enter Valid Page number Size", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (pgsz == -1)
            {
                return;
            }
            int rowcount = getdatafromDB().Rows.Count;
            int lastpg = rowcount / pgsz;
            clickNumbers = lastpg;
            dataGridView1.DataSource = getdatafromDBPageSize(pgsz, lastpg);
            label3.Visible = false;
            button3.Enabled = true;
            button2.Enabled = true;
            dataGridView1.Visible = true;
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            btnRefresh.Visible = true;
        }

        public DataTable getdatafromDBPageSize(int pagesize, int pagenum)
        {
            string odbccmdtext = "";
            int PreviousPageOffSet = -1;
            try
            {
                DataTable dtble = new DataTable();
                MySqlConnection oconn = new MySqlConnection();
                oconn.ConnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
                if (pagenum == 1)
                {
                    odbccmdtext = "Select * from customer ORDER BY custid LIMIT " + (pagesize) + " ;";
                }
                else
                {
                    PreviousPageOffSet = (pagenum - 1) * pagesize;
                    odbccmdtext = "Select * from customer ORDER BY custid LIMIT " + PreviousPageOffSet + " , " + pagesize + " ;";
                }
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


        private int  CalculateTotalPages(DataTable dt, int PgSize)
        {
            int rowCount = dt.Rows.Count;
            int TotalPage = rowCount / PgSize;
            // if any row left after calculated pages, add one more page 
            if (rowCount % PgSize > 0)
                TotalPage += 1;
            return TotalPage;
        }


        private bool IsValidNumber(string strphone)
        {
            bool valid = false;
            char[] numchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            int charmatchcount = 0;
            if (strphone.Length > 0 )
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox t = sender as TextBox;
                if (t != null)
                {
                    //say you want to do a search when user types 3 or more chars
                    if (t.Text.Length >= 2)
                    {
                        //SuggestStrings will have the logic to return array of strings either from cache/db
                        string[] arr = SuggestStrings(t.Text);
                        if (arr.Length > 0)
                        {
                            // string[] arr = new string[] { "Nikunj", "Nirav", "Nitesh" };
                            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
                            textBox2.AutoCompleteCustomSource = acsc;
                            acsc.AddRange(arr);
                            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public string[] SuggestStrings(string inptstring)
        {
            int i = 0;
            
            DataTable dt = getdatafromDB();
            string[] retstrins;
            retstrins = new string[1];
           foreach (DataRow drow in dt.Rows)
           {
               if (drow[comboBox1.SelectedItem.ToString()].ToString().Contains(inptstring))
               {
                   retstrins[i] = drow[comboBox1.SelectedItem.ToString()].ToString();
                   i++;
                   Array.Resize(ref retstrins, i+1);
               }
           }
           Array.Resize(ref retstrins, retstrins.Length - 1);
            return retstrins;
        }
    }
}

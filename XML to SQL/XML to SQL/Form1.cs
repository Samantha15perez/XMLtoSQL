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
using System.Configuration;
using System.IO;

namespace XML_to_SQL
{
    public partial class Form1 : Form
    {
        DataSet dsApps = new DataSet();
        SqlConnection cnSQL = new SqlConnection(@"Server=PL8\MTCDB;Database=SandboxLoginTest;Trusted_Connection=True;");
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XML files |*.xml|All files |*.*";
            openFileDialog1.InitialDirectory = @"C:\Users\Cyberadmin\Desktop\";
            openFileDialog1.Title = "Please select a file to load data from.";
            openFileDialog1.FileName = "MOCK_DATA.xml"; //to make things faster in testing

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    dsApps.ReadXml(openFileDialog1.FileName);
                    dsApps.Tables[0].Columns.Add("id");
                    dataGridView1.DataSource = dsApps.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...");
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
             SqlCommand cmdApps = new SqlCommand("InsertApplicant", cnSQL);
             cmdApps.CommandType = CommandType.StoredProcedure;

            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    cnSQL.Open();
                    statusStrip1.ForeColor = Color.Green;
                    toolStripStatusLabel1.Text = "Success.";

                    foreach (DataGridViewRow drApp in dataGridView1.Rows)
                    {
                        if (drApp.Cells["SSN"].Value.ToString() != null)
                        { 
                            //do the thing

                        

                        cmdApps.Parameters.AddWithValue("@FirstName", drApp.Cells["first_name"].Value);
                        cmdApps.Parameters.AddWithValue("@LastName", drApp.Cells["last_name"].Value);
                        cmdApps.Parameters.AddWithValue("@SSN", drApp.Cells["ssn"].Value);
                        cmdApps.Parameters.AddWithValue("@Email", drApp.Cells["email"].Value);
                        cmdApps.Parameters.AddWithValue("@Gender", drApp.Cells["gender"].Value);
                        cmdApps.Parameters.AddWithValue("@AppID", 0);
                        cmdApps.Parameters["@AppID"].Direction = ParameterDirection.Output;
                        cmdApps.ExecuteNonQuery();
                        drApp.Cells["id"].Value = cmdApps.Parameters["@AppID"].Value;
                        cmdApps.Parameters.Clear();


                        }
            
                    }

                    
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error ...");
                statusStrip1.ForeColor = Color.Maroon;
                //statusStrip1.BackColor = Color.Red;
                toolStripStatusLabel1.Text = (ex.Message);
            }
            finally
            {
                cnSQL.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";

            if (File.Exists(@"C:\Users\Cyberadmin\Desktop\MOCK_DATA.xml"))
            {
                dsApps.ReadXml(@"C:\Users\Cyberadmin\Desktop\MOCK_DATA.xml");
                dsApps.Tables[0].Columns.Add("id");
                dataGridView1.DataSource = dsApps.Tables[0];

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Applicants", cnSQL);

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}

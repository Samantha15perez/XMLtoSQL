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

namespace XML_to_SQL
{
    public partial class Form1 : Form
    {
        DataSet dsApps = new DataSet();
        SqlConnection cnSQL = new SqlConnection("Server=COMEAU-WIN7;Database=Sandbox;Trusted_Connection=True;");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

                    foreach (DataGridViewRow drApp in dataGridView1.Rows)
                    {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...");
            }
            finally
            {
                cnSQL.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReadWriteXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataSet DS;
        private void btnCreateXML_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a datatable to store XML data
                DataTable DT = new DataTable();
                DT.Columns.Add("FirstName");
                DT.Columns.Add("LastName");
                DT.Columns.Add("Gender");
                DT.Columns.Add("Age");
                DT.Rows.Add(new object[] { "Nick", "Solimine", "Male", 22 });
                DT.Rows.Add(new object[] { "Mark", "Taylor", "Male", 32 });
                DT.Rows.Add(new object[] { "Alice", "Warden", "Female", 20 });

                //Create a dataset
                DS = new DataSet();

                //Add datatable to this dataset
                DS.Tables.Add(DT);

                //Write dataset to XML file
                DS.WriteXml(txtXMLFilePath.Text);

                MessageBox.Show("XML data written successfully to "+txtXMLFilePath.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception: "+ex.Message);
            }
        }

        private void btnReadXML_Click(object sender, EventArgs e)
        {
            try
            {
                //Initialize new Dataset
                DS = new DataSet();

                //Read XML data from file
                DS.ReadXml(txtXMLFilePath.Text);

                //Fill grid with XML data
                dataGridView1.DataSource = DS.Tables[0];
                dataGridView1.Refresh();

                MessageBox.Show("XML data read successful");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            try
            {
                //Write dataset to XML file
                DS.WriteXml(txtXMLFilePath.Text);
                MessageBox.Show("XML data saved successfully to " + txtXMLFilePath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            //Clear Grid
            dataGridView1.DataSource = null;
        }
    }
}

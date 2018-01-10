using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace ReadWriteXML
{
    public partial class Form1 : Form
    {

        public bool test;
        public Form1()
        {
            InitializeComponent();
        }

        //DataSet DS;
        DataSet DS = new DataSet();
        DataSet DS2 = new DataSet();
        DataSet toSend = new DataSet();
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
                //DS = new DataSet();

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
        private void button1_Click(object sender, EventArgs e)
        {
            //string oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.60.10.198)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = XE)));" + "User Id= ;Password=;";
            //protected void Page_Load(object sender, EventArgs e) { }
            //protected void btn_Click(object sender, EventArgs e)
           // OracleConnection conn = new OracleConnection(oradb);
           
            try
            {
                


                mysqlDatabase db = new mysqlDatabase();

                string query = "select * from uzytkownicy order by id";
                DataSet DS = new DataSet();
                DS = db.getDataSet(query);
                dataGridView1.DataSource = DS.Tables[0];
                //conn.Open();

                //OracleCommand cmd = new OracleCommand("select * from contacts;", conn);
                //OracleCommand cmd = new OracleCommand("select * from uzytk;", conn);
                //OracleDataAdapter oda = new OracleDataAdapter(cmd);
                
                

                //oda.Fill(this.DS);

                if (DS.Tables.Count > 0)

                {

                    dataGridView1.DataSource = DS.Tables[0].DefaultView;

                }
                try
                {
                    //Write dataset to XML file

                    /*Stream stream = File.Open(@"c:\Uczelnia\repos\BasicDataSetSerialization.xml", FileMode.Create);
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(stream, DS);
                    stream.Close();

                    Stream stream2 = File.Open(@"c:\Uczelnia\repos\BasicDataSetSerialization1.xml", FileMode.Open);
                    bf = new BinaryFormatter();

                    DS2 = (DataSet)bf.Deserialize(stream2);
                    dataGridView2.DataSource = DS2.Tables[0].DefaultView;
                    DS.WriteXml(txtXMLFilePath.Text);
                    MessageBox.Show("XML data saved successfully to " + txtXMLFilePath.Text);


                    MemoryStream streamMemory = new MemoryStream();
                    XmlTextWriter writer = new XmlTextWriter(streamMemory, Encoding.UTF8);
                    writer.WriteStartDocument();
                    DS.WriteXml(streamMemory);
                    */
                    /////////////////////////////////////////

                    string serverAddress = "127.0.0.1";
                    int serverPort = 5000;
                    TcpClient client = new TcpClient();
                    client.Connect(serverAddress, serverPort);
                    //Stream streamToClient = client.GetStream();
                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream();
                    bf.Serialize(ms, DS);
                    ms.ToArray();
                    
                    ms.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    DataSet obj = (DataSet)bf.Deserialize(ms);
                    dataGridView2.DataSource = obj.Tables[0].DefaultView;
                    dataGridView2.Refresh();


                    //xs.Serialize(streamToClient, DS);
                    ms.Close();
                    //streamToClient.Close();
                    client.Close();

                    //

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
                //MessageBox.Show("Connected to Oracle" + conn.ServerVersion);
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }

            //conn.Close();
            //conn.Dispose();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connetionString = null;
            
           
            OracleDataAdapter adpter = new OracleDataAdapter();
            DataSet ds = new DataSet();
            XmlReader xmlFile;
            string sql = null;

            int product_ID = 0;
            string Product_Name = null; 
            double product_Price = 0;
            
            /*
            int LUZYTKREF = 0;
            int LGRUPAUZYTKREF = 0;
            string SLOGIN;
            string SPASS;
            string SIMIE;
            string SNAZWISKO;
            int LWPISALREF = 0;
            string CZASMOD;
            int BYTYP = 0;
            int NSTANOWISKO = 0;
            string NUZYTKNR;
            string SPRAWADOSTEPU;
            int LLOKALIZACJAREF = 0;
            int NSTATUSAKTYWNOSCI = 0;
            string GUID;
            string LPSREF;
            int BZEWNETRZNY = 0;
            string    IDENTIFICATIONINFO;
            int BKASJERSCO = 0;
            int BSTATUSWYSLANIA = 0;
            string PASSWORDMODIFICATIONTIME;
            int LOCAL_USER = 0;
            */

            string contact_id, last_name, first_name, address, city, state, zip_code;

            connetionString = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.60.10.198)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = XE)));" + "User Id= ;Password=;";

            //connection = new OracleConnection(connetionString);

            xmlFile = XmlReader.Create(txtXMLFilePath.Text, new XmlReaderSettings());
            ds.ReadXml(xmlFile);
            int i = 0;
 
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                //product_ID = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                //Product_Name = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                //product_Price = Convert.ToDouble(ds.Tables[0].Rows[i].ItemArray[2]);

                contact_id = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                last_name = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                first_name = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                address = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                city = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                state = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                zip_code = ds.Tables[0].Rows[i].ItemArray[6].ToString();
                //sql = "insert into UZYTK_MR values(" + LUZYTKREF + "," + LGRUPAUZYTKREF + ",'" + SLOGIN + "','" + SPASS + "','" + SIMIE + "','" + SNAZWISKO + "'," + LWPISALREF + "," + CZASMOD + "," + BYTYP + "," + NSTANOWISKO + "," + NUZYTKNR + ",'" + SPRAWADOSTEPU + "'," + LLOKALIZACJAREF + "," + NSTATUSAKTYWNOSCI + ",'" + GUID + "'," + LPSREF + "," + BZEWNETRZNY + ",'" + IDENTIFICATIONINFO + "'," + BKASJERSCO + "," + BSTATUSWYSLANIA + "," + PASSWORDMODIFICATIONTIME + "," + LOCAL_USER + ")";
                sql = @"INSERT INTO contacts (contact_id, last_name, first_name, address, city, state, zip_code) VALUES(" + contact_id + ",'" + last_name + "','" + first_name + "','" + address + "','" + city + "','" + state + "','" + zip_code + "'" +")"; 
                //command = new OracleCommand(sql, connection);
                //command.CommandText = "INSERT INTO contacts (contact_id, last_name, first_name, address, city, state, zip_code) VALUES(" + contact_id + ",'" + last_name + "','" + first_name + "','" + address + "','" + city + "','" + state + "','" + zip_code + "'" + ");";
                richTextBox1.Text = sql;
                //adpter.InsertCommand = command;
                //adpter.InsertCommand.ExecuteNonQuery();

                try
                {
                    OracleConnection connection = new OracleConnection(connetionString);
                    OracleCommand command = new OracleCommand(sql, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    this.test = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("" + ex);
                    this.test = false;
                    break;
                }





            }


            if (test==false)
            {

            }
            else
            {
                MessageBox.Show("Done .. ");
            }
            


            
        }
    }
}

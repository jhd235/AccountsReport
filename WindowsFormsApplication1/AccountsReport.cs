using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            this.Text = "Баланс"; 
        }
        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<style>table, th, td {border: 1px solid black;}</style><table style =\"width:100%\">";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        dt.Columns[i].ColumnName = "ФИО";
                        break;
                    case 2:
                        dt.Columns[i].ColumnName = "Телефон";
                        break;
                    case 3:
                        dt.Columns[i].ColumnName = "Баланс";
                        break;
                    default:

                        break;
                };
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";}
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myForm = new Form();
            
            
            var webbrowser = new WebBrowser();
            myForm.Controls.Add(webbrowser);
            webbrowser.Dock = DockStyle.Fill;
            string connetionString = null;
            SqlConnection connection;
            string sql = null;
            string[] lines = System.IO.File.ReadAllLines(@"conf.txt");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                //Console.WriteLine("\t" + line);
            //    var result = MessageBox.Show(lines[0]);

            }

           //connetionString = "Data Source=USERPC\\SQLEXPRESS;Initial Catalog=GameClass;User ID=manager;Password=zxc";
            connetionString = "Data Source=" + lines[0] + ";Initial Catalog=GameClass;User ID=" + lines[1] + ";Password=" + lines[2] + "";
            //var resultt = MessageBox.Show(connetionString);
            sql = "SELECT [name], [email], [phone], [balance] FROM [Accounts] ORDER BY [balance] DESC";
            connection = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
         
            da.Fill(dt);
            
            webbrowser.DocumentText = ConvertDataTableToHTML(dt);
            //var res = MessageBox.Show(dt.Rows[0][1].ToString()); 
            //var res = MessageBox.Show(dt.Columns[0].ColumnName);
            myForm.Show();
            myForm.WindowState = FormWindowState.Maximized;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dt_extract()
        {
            
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var About = new Form();
            About.Text = "О программе";
            var lblAbout = new Label();
            lblAbout.Text = "capslock.kz";
            About.Controls.Add(lblAbout);
            About.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmSettings = new Form();
            var txtServer = new TextBox();
            frmSettings.Controls.Add(txtServer);
            frmSettings.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dt_extract();
        }
    }
}
            
       



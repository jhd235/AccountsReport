using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Timers;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        WebBrowser webbrowser = new WebBrowser();
        string[] lines = System.IO.File.ReadAllLines(@"conf.txt");
        public Form1()
        {
            InitializeComponent();
            this.Text = "Баланс"; 
        }
        //This is a scheduler
        public static void Scheduler()
        {
        System.Timers.Timer aTimer = new System.Timers.Timer();
        aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
        aTimer.Interval=5000;
        aTimer.Enabled=true;

        Console.WriteLine("Press \'q\' to quit the sample.");
        while(Console.Read()!='q');
}
    
 // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MessageBox.Show("Hello World!");
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

        private void report()
        {
            var myForm = new Form();
            //var topPanel = new Panel();
            //topPanel.Size = new Size(10,10); 
            //topPanel.Dock = DockStyle.Top;
            //myForm.Controls.Add(topPanel);
            //var Menu = new MenuStrip();
            //Menu.Items.Add("СохранитьHTML");
            //myForm.Controls.Add(Menu);
            /*var item = new System.Windows.Forms.ToolStripMenuItem()
            {
                Name = "Test",
                Text = "Test"
            };
            
            
            Menu.Items.Add(item);
            var btnSaveHTML = new Button();
            btnSaveHTML.Name = "HTML";
            btnSaveHTML.Text = "HTML";
            myForm.Controls.Add(btnSaveHTML);*/
            myForm.Controls.Add(webbrowser);
            webbrowser.Dock = DockStyle.Top;
            string connetionString = null;
            SqlConnection connection;
            string sql = null;
            
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
            var sfDialog = new SaveFileDialog();
            sfDialog.Filter = "HTML Files | *.html";
            sfDialog.DefaultExt = "html";
            //File.WriteAllText(@"file.html", ConvertDataTableToHTML(dt), Encoding.Unicode); 
            sfDialog.ShowDialog();
            File.WriteAllText(sfDialog.FileName, ConvertDataTableToHTML(dt), Encoding.Unicode); 
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

        private void наПочтуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string your_id = "gameclass.infinity@gmail.com";
            string your_password = "X@[MHb_uB#3mekk*";
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(your_id, your_password),
                    Timeout = 10000,
                };
                //MailMessage mm = new MailMessage(your_id, "gameclass.infinity@gmail.com", "subject", "body");
                MailMessage mm = new MailMessage(your_id, "gameclass.infinity@gmail.com", "subject", ConvertDataTableToHTML(dt));
                mm.IsBodyHtml = true;
                client.Send(mm);
                //Console.WriteLine("Email Sent");
            }
            catch (Exception error)
            {
                //Console.WriteLine("Could not end email\n\n" + error.ToString());
            }
        }

        

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report();
            
        }

        
}
}
            
       



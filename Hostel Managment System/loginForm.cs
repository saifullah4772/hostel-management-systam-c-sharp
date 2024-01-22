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
using System.IO;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Hostel_Managment_System
{
    public partial class loginForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public loginForm()
        {
            InitializeComponent();
        }
        
        private void loginForm_Load(object sender, EventArgs e)
        {
            
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        public bool createSession(int user_id,string email)
        {
            try
            {
                conn.Open();
                string startTime = DateTime.Now.ToString();
                string endTime = DateTime.Now.AddHours(12).ToString();
                Console.WriteLine(startTime);
                Console.WriteLine(endTime);
                string query = "insert into sessions (user_id,email,start_time,end_time) values ('"+user_id+"','"+email+"','"+startTime+"','"+endTime+"')";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void textBox1_TabIndexChanged(object sender, EventArgs e)
        {
            txtUser.Text = "test";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            lblEmailRequired.Visible = false;
            if (txtUser.Text == "Enter Email")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Black;
            }
            else
            {
                return;
            }
        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            lblPassRequired.Visible = false;
            if(txtpassword.Text == "Enter Password")
            {
                txtpassword.Text = "";
                txtpassword.ForeColor = Color.Black;
            }
            else
            {
                return;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if(txtUser.Text == "") {
                txtUser.Text = "Enter Email";
                txtUser.ForeColor = Color.Silver;
            }
            else
            {
                return;
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (txtpassword.Text == "")
            {
                txtpassword.Text = "Enter Password";
                txtpassword.ForeColor = Color.Silver;
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader data = null;
            try
            {
                if (txtUser.Text == "Enter Email")
                {
                    if(txtpassword.Text == "Enter Password")
                    {
                        lblEmailRequired.Visible = true;
                        lblPassRequired.Visible = true;
                    }
                    else
                    {
                        lblEmailRequired.Visible = true;
                    }
                }else if (txtpassword.Text == "Enter Password") { 
                    lblPassRequired.Visible = true;
                }
                else
                {
                    conn.Open();
                    string email = "";
                    string name = "";
                    string password = "";
                    int id = 0;
                    string query = "select * from users where email='" + txtUser.Text + "'";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    data = cmd.ExecuteReader();
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            id = data.GetInt32(0);
                            name = data.GetString(1);
                            email = data.GetString(2);
                            password = data.GetString(3);
                            break;
                        }
                    }
                    else
                    {
                        lblMessage.Visible = true;
                    }
                    data.Close();
                    conn.Close();
                    if(txtpassword.Text != password)
                    {
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        
                        bool isSessionCreated =  createSession(id,email);
                        if (isSessionCreated)
                        {
                            mainForm mainForm = new mainForm();
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            lblMessage.Visible = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        bool isShowPass = false;
        private void lblEye_Click(object sender, EventArgs e)
        {
            if(isShowPass)
            {
                txtpassword.UseSystemPasswordChar = false;
                lblEye.Image = Properties.Resources.hidden;

            }
            else
            {
                txtpassword.UseSystemPasswordChar= true;
                lblEye.Image = Properties.Resources.view;
            }
            isShowPass = !isShowPass;
        }
    }
}

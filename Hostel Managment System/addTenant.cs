using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hostel_Managment_System
{
    public partial class addTenant : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public addTenant()
        {
            InitializeComponent();
        }
        int totalrooms = 0;
        int living = 0;
        public int isFull(int roomNo)
        {
            try
            {
                SqlDataReader data = null;
                conn.Open();
                string query = "select room_space,living from rooms where room_number=" + roomNo + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())                    
                    {
                        totalrooms = data.GetInt32(0);
                        living = data.GetInt32(1);
                        break;
                    }
                }
                else
                {
                    conn.Close();
                    return 3;
                }
                data.Close();
                conn.Close();
                if (living == totalrooms)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 2;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAge.Text == "" || txtCNIC.Text == "" || txtCollege.Text == "" || txtName.Text == "" || txtFName.Text == "" || txtRoomNumber.Text == "")
                {
                    MessageBox.Show("All Fields are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtRoomNumber.Text,"[^0-9]"))
                {
                    MessageBox.Show("Room Number must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtAge.Text, "[^0-9]"))

                {
                    MessageBox.Show("Age must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                mainForm mainForm = new mainForm();
                string name = txtName.Text;
                string fname = txtFName.Text;
                string cnic = txtCNIC.Text;
                int age = Convert.ToInt32(txtAge.Text);
                int roomNo = Convert.ToInt32(txtRoomNumber.Text);
                string college = txtCollege.Text;
                if (isFull(roomNo) == 0)
                {
                    conn.Open();
                    string query = "insert into tenants (name,father_name,cnic,age,room_number,college) values ('" + name + "','" + fname + "','" + cnic + "','" + age + "','" + roomNo + "','" + college + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    string query2 = "update rooms set living = living + 1 where room_number = " + roomNo + "";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }
                else if(isFull(roomNo) == 2)
                {
                    MessageBox.Show("Error in Adding Tenant", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } else if(isFull(roomNo) == 3)
                {
                    MessageBox.Show("No Room Number found on this number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Room is Full","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                
                
                mainForm.setCurrentForm("hostel");
                mainForm.setFormCount(3);
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.setCurrentForm("hostel");
            mainForm.setFormCount(3);
            mainForm.Show();
            this.Hide();
        }
    }
}

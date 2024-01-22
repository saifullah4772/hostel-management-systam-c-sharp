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
    public partial class addStaff : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public addStaff()
        {
            InitializeComponent();
        }

        private void addStaff_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.setCurrentForm("staff");
            mainForm.setFormCount(4);
            mainForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSalary.Text == "" || txtPhone.Text == "" || txtName.Text == "" || txtJob.Text == "")
                {
                    MessageBox.Show("All Fields are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, "[^0-9]"))
                {
                    MessageBox.Show("Phone Number must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtSalary.Text, "[^0-9]"))

                {
                    MessageBox.Show("Salary must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                conn.Open();
                mainForm mainForm = new mainForm();
                string name = txtName.Text;
                string job = txtJob.Text;
                string phone = txtPhone.Text;
                int salary = Convert.ToInt32(txtSalary.Text);
                DateTime start = DateTime.Parse(dtpStart.Text);
                string start_time = start.ToString("hh:mm tt");
                DateTime end = DateTime.Parse(dtpEnd.Text);
                string end_time = end.ToString("hh:mm tt");
                string query = "insert into staff (name,job,phone,salary,start_time,end_time) values ('" + name + "','" + job + "','" + phone + "','" + salary + "','" + start_time + "','" + end_time + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                mainForm.setCurrentForm("staff");
                mainForm.setFormCount(4);
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addStaff_Load_1(object sender, EventArgs e)
        {

        }
    }
}

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

namespace Hostel_Managment_System
{
    public partial class addRoomForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public addRoomForm()
        {
            InitializeComponent();
        }
        
        private void addRoomForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtRoomSpace.Text == "" || txtRoomSpace.Text == "" || txtRent.Text == "")
                {
                    MessageBox.Show("All Fields are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtRoomNo.Text, "[^0-9]"))
                {
                    MessageBox.Show("Room Number must be in Number","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if(System.Text.RegularExpressions.Regex.IsMatch(txtRoomSpace.Text, "[^0-9]"))
                
                {
                    MessageBox.Show("Room Space must be in Number","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtRent.Text, "[^0-9]"))

                {
                    MessageBox.Show("Rent must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                conn.Open();
                mainForm mainForm = new mainForm();
                int roomNo = Convert.ToInt32(txtRoomNo.Text);
                int roomSpace = Convert.ToInt32(txtRoomSpace.Text);
                int rent = Convert.ToInt32(txtRent.Text);
                string query = "insert into rooms (room_number,room_space,rent) values ('"+roomNo+ "','"+roomSpace+ "','"+rent+ "')";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                mainForm.setCurrentForm("rooms");
                mainForm.setFormCount(2);
                mainForm.Show();
                this.Hide();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            mainForm mainForm = new mainForm();
            mainForm.setCurrentForm("rooms");
            mainForm.setFormCount(2);
            mainForm.Show();
            this.Hide();
        }
    }
}

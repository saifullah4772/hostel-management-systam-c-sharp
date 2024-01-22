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
    public partial class hostelForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public void loadTenants()
        {
            try
            {
                conn.Open();
                string query = "select * from tenants";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                hostelTable.DataSource = dt;
                hostelTable.Columns[0].HeaderText = "ID";
                hostelTable.Columns[1].HeaderText = "Name";
                hostelTable.Columns[2].HeaderText = "Father Name";
                hostelTable.Columns[3].HeaderText = "CNIC";
                hostelTable.Columns[4].HeaderText = "Age";
                hostelTable.Columns[5].HeaderText = "Room Number";
                hostelTable.Columns[6].HeaderText = "College";
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int id = 0;
        int updateRowShow = 0;
        public void loadRow(int rowNumber)
        {
            try
            {
                updateRowShow = rowNumber;
                if (hostelTable.Rows.Count > 1)
                {
                    DataGridViewRow firstRow = hostelTable.Rows[rowNumber];
                    id = Convert.ToInt32(firstRow.Cells[0].Value.ToString());
                    txtName.Text = firstRow.Cells[1].Value.ToString();
                    txtFName.Text = firstRow.Cells[2].Value.ToString();
                    txtCNIC.Text = firstRow.Cells[3].Value.ToString();
                    txtAge.Text = firstRow.Cells[4].Value.ToString();
                    txtRoomNumber.Text = firstRow.Cells[5].Value.ToString();
                    txtCollege.Text = firstRow.Cells[6].Value.ToString();
                }
            }catch(Exception ex)
            {
                return;
            }
        }
            public hostelForm()
        {
            InitializeComponent();
            loadTenants();
            loadRow(0);
        }
        bool isUpdating = false;
        public void setReadOnly(bool readOnly)
        {
            txtName.ReadOnly = readOnly;
            txtFName.ReadOnly = readOnly;
            txtCNIC.ReadOnly = readOnly;
            txtRoomNumber.ReadOnly = readOnly;
            txtCollege.ReadOnly = readOnly;
            txtAge.ReadOnly = readOnly;
            
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (isUpdating)
            {
                isUpdating = false;
                btnUpdate.Enabled = false;
                btnUpdate.BackColor = SystemColors.ControlLight;
                lblEdit.Image = Hostel_Managment_System.Properties.Resources.editing;
            }
            else
            {
                isUpdating = true;
                btnUpdate.Enabled = true;
                btnUpdate.BackColor = Color.DeepSkyBlue;
                lblEdit.Image = Hostel_Managment_System.Properties.Resources.close_dark;
            }
            setReadOnly(!isUpdating);
        }

        private void hostelForm_Load(object sender, EventArgs e)
        {
            btnUpdate.BackColor = SystemColors.ControlLight;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAge.Text == "" || txtCNIC.Text == "" || txtCollege.Text == "" || txtName.Text == "" || txtFName.Text == "" || txtRoomNumber.Text == "")
                {
                    MessageBox.Show("All Fields are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtRoomNumber.Text, "[^0-9]"))
                {
                    MessageBox.Show("Room Number must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtAge.Text, "[^0-9]"))

                {
                    MessageBox.Show("Age must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                conn.Open();
                mainForm mainForm = new mainForm();
                string name = txtName.Text;
                string fname = txtFName.Text;
                string cnic = txtCNIC.Text;
                int age = Convert.ToInt32(txtAge.Text);
                int roomNo = Convert.ToInt32(txtRoomNumber.Text);
                string college = txtCollege.Text;
                string query = "update tenants set name='" + name + "',father_name='" + fname + "',cnic='" + cnic + "',age='" + age + "',room_number='" + roomNo + "',college='" + college + "' where id = " + id + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                loadTenants();
                setReadOnly(true);
                loadRow(updateRowShow);
                hostelTable.Rows[updateRowShow].Selected = true;
                lblEdit.Image = Hostel_Managment_System.Properties.Resources.editing;
                btnUpdate.Enabled = false;
                btnUpdate.BackColor = SystemColors.ControlLight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hostelTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            loadRow(rowIndex);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "delete from tenants where id = " + id + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                loadTenants();
                loadRow(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    public partial class staffForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public void loadStaff()
        {
            try
            {
                conn.Open();
                string query = "select * from staff";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                staffTable.DataSource = dt;
                staffTable.Columns[0].HeaderText = "ID";
                staffTable.Columns[1].HeaderText = "Name";
                staffTable.Columns[2].HeaderText = "Job Position";
                staffTable.Columns[3].HeaderText = "Phone";
                staffTable.Columns[4].HeaderText = "Salary";
                staffTable.Columns[5].HeaderText = "Start Time";
                staffTable.Columns[6].HeaderText = "End Time";
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
                if (staffTable.Rows.Count > 1)
                {
                    DataGridViewRow firstRow = staffTable.Rows[rowNumber];
                    id = Convert.ToInt32(firstRow.Cells[0].Value.ToString());
                    txtName.Text = firstRow.Cells[1].Value.ToString();
                    txtJob.Text = firstRow.Cells[2].Value.ToString();
                    txtPhone.Text = firstRow.Cells[3].Value.ToString();
                    txtSalary.Text = firstRow.Cells[4].Value.ToString();
                    txtStart.Text = firstRow.Cells[5].Value.ToString();
                    txtEnd.Text = firstRow.Cells[6].Value.ToString();
                }
            }catch(Exception ex)
            {
                return;
            }
        }
        public staffForm()
        {
            InitializeComponent();
            loadStaff();
            loadRow(0);
        }
        bool isUpdating = false;
        public void setReadOnly(bool readOnly)
        {
            txtName.ReadOnly = readOnly;
            txtJob.ReadOnly = readOnly;
            txtPhone.ReadOnly = readOnly;
            txtStart.ReadOnly = readOnly;
            txtEnd.ReadOnly = readOnly;
            txtSalary.ReadOnly = readOnly;

        }
        private void staffForm_Load(object sender, EventArgs e)
        {
            btnUpdate.BackColor = SystemColors.ControlLight;
        }

        private void lblEdit_Click(object sender, EventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void staffTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            loadRow(rowIndex);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtPhone.Text == "" || txtJob.Text == "" || txtSalary.Text == "")
                {
                    MessageBox.Show("All Fields are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtSalary.Text, "[^0-9]"))
                {
                    MessageBox.Show("Salary must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, "[^0-9]"))

                {
                    MessageBox.Show("Phone must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                conn.Open();
                mainForm mainForm = new mainForm();
                string name = txtName.Text;
                string job = txtJob.Text;
                string phone = txtPhone.Text;
                int salary = Convert.ToInt32(txtSalary.Text);
                string start_time = txtStart.Text;
                string end_time = txtEnd.Text;
                string query = "update staff set name='" + name + "',job='" + job + "',phone='" + phone + "',salary='" + salary + "',start_time='" + start_time + "',end_time='" + end_time + "' where id = " + id + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                loadStaff();
                setReadOnly(true);
                loadRow(updateRowShow);
                staffTable.Rows[updateRowShow].Selected = true;
                lblEdit.Image = Hostel_Managment_System.Properties.Resources.editing;
                btnUpdate.Enabled = false;
                btnUpdate.BackColor = SystemColors.ControlLight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "delete from staff where id = " + id + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                loadStaff();
                loadRow(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

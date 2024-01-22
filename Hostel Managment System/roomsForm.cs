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
    public partial class roomsForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public void loadRooms()
        {
            try
            {
                conn.Open();
                string query = "select * from rooms";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);  
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                roomsTable.DataSource = dt;
                roomsTable.Columns[0].HeaderText = "ID";
                roomsTable.Columns[1].HeaderText = "Room Number";
                roomsTable.Columns[2].HeaderText = "Room Space";
                roomsTable.Columns[3].HeaderText = "Rent";
                roomsTable.Columns[4].HeaderText = "Living Persons";
                conn.Close();
            }catch(Exception ex)
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
                if (roomsTable.Rows.Count > 1)
                {
                    DataGridViewRow firstRow = roomsTable.Rows[rowNumber];
                    id = Convert.ToInt32(firstRow.Cells[0].Value.ToString());
                    txtRoomNo.Text = firstRow.Cells[1].Value.ToString();
                    txtTSpace.Text = firstRow.Cells[2].Value.ToString();
                    txtASpace.Text = firstRow.Cells[4].Value.ToString();
                    txtRent.Text = firstRow.Cells[3].Value.ToString();
                }
            } catch (Exception ex)
            {
                return;
            }
        }
        public roomsForm()
        {
            InitializeComponent();
            loadRooms();
            loadRow(0);
        }
        public void setReadOnly(bool readOnly)
        {
            txtTSpace.ReadOnly = readOnly;
            txtRent.ReadOnly = readOnly;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void publicDataLoad()
        {
            loadRooms();
        }
        bool isUpdating = false;
        private void roomsForm_Load(object sender, EventArgs e)
        {
            btnUpdate.BackColor = SystemColors.ControlLight;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addRoomForm addRoomForm = new addRoomForm();
            addRoomForm.ShowDialog();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtRoomNo.Text == "" || txtTSpace.Text == "" || txtRent.Text == "")
                {
                    MessageBox.Show("All Fields are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtRoomNo.Text, "[^0-9]"))
                {
                    MessageBox.Show("Room Number must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtTSpace.Text, "[^0-9]"))

                {
                    MessageBox.Show("Room Space must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtRent.Text, "[^0-9]"))

                {
                    MessageBox.Show("Rent must be in Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                conn.Open();
                int roomNo = Convert.ToInt32(txtRoomNo.Text);
                int roomSpace = Convert.ToInt32(txtTSpace.Text);
                int rent = Convert.ToInt32(txtRent.Text);
                string query = "update rooms set room_space='" + roomSpace + "',rent='" + rent +"' where id = "+id+"";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                loadRooms();
                setReadOnly(true);
                loadRow(updateRowShow);
                roomsTable.Rows[updateRowShow].Selected = true;
                lblEdit.Image = Hostel_Managment_System.Properties.Resources.editing;
                btnUpdate.Enabled = false;
                btnUpdate.BackColor = SystemColors.ControlLight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void roomsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            loadRow(rowIndex);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "delete from rooms where id = "+id+"";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                loadRooms();
                loadRow(0);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hostel_Managment_System
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        public string currentForm = "";
        public void setCurrentForm(string formName)
        {
            currentForm = formName;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public int formCount = 1;
        public void setFormCount(int formCountVal)
        {
            formCount = formCountVal;
        }
        public void formLoad(Object Form)
        {
            if(this.panelMain.Controls.Count > 0)
                this.panelMain.Controls.RemoveAt(0);
            Form f1 = Form as Form;
            f1.TopLevel = false;
            f1.Dock = DockStyle.Fill;
            this.panelMain.Controls.Clear();
            this.panelMain.Controls.Add(f1);
            this.panelMain.Tag = f1;
            f1.Show();

            switch (formCount)
            {
                case 1:
                    this.btnDashboard.BackColor = SystemColors.Window;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;

                case 2:
                    this.btnRoom.BackColor = SystemColors.Window;
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;

                case 3:
                    this.btnHostel.BackColor = SystemColors.Window;
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;

                case 4:
                    this.btnStaff.BackColor = SystemColors.Window;
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    break;
                default:
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;
            }
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            switch (formCount)
            {
                case 1:
                    this.btnDashboard.BackColor = SystemColors.Window;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;

                case 2:
                    this.btnRoom.BackColor = SystemColors.Window;
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;

                case 3:
                    this.btnHostel.BackColor = SystemColors.Window;
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;

                case 4:
                    this.btnStaff.BackColor = SystemColors.Window;
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    break;
                default:
                    this.btnDashboard.BackColor = Color.Transparent;
                    this.btnRoom.BackColor = Color.Transparent;
                    this.btnHostel.BackColor = Color.Transparent;
                    this.btnStaff.BackColor = Color.Transparent;
                    break;
            }
            if (currentForm == "")
            {
                formLoad(new dashboardForm());
                lblHead.Text = "Dashboard";
                btnAdd.Visible = false;
            }
            else if(currentForm == "rooms")
            {
                formLoad(new roomsForm());
                lblHead.Text = "Rooms";
                btnAdd.Visible = true;
            } else if(currentForm == "hostel")
            {
                formLoad(new hostelForm());
                lblHead.Text = "Tenants";
                btnAdd.Text = " Add Tenant";
                btnAdd.Visible = true;
            }
            else if (currentForm == "staff")
            {
                formLoad(new staffForm());
                lblHead.Text = "Staff";
                btnAdd.Text = " Add Staff";
                btnAdd.Visible = true;
            }
            else
            {
                formLoad(new dashboardForm());
                btnAdd.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            formCount = 1;
            formLoad(new dashboardForm());
            lblHead.Text = "Dashboard";
            btnAdd.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            formCount = 4;
            formLoad(new staffForm());
            lblHead.Text = "Staff";
            btnAdd.Visible = true;
            btnAdd.Text = " Add Staff";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            formCount = 2;
            formLoad(new roomsForm());
            lblHead.Text = "Rooms";
            btnAdd.Visible = true;
            btnAdd.Text = " Add Room";
        }

        private void btnHostel_Click(object sender, EventArgs e)
        {
            formCount = 3;
            formLoad(new hostelForm());
            lblHead.Text = "Tenants";
            btnAdd.Visible = true;
            btnAdd.Text = " Add Tenant";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form f2 = new loginForm();
            f2.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            if(btnAdd.Text == " Add Room")
            {
                addRoomForm addRoomForm = new addRoomForm();
                addRoomForm.ShowDialog();
                return;
            }
            if(btnAdd.Text ==" Add Tenant")
            {
                addTenant addTenant = new addTenant();
                addTenant.ShowDialog();
                return;
            }
            if(btnAdd.Text == " Add Staff")
            {
                addStaff addStaff = new addStaff();
                addStaff.ShowDialog();
                return; 
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

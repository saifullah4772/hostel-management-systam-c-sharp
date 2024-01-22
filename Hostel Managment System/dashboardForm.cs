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
    public partial class dashboardForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-TRQPASG\\SQLEXPRESS;Initial Catalog=hostel_mangement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public void loadStaff()
        {
            try
            {
                conn.Open();
                string query = "select COUNT(*) from staff";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblStaff.Text = Convert.ToString(dr.GetInt32(0));
                        break;
                    }

                }
                dr.Close();
                conn.Close();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void loadDashboardData()
        {
            try
            {
                conn.Open();
                string query = "select COUNT(*),SUM(living) from rooms";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblRoom.Text = Convert.ToString(dr.GetInt32(0) + 3);
                        lblTanent.Text = Convert.ToString(dr.GetInt32(1));
                        break;
                    }

                }
                else
                {
                    lblRoom.Text = "0";
                    lblTanent.Text = "0";
                }
                dr.Close();
                
                conn.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public dashboardForm()
        {
            InitializeComponent();
            loadStaff();
            loadDashboardData();
        }

        private void dashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}

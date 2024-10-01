using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace taskCrud
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               LoadReportingManagers();
                LoadEmployees();
            }
        }

        private void LoadReportingManagers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT RepotingManager FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                ddlReportingManager.DataSource = cmd.ExecuteReader();
                ddlReportingManager.DataTextField = "RepotingManager";
                ddlReportingManager.DataValueField = "RepotingManager";
                ddlReportingManager.DataBind();

                ddlReportingManager.Items.Insert(0, new ListItem("--Select Manager--", ""));
            }
        }

        private void LoadEmployees()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select * from Employee";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string empName = txtEmpName.Text;
            string address = txtAddress.Text;
            string repotingManager = ddlReportingManager.SelectedValue;
            string empLocation = txtEmpLocation.Text;
            bool isActive = chkIsActive.Checked;

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employee (EmployeeName, Address, RepotingManager, Location, IsActive) VALUES (@EmployeeName, @Address, @RepotingManager, @Location, @IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@EmployeeName", empName);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@RepotingManager", repotingManager);
                cmd.Parameters.AddWithValue("@Location", empLocation);
                cmd.Parameters.AddWithValue("@IsActive", isActive);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadEmployees();  
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int employeeID = Convert.ToInt32(ViewState["EmployeeID"]);
            string empName = txtEmpName.Text;
            string address = txtAddress.Text;
            string RepotingManager = ddlReportingManager.SelectedValue;
            string empLocation = txtEmpLocation.Text;
            bool isActive = chkIsActive.Checked;

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employee SET EmployeeName = @EmployeeName, Address = @Address, RepotingManager = @RepotingManager, Location = @Location, IsActive = @IsActive WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@EmployeeName", empName);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@RepotingManager", RepotingManager);
                cmd.Parameters.AddWithValue("@Location", empLocation);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadEmployees();  
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
           
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int employeeID = Convert.ToInt32(ViewState["EmployeeID"]);

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadEmployees(); 
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
          
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
             int employeeID = Convert.ToInt32(row.Cells[0].Text.Trim());


            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtEmpName.Text = reader["EmployeeName"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                    ddlReportingManager.SelectedValue = reader["RepotingManager"].ToString();
                    txtEmpLocation.Text = reader["Location"].ToString();
                    chkIsActive.Checked = Convert.ToBoolean(reader["IsActive"]);

                    ViewState["EmployeeID"] = employeeID;
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
            }
        }
    }
}
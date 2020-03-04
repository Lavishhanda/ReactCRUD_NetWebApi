using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiWithReact.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApiWithReact.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            //string x = string.Empty;
            string query = @"
                            Select EmployeeId, EmployeeName, Department, MailID,
                            convert(varchar(10),DOJ,120) as DOJ from dbo.Employees";
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDb"].ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        public string Post(Employee employee)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                               Insert into dbo.Employees (EmployeeName, Department, MailID, DOJ) values (
                                '"+ employee.EmployeeName + @"'
                                ,'" + employee.Department + @"'
                                ,'" + employee.MailID + @"'
                                ,'" + employee.DOJ + @"'
                                )";
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Added successfully";
            }
            catch
            {
                return "Failed to Add";
            }
        }

        public string Put(Employee employee)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                               Update dbo.Employees set 
                               EmployeeName = '" + employee.EmployeeName + @"'
                               ,Department = '" + employee.Department + @"'
                               ,MailId = '" + employee.MailID + @"'
                               ,DOJ = '" + employee.DOJ + @"'
                               where EmployeeID = " + employee.EmployeeId + @"                                
                                ";
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Updated successfully";
            }
            catch
            {
                return "Failed to update";
            }
        }

        public string Delete(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                               Delete from dbo.Employees where EmployeeId = " + id;
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Updated successfully";
            }
            catch
            {
                return "Failed to update";
            }
        }
    }
}


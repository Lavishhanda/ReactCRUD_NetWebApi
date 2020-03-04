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
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"
                            select DepartmentId, DepartmentName
                            from dbo.Departments";
            using(var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDb"].ConnectionString)) 
            using(var cmd= new SqlCommand(query,conn))
                using(var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        public string Post(Department dept)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                               Insert into dbo.Departments values('"+ dept.DepartmentName+ @"')";
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

        public string Put(Department dept)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                               Update dbo.Departments set DepartmentName = '" + dept.DepartmentName + @"'
                                where DepartmentId = " + dept.DepartmentId + @"
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
                               Delete from dbo.Departments where DepartmentId = " + id;
                                
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }

                return "Deleted successfully";
            }
            catch
            {
                return "Failed to delete";
            }
        }
    }
}

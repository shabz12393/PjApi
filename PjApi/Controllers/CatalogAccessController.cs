using System;
using System.Data;
using System.Data.Common;
using System.Web.Http;

namespace PjApi.Controllers
{
    public class CatalogAccessController : ApiController
    {
        public struct UserDetails
        {
            public int user_id;
            public string full_name;
            public int role_id;
            public string role;
        }
        public static class CatalogAccess
        {
            static CatalogAccess()
            {
                //
                // TODO: Add constructor logic here
                //
            }

            //pj login function
            public static UserDetails Login(string user_name, string password)
            {
                // get a configured DbCommand object
                DbCommand comm = GenericDataAccessController.GenericDataAccess.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "ViableLogin";

                // create a new parameter
                DbParameter param = comm.CreateParameter();
                param.ParameterName = "@_userName";
                param.Value = user_name;
                param.DbType = DbType.String;
                param.Size = 50;
                comm.Parameters.Add(param);

                // create a new parameter
                param = comm.CreateParameter();
                param.ParameterName = "@_userPassword";
                param.Value = password;
                param.DbType = DbType.String;
                param.Size = 250;
                comm.Parameters.Add(param);

                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_roleId";
                param.Direction = ParameterDirection.Output;
                param.DbType = DbType.Int32;
                comm.Parameters.Add(param);


                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_userId";
                param.Direction = ParameterDirection.Output;
                param.DbType = DbType.Int32;
                comm.Parameters.Add(param);

                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_role";
                param.Direction = ParameterDirection.Output;
                param.DbType = DbType.String;
                param.Size = 20;
                comm.Parameters.Add(param);
                

                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_fullName";
                param.Direction = ParameterDirection.Output;
                param.DbType = DbType.String;
                param.Size = 250;
                comm.Parameters.Add(param);


                // execute the stored procedure
                GenericDataAccessController.GenericDataAccess.ExecuteSelectCommand(comm);

                UserDetails user = new UserDetails();
                user.full_name = comm.Parameters["@_fullName"].Value.ToString();
                user.role_id = Int32.Parse(comm.Parameters["@_roleId"].Value.ToString());
                user.user_id = Int32.Parse(comm.Parameters["@_userId"].Value.ToString());
                user.role = comm.Parameters["@_role"].Value.ToString();

                return user;
            }

            // Retrieve the list of Products
            public static DataTable GetBookingForMobile()
            {
                // get a configured DbCommand object
                DbCommand comm = GenericDataAccessController.GenericDataAccess.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "getBookingForMobile";
                // execute the stored procedure and return the results
                return GenericDataAccessController.GenericDataAccess.ExecuteSelectCommand(comm);
            }
            // Retrieve the list of Products
            public static DataTable GetServicesForMobile()
            {
                // get a configured DbCommand object
                DbCommand comm = GenericDataAccessController.GenericDataAccess.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "getServicesForMobile";
                // execute the stored procedure and return the results
                return GenericDataAccessController.GenericDataAccess.ExecuteSelectCommand(comm);
            }

            // get department details
            public static DataTable GetProductDetails(string productId)
            {
                // get a configured DbCommand object
                DbCommand comm = GenericDataAccessController.GenericDataAccess.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "getProductForId";
                // create a new parameter
                DbParameter param = comm.CreateParameter();
                param.ParameterName = "@prodt_id";
                param.Value = productId;
                param.DbType = DbType.String;
                comm.Parameters.Add(param);
                // execute the stored procedure
               return GenericDataAccessController.GenericDataAccess.ExecuteSelectCommand(comm);
            }

            
            public static bool Add_Booking(string customer_code, int staff_id, DateTime booked_date, string role)
            {
                // get a configured DbCommand object
                DbCommand comm = GenericDataAccessController.GenericDataAccess.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "addBooking";

                // create a new parameter
                DbParameter param = comm.CreateParameter();
                param.ParameterName = "@_customerCode";
                param.Value = customer_code;
                param.DbType = DbType.String;
                param.Size = 20;
                comm.Parameters.Add(param);


                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_staffId";
                param.Value = staff_id;
                param.DbType = DbType.Int32;
                comm.Parameters.Add(param);

                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_bookedDate";
                param.Value = booked_date;
                param.DbType = DbType.Date;
                comm.Parameters.Add(param);

                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_role";
                param.Value = role;
                param.DbType = DbType.String;
                param.Size = 10;
                comm.Parameters.Add(param);


                return Comm_Result(comm);

            }
            //Audit
            public static bool Log_Audit(string action, string table, string details, string by)
            {
                // get a configured DbCommand object
                DbCommand comm = GenericDataAccessController.GenericDataAccess.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "LogAudit";

                // create a new parameter
                DbParameter param = comm.CreateParameter();
                param.ParameterName = "@_action";
                param.Value = action;
                param.DbType = DbType.String;
                param.Size = 50;
                comm.Parameters.Add(param);


                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_table";
                param.Value = table;
                param.DbType = DbType.String;
                param.Size = 50;
                comm.Parameters.Add(param);

                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_details";
                param.Value = details;
                param.DbType = DbType.String;
                param.Size = 500;
                comm.Parameters.Add(param);

                // create a new parameter (out)
                param = comm.CreateParameter();
                param.ParameterName = "@_by";
                param.Value = by;
                param.DbType = DbType.String;
                param.Size = 50;
                comm.Parameters.Add(param);


                return Comm_Result(comm);

            }

            //Audit
            public static bool Log_Error(string message)
            {
                // get a configured DbCommand object
                DbCommand comm = GenericDataAccessController.GenericDataAccess.CreateCommand();
                // set the stored procedure name
                comm.CommandText = "LogAudit";

                // create a new parameter
                DbParameter param = comm.CreateParameter();
                param.ParameterName = "@_message";
                param.Value = message;
                param.DbType = DbType.String;
                param.Size = 250;
                comm.Parameters.Add(param);

                return Comm_Result(comm);

            }
            public static bool Comm_Result(DbCommand comm)
            {
                /// result will represent the number of changed rows
                int result = -1;
                try
                {
                    // execute the stored procedure
                    result = GenericDataAccessController.GenericDataAccess.ExecuteNonQuery(comm);
                }
                catch
                {
                    // any errors are logged in GenericDataAccess, we ignore them here
                }
                // result will be 1 in case of success
                return (result != -1);
            }
        }
    }
}

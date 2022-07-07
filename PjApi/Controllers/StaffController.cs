using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PjApi.App_Code.dsMainTableAdapters;
using PjApi.Models;
using Newtonsoft.Json;
using PjApi.App_Code;
using System.Data;

namespace PjApi.Controllers
{
    public class StaffController : ApiController
    {
        staffTableAdapter staffAdapter = new staffTableAdapter();
        dsMain ds = new dsMain();
        [HttpGet]
        // POST api/Notification
        public IHttpActionResult Get()
        {
            staffAdapter.Fill(ds.staff);
            try
            {
                List<Staff> s = ds.staff.AsEnumerable()
                .Select(dataRow => new Staff
                {
                    staffId = dataRow.Field<int>("staffId"),
                    fullName = dataRow.Field<string>("full_name"),
                    telephone = dataRow.Field<string>("telephone"),
                    email = dataRow.Field<string>("email"),
                    gender = dataRow.Field<string>("gender")
                }).ToList();
                return Ok(s);
            }
            catch (Exception ex)
            {
                CatalogAccessController.CatalogAccess.Log_Error("StaffController: " + ex.Message);
                return NotFound();
            }
        }
    }
}

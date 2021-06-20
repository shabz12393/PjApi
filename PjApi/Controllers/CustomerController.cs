using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PjApi.App_Code.dsMainTableAdapters;
using PjApi.App_Code.dsProcTableAdapters;
using PjApi.Models;
using Newtonsoft.Json;
using PjApi.App_Code;
using System.Data;

namespace PjApi.Controllers
{
    public class CustomerController : ApiController
    {
        errorsTableAdapter errorAdapter = new errorsTableAdapter();
        dsMain ds = new dsMain();
        customerTableAdapter customerAdapter = new customerTableAdapter();

        [HttpPost]
        // POST api/Notification
        public IHttpActionResult Post([FromBody] Customers c)
        {
            ResponseMessage m;
            try
            {

                customerAdapter.addCustomer(c.fullName, c.telephone, c.email, c.gender, c.notes);
                m = new ResponseMessage(true, "Success");

            }
            catch (Exception ex)
            {
                m = new ResponseMessage(false, ex.Message);
                errorAdapter.Insert("CustomerController: " + ex.Message);
            }
            return Ok(m);
        }

        [HttpPut]
        [Route("api/customer/update")]
        // POST api/Notification
        public IHttpActionResult PUT([FromBody] Customers c)
        {
            ResponseMessage m;
            try
            {

                customerAdapter.Update(c.fullName, c.telephone, c.email, c.gender, c.notes, c.customerId);
                m = new ResponseMessage(true, "Success");

            }
            catch (Exception ex)
            {
                m = new ResponseMessage(false, ex.Message);
                errorAdapter.Insert("CustomerController: " + ex.Message);
            }
            return Ok(m);
        }

        [HttpGet]
        // POST api/Notification
        public IHttpActionResult Get()
        {
            customerAdapter.Fill(ds.customer);
            try
            {
                List<Customers> c = ds.customer.AsEnumerable()
                .Select(dataRow => new Customers
                {
                    customerId = dataRow.Field<int>("customerId"),
                    customerCode = dataRow.Field<string>("customer_code"),
                    fullName = dataRow.Field<string>("full_name"),
                    telephone = dataRow.Field<string>("telephone"),
                    email = dataRow.Field<string>("email"),
                    gender = dataRow.Field<string>("gender"),
                    notes = dataRow.Field<string>("notes")
                }).ToList();
                return Ok(c);
            }
            catch (Exception ex)
            {
                errorAdapter.Insert("CustomerController: " + ex.Message);
                return NotFound();
            }
        }
    }
}

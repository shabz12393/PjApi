using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PjApi.Models;
using System.Data;

namespace PjApi.Controllers
{
    public class BookingController : ApiController
    {

        [HttpPost]
        // POST api/Notification
        public IHttpActionResult Post([FromBody] Bookings b)
        {
            ResponseMessage m;
            try
            {

               bool response = CatalogAccessController.CatalogAccess.Add_Booking(
                   b.customerCode, 
                   b.staffId, 
                   DateTime.Parse(b.bookedDt),
                    "Adult"
                    );

                m = new ResponseMessage(true, response?"Insert successful": "Insert failed");

            }
            catch (Exception ex)
            {
                m = new ResponseMessage(false, ex.Message);
                CatalogAccessController.CatalogAccess.Log_Error("BookingController: " + ex.Message);
            }
            return Ok(m);
        }

        [HttpGet]
        // POST api/Notification
        public IHttpActionResult Get()
        {
            try
            {
                List<Bookings> bookings = CatalogAccessController.CatalogAccess.GetBookingForMobile().AsEnumerable()
                 .Select(dataRow => new Bookings
                 {
                     bookingId = dataRow.Field<int>("bookingId"),
                     customer = dataRow.Field<string>("customer"),
                     customerCode = dataRow.Field<string>("customer_code"),
                     staffId = dataRow.Field<int>("staffId"),
                     paid = dataRow.Field<int>("paid"),
                     discount = dataRow.Field<int>("discount"),
                     balance = dataRow.Field<int>("balance"),
                     bookedDt = dataRow.Field<DateTime>("booked_date").ToShortDateString(),
                     paymentStat = dataRow.Field<string>("payment_status"),
                     bookingStat = dataRow.Field<string>("booking_status"),
                 }).ToList();

               
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                CatalogAccessController.CatalogAccess.Log_Error("BookingController: " + ex.Message);
                return NotFound();
            }
        }
    }
}

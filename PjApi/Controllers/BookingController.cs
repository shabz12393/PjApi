using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PjApi.App_Code.dsMainTableAdapters;
using PjApi.App_Code.dsProcTableAdapters;
using PjApi.Models;
using PjApi.App_Code;
using System.Data;

namespace PjApi.Controllers
{
    public class BookingController : ApiController
    {
        customer_bookingsTableAdapter bookingAdapter = new customer_bookingsTableAdapter();
        getBookingForMobileTableAdapter getBookingAdapter = new getBookingForMobileTableAdapter();
        getServicesForMobileTableAdapter serviceAdapter = new getServicesForMobileTableAdapter();
        errorsTableAdapter errorAdapter = new errorsTableAdapter();
        dsMain ds = new dsMain();
        dsProc dsproc = new dsProc();

        [HttpPost]
        // POST api/Notification
        public IHttpActionResult Post([FromBody] Bookings b)
        {
            ResponseMessage m;
            try
            {
                string schedule_status = "Pending";
                string payment_status = "Pending";

                bookingAdapter.Insert(b.customerCode, b.staffId, 0, payment_status, schedule_status, DateTime.Parse(b.bookedDt), b.notes, "Adult");

                m = new ResponseMessage(true, "Success");

            }
            catch (Exception ex)
            {
                m = new ResponseMessage(false, ex.Message);
                errorAdapter.Insert("BookingController: " + ex.Message);
            }
            return Ok(m);
        }

        [HttpGet]
        // POST api/Notification
        public IHttpActionResult Get()
        {
            try
            {
                getBookingAdapter.Fill(dsproc.getBookingForMobile);

                List<Bookings> bookings = dsproc.getBookingForMobile.AsEnumerable()
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
                errorAdapter.Insert("BookingController: " + ex.Message);
                return NotFound();
            }
        }
    }
}

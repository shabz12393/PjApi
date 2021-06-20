using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using PjApi.App_Code.dsProcTableAdapters;
using PjApi.App_Code.dsMainTableAdapters;
using PjApi.Models;
using PjApi.App_Code;
using System.Data;

namespace PjApi.Controllers
{
    public class ServiceController : ApiController
    {
        customer_bookingsTableAdapter bookingAdapter = new customer_bookingsTableAdapter();
        getServicesForMobileTableAdapter serviceAdapter = new getServicesForMobileTableAdapter();
        errorsTableAdapter errorAdapter = new errorsTableAdapter();
        dsMain ds = new dsMain();
        dsProc dsproc = new dsProc();

        [HttpPost]
        // POST api/Notification
        public IHttpActionResult Post([FromBody] CustomerServices s)
        {
            ResponseMessage m;
            try
            {
                bookingAdapter.addService(s.bookingId, s.serviceId, s.receptionId, s.seatId,
                    s.timeIn, s.timeOut, DateTime.Parse(s.bookedDt), s.serviceStatus, s.stylists, s.receptionId);

                m = new ResponseMessage(true, "Success");

            }
            catch (Exception ex)
            {
                m = new ResponseMessage(false, ex.Message);
                errorAdapter.Insert("ServiceController: " + ex.Message);
            }
            return Ok(m);
        }

        [HttpGet]
        // POST api/Notification
        public IHttpActionResult getServicesForBooking(int bookingId)
        {
            try
            {
                serviceAdapter.Fill(dsproc.getServicesForMobile, bookingId);

                List<CustomerServices> services = dsproc.getServicesForMobile.AsEnumerable()
                 .Select(dataRow => new CustomerServices
                 {
                     customerServiceId = dataRow.Field<int>("customer_service_id"),
                     bookingId = dataRow.Field<int>("bookingId"),
                     serviceId = dataRow.Field<int>("serviceId"),
                     seatId = dataRow.Field<int>("seatId"),
                     timeIn = dataRow.Field<string>("timeIn"),
                     timeOut = dataRow.Field<string>("timeOut"),
                     bookedDt = dataRow.Field<DateTime>("booked_date").ToShortDateString(),
                     paid = dataRow.Field<int>("paid"),
                     balance = dataRow.Field<int>("balance"),
                     serviceStatus = dataRow.Field<string>("service_status"),
                     paymentStatus = dataRow.Field<string>("payment_status"),
                     notes = dataRow.Field<string>("_notes"),
                     service = dataRow.Field<string>("service"),
                     stylist = dataRow.Field<string>("stylist")
                 }).ToList();


                return Ok(services);
            }
            catch (Exception ex)
            {
                errorAdapter.Insert("ServiceController: " + ex.Message);
                return NotFound();
            }
        }
    }
}

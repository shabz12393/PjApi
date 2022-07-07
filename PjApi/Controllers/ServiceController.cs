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
        seatsTableAdapter seatAdapter = new seatsTableAdapter();
        servicesTableAdapter srvcAdapter = new servicesTableAdapter();
        dsMain ds = new dsMain();
        dsProc dsproc = new dsProc();

        [HttpPost]
        // POST api/Notification
        public IHttpActionResult Post([FromBody] CustomerServices s)
        {
            ResponseMessage m;
            try
            {
                DateTime booked_dt = DateTime.Parse(s.bookedDt);
              bookingAdapter.addService(s.bookingId, s.serviceId, s.stylistId, s.seatId,
                    s.timeIn, s.timeOut,booked_dt , s.serviceStatus, s.stylists);

                m = new ResponseMessage(true, "Success");

            }
            catch (Exception ex)
            {
                m = new ResponseMessage(false, ex.Message);
                CatalogAccessController.CatalogAccess.Log_Error("ServiceController: " + ex.Message);
            }
            return Ok(m);
        }

        [HttpGet]
        public IHttpActionResult getServicesForBooking(int bookingId)
        {
            try
            {

                List<CustomerServices> services = CatalogAccessController.CatalogAccess.GetServicesForMobile().AsEnumerable()
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
                CatalogAccessController.CatalogAccess.Log_Error("ServiceController: " + ex.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/service/services")]
        public IHttpActionResult getServices()
        {
            try
            {
                srvcAdapter.Fill(ds.services);

                List<Services> services = ds.services.AsEnumerable()
                 .Select(dataRow => new Services
                 {
                     serviceId = dataRow.Field<int>("serviceId"),
                     service = dataRow.Field<string>("service"),
                     price = dataRow.Field<int>("price")
                 }).ToList();


                return Ok(services);
            }
            catch (Exception ex)
            {
                CatalogAccessController.CatalogAccess.Log_Error("ServiceController: " + ex.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/service/seats")]
        public IHttpActionResult getSeats()
        {
            try
            {
                seatAdapter.Fill(ds.seats);

                List<Seat> s = ds.seats.AsEnumerable()
                 .Select(dataRow => new Seat
                 {
                     seatId = dataRow.Field<int>("seatId"),
                     seat = dataRow.Field<string>("seat")
                 }).ToList();


                return Ok(s);
            }
            catch (Exception ex)
            {
                CatalogAccessController.CatalogAccess.Log_Error("ServiceController: " + ex.Message);
                return NotFound();
            }
        }
    }
}

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
    public class PaymentController : ApiController
    {
        customer_bookingsTableAdapter bookingAdapter = new customer_bookingsTableAdapter();
        getPaymentsForMobileTableAdapter paymentAdapter = new getPaymentsForMobileTableAdapter();
        dsMain ds = new dsMain();
        dsProc dsproc = new dsProc();
        [HttpGet]
        // POST api/Notification
        public IHttpActionResult getPaymentForBooking(int bookingId)
        {
            try
            {
                paymentAdapter.Fill(dsproc.getPaymentsForMobile, bookingId);

                List<CustomerPayment> payment = dsproc.getPaymentsForMobile.AsEnumerable()
                 .Select(dataRow => new CustomerPayment
                 {
                     depositId = dataRow.Field<int>("depositId"),
                     pmId = dataRow.Field<int>("pm_id"),
                     customerServiceId = dataRow.Field<int>("customer_service_id"),
                     staffId = dataRow.Field<int>("staffId"),
                     deposit = dataRow.Field<int>("deposit"),
                     discount = dataRow.Field<int>("discount"),
                     depositDate = dataRow.Field<DateTime>("depositDate").ToShortDateString(),
                     paymentMode = dataRow.Field<string>("pay_mode")
                 }).ToList();


                return Ok(payment);
            }
            catch (Exception ex)
            {
                CatalogAccessController.CatalogAccess.Log_Error("PaymentController: " + ex.Message);
                return NotFound();
            }
        }
    }
}

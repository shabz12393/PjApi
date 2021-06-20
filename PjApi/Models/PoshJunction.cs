using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PjApi.Models
{
    public class Login
    {
        public Login()
        {

        }
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class ResponseMessage
    {
        public ResponseMessage()
        {

        }

        public ResponseMessage(bool status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Users
    {
        public Users()
        {

        }
        public int userId { get; set; }
        public string fullName { get; set; }
        public string role { get; set; }
    }

    public class Bookings
    {
        public Bookings()
        {
        }
        public int bookingId { get; set; }
        public string customer { get; set; }
        public string customerCode { get; set; }
        public int staffId { get; set; }
        public int paid { get; set; }
        public int discount { get; set; }
        public int balance { get; set; }
        public string bookedDt { get; set; }
        public string paymentStat { get; set; }
        public string bookingStat { get; set; }
        public string notes { get; set; }
    }

    public class CustomerServices
    {
        public CustomerServices()
        {

        }
        public int customerServiceId { get; set; }
        public int bookingId { get; set; }
        public int receptionId { get; set; }
        public int serviceId { get; set; }
        public int seatId { get; set; }
        public string timeIn { get; set; }
        public string timeOut { get; set; }
        public string bookedDt { get; set; }
        public int paid { get; set; }
        public int balance { get; set; }
        public string serviceStatus { get; set; }
        public string paymentStatus { get; set; }
        public string notes { get; set; }
        public string stylist { get; set; }
        public string stylists { get; set; }
        public string service { get; set; }
    }


    public class CustomerPayment
    {
        public CustomerPayment()
        {

        }
        public int depositId { get; set; }
        public int pmId { get; set; }
        public int customerServiceId { get; set; }
        public int staffId { get; set; }
        public int deposit { get; set; }
        public int discount { get; set; }
        public string depositDate { get; set; }
        public string paymentMode { get; set; }
    }
    public class Services
    {
        public Services()
        {

        }
        public int serviceId { get; set; }
        public string service { get; set; }
        public int price { get; set; }
    }

    public class Customers
    {
        public Customers()
        {

        }
        public int customerId { get; set; }
        public string customerCode { get; set; }
        public string fullName { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string notes { get; set; }

    }
    public class Sale
    {
        public Sale()
        {

        }
        public int saleId { get; set; }
        public string batchNo { get; set; }
        public int customerId { get; set; }
        public int staffId { get; set; }
        public string customer { get; set; }
        public string cashier { get; set; }
        public int paid { get; set; }
        public int discount { get; set; }
        public int balance { get; set; }
        public string paymentStat { get; set; }
        public string saleStatus { get; set; }
        public string salesDt { get; set; }
        public string notes { get; set; }
    }
    public class Cart
    {
        public Cart()
        {

        }
        public int productId { get; set; }
        public int saleId { get; set; }
        public int quantity { get; set; }
        public int saleBy { get; set; }
    }
    public class CartItems
    {
        public CartItems()
        {

        }
        public int cartId { get; set; }
        public int saleId { get; set; }
        public string product { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public int amount { get; set; }
        public int saleBy { get; set; }
        public string cartStatus { get; set; }
    }
    public class Product
    {
        public Product()
        {

        }
        public int productId { get; set; }
        public int categoryId { get; set; }
        public string productCode { get; set; }
        public int product { get; set; }
        public int price { get; set; }
        public string expiryDt { get; set; }
    }
    public class SalePayment
    {
        public SalePayment()
        {

        }
        public int customerId { get; set; }
        public int amount { get; set; }
        public int discount { get; set; }
        public int pmId { get; set; }
        public int saleId { get; set; }
        public string tranNotes { get; set; }
    }
}
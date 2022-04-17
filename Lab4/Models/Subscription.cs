using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace Assignment02NET.Models
{
    public class Subscription
    {


        public int ClientId { get; set; }
        public Client Client { get; set; }


        public Brokerage Brokerage { get; set; }
        public string BrokerageId { get; set; }
       
    }
}

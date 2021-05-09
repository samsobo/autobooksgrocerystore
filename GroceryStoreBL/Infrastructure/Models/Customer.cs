using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreBL.Infrastructure.Models
{
    public class CustomerCollection
    {
        [JsonProperty("customers")]
        public ICollection<Customer> Customers { get; set; }
    }
    public class Customer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

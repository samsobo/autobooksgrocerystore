using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreBL.Infrastructure.Models
{
    /// <summary>
    /// Customer Model Collection Root
    /// </summary>
    public class CustomerCollection
    {
        [JsonProperty("customers")]
        public ICollection<Customer> Customers { get; set; }
    }

    /// <summary>
    /// Customer Model
    /// </summary>
    public class Customer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

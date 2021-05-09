using GroceryStoreBL.Infrastructure.Interfaces;
using GroceryStoreBL.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GroceryStoreBL.Infrastructure.Services
{
    public class JsonCustomerService : ICustomerService
    {
        private readonly string _filePath;

        private static object _lock = new object();

        public JsonCustomerService(string filePath)
        {
            _filePath = filePath;
        }

        public Customer Add(Customer entity)
        {
            var customers = Read(_filePath);

            var maxId = customers.Customers.Max(c => c.Id);

            var newId = maxId == 0 ? 1 : (maxId + 1);

            entity.Id = newId;

            customers.Customers.Add(entity);

            Write(customers, _filePath);

            return entity;
        }

        public void Delete(int id)
        {
            var collection = Read(_filePath);

            var customer = collection.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return;

            collection.Customers.Remove(customer);

            Write(collection, _filePath);
        }

        public Customer Find(Func<Customer, bool> filter = null, string includes = "")
        {
            var collection = Read(_filePath);
            return collection.Customers.FirstOrDefault(filter);
        }

        public Customer GetById(int id)
        {
            return Read(_filePath)
                .Customers
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> List()
        {
            return Read(_filePath).Customers;
        }

        public void Update(Customer entity)
        {
            var collection = Read(_filePath);

            var customer = collection
                .Customers
                .FirstOrDefault(c => c.Id == entity.Id);

            if (customer == null)
                return;

            customer.Name = entity.Name;

            Write(collection, _filePath);
        }

        private static CustomerCollection Read(string filePath)
        {
            lock (_lock)
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    return JsonConvert.DeserializeObject<CustomerCollection>(streamReader.ReadToEnd());
                }
            }

        }

        private static void Write(CustomerCollection customers, string filePath)
        {
            lock (_lock)
            {
                var customerData = JsonConvert.SerializeObject(customers);
                File.WriteAllText(filePath, customerData);
            }
        }
    }
}

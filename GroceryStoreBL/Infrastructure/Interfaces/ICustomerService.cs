using GroceryStoreBL.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStoreBL.Infrastructure.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> List();
        Customer GetById(int id);
        Customer Find(Func<Customer, bool> filter = null, string includes = "");
        Customer Add(Customer entity);
        void Update(Customer entity);
        void Delete(int id);
    }
}

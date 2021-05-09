using GroceryStoreBL.Infrastructure.Interfaces;
using GroceryStoreBL.Infrastructure.Models;
using GroceryStoreBL.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace GroceryStoreTest
{
    [TestClass]
    public class GroceyStoreTests
    {

        private static IConfiguration Configuration;
        private static IServiceProvider ServiceProvider;
        private static ICustomerService CustomerService;

        [ClassInitialize]
        public static void TestInit(TestContext context)
        {
            Configuration = new ConfigurationBuilder()
             .SetBasePath(context.DeploymentDirectory)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables().Build();

            var services = new ServiceCollection();

            services.AddTransient<ICustomerService>(sp =>
            {
                var fileLocation = $@"{context.DeploymentDirectory}\{Configuration.GetSection("CustomerFile:Name").Value}";
                return new JsonCustomerService(fileLocation);
            });
            ServiceProvider = services.BuildServiceProvider();
            CustomerService = ServiceProvider.GetRequiredService<ICustomerService>();
        }

        [TestMethod]
        public void GetCustomersTest()
        {
            var customers = CustomerService.List();
            Assert.IsNotNull(customers);
        }

        [TestMethod]
        public void GetCustomerTest()
        {
            var customer = CustomerService.GetById(1);
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public void AddCustomerTest()
        {
            var customer = new Customer
            {
                Name = "drDetroit"
            };
            CustomerService.Add(customer);
            Assert.IsTrue(customer.Id > 0);

        }

        [TestMethod]
        public void UpdateCustomerTest()
        {
            var customer = CustomerService.Find(c => c.Name == "drDetroit");
            customer.Name = "Buford";
            CustomerService.Update(customer);
            customer = CustomerService.Find(c => c.Name == "Buford");
            Assert.IsNotNull(customer);

        }

        [TestMethod]
        public void DeleteCustomerTest()
        {
            var customer = CustomerService.Find(c => c.Name == "Buford");
            CustomerService.Delete(customer.Id);
            Assert.IsNull(CustomerService.Find(c => c.Name == "Buford"));
        }

    }
}

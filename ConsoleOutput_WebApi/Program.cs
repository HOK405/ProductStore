using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleOutput_WebApi
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(@"https://localhost:44384/CustomerProduct/Select").Result;
                var result = response.Content.ReadAsAsync<List<CustomerProductViewModel>>().Result;

                foreach (CustomerProductViewModel item in result)
                    Console.WriteLine($"{item.CustomerName} -- {item.CustomerPhone} -- {item.ProductName} - {item.TotalPrice}");

                Console.ReadLine();
            }
        }
    }
}

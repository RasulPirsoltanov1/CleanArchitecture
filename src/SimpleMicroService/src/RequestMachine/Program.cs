using SimpleMicroService.Configurations.Models;
using System.Text;
using System.Text.Json;

namespace HttpConsoleApp; 
class Program
{
    static async Task Main(string[] args)
    {
        for (int i = 0; i < 100000000000000; i++)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var order = new Order
                    {
                        OrderID = 1,
                        CustomerID = "CUST001",
                        EmployeeID = 2,
                        OrderDate = DateTime.Now
                    };

                    var json = JsonSerializer.Serialize(order);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    string url = "http://localhost:1980/orders"; 
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("<- " + i + "->");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        Console.WriteLine("finito");
    }
}

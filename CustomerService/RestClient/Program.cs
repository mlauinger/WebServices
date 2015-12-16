using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var task = client.GetAsync("http://localhost:65268/api/customers/6");
            task.ContinueWith(
                t =>
                {
                    t.Result.Content.ReadAsStringAsync().ContinueWith(
                        tt =>
                        {
                            Console.WriteLine(tt.Result);
                        }
                        );
                }
                );
            GetCustomer1();
            Console.ReadLine();

        }

        private static async void GetCustomer1()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:65268/api/customers/1");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }
    }
}

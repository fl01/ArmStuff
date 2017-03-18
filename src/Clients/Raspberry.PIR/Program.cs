using System;
using System.Net.Http;
using System.Threading;
using Raspberry.PIR.Http;
using Raspberry.PIR.Models;

namespace Raspberry.PIR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHttpClient httpClient = new SimpleHttpClient();
            Console.WriteLine("Hey. I'm PIR handler.");
            int totalPing = 10;
            for (int i = 0; i < totalPing; i++)
            {
                Console.WriteLine($"Ping #{i} of {totalPing}");
                Ping(httpClient);
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }

            Console.WriteLine("PIR handler is out");
        }

        private static void Ping(IHttpClient client)
        {
            HttpResponseMessage pingResult = client.PostJson("http://192.168.1.165:5100/api/ping", new Ping(new Guid("BB4F1E20-6B52-4EA5-B120-AEFE21894B49")));
            if (pingResult != null)
            {
                string message = (pingResult.IsSuccessStatusCode ? "Success - " : "Fail - ") + pingResult.StatusCode;
                Console.WriteLine(message);
            }
        }
    }
}
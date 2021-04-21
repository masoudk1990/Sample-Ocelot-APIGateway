using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44340");

            //no authentication is required
            var res = client.GetAsync("/customers/1").Result;
            Console.WriteLine(res.Content.ReadAsStringAsync().Result);

            //authentication is required
            client.DefaultRequestHeaders.Clear();
            var jwt = GetJwt();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            var resWithToken = client.GetAsync("/customers").Result;
            Console.WriteLine(resWithToken.Content.ReadAsStringAsync().Result);


            Console.Read();
        }

        private static string GetJwt()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44358");
            client.DefaultRequestHeaders.Clear();
            var res2 = client.GetAsync("/api/auth?name=masoud&pwd=123456").Result;
            dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);
            return jwt.access_token;
        }
    }
}

using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage res = await httpClient.GetAsync("https://www.pja.edu.pl/dziekanat");

            if (res.IsSuccessStatusCode)
            {
                string html = await res.Content.ReadAsStringAsync();
                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);
                MatchCollection matches = regex.Matches(html);
                foreach (var m in matches)
                {
                    Console.WriteLine(m.ToString());
                }

            }

            Console.WriteLine("Koniec!");
        }
    }
}
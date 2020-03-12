using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
             if (args is null || args.Length < 1){
                 throw new System.ArgumentNullException("Url");
             }else{
                Uri validatedUri;
                if (Uri.TryCreate(args[0], UriKind.Absolute, out validatedUri) && (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps))
                {
                    var httpClient = new HttpClient();
                    HttpResponseMessage res = await httpClient.GetAsync("https://www.pja.edu.pl/dziekanat");
                    if (res.IsSuccessStatusCode)
                    {
                        string html = await res.Content.ReadAsStringAsync();
                        res.Dispose();
                        var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);
                        MatchCollection matches = regex.Matches(html);
                        if(matches is null || matches.Count < 1)
                        {
                            Console.WriteLine("Nie znaleziono adresów email");
                        }
                        else
                        {
                            HashSet<string> uniqueSet = new HashSet<string>();
                            foreach (var m in matches)
                            {
                                uniqueSet.Add(m.ToString());
                            }
                            foreach(var x in uniqueSet)
                            {
                                Console.WriteLine(x);
                            }

                        }
                    }
                    else
                    {
                        res.Dispose();
                        Console.WriteLine("Błąd w czasie pobierania strony");
                    }
                }
                else{
                    throw new System.ArgumentException("Url");
                }
             }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Espp
    {
        public static readonly Uri Uri = new Uri("http://sm-web33.espp.gvc.rzd/sm/index.do");
        private static readonly HttpClient Client = new HttpClient();
        // public const string Uri = @"http://http://sm-web33.espp.gvc.rzd/";
        
        private async Task<bool> LogIn()
        {
            string data;
            // var baseAddress = new Uri("http://sm-web33.espp.gvc.rzd/");//base site address
            // var url = "sm/index.do";//needs site page
            var cookieContainer = new CookieContainer();//sent cookie

            using var handler = new HttpClientHandler(){ CookieContainer = cookieContainer };
            using (var client = new HttpClient(handler) {BaseAddress = Uri})
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.96 Safari/537.36");
                cookieContainer.Add(Uri, new Cookie("lang","en"));
                
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("user.id","МИИТ-030"),
                    new KeyValuePair<string, string>("old.password", "123456789"),
                    new KeyValuePair<string, string>("l.language","ru"),
                    new KeyValuePair<string, string>("type", "login"),
                    new KeyValuePair<string, string>("xHtoken", ""),
                    new KeyValuePair<string, string>("event", "0"), 
                });

                var result = await client.PostAsync(Uri, content);
                var bytes = await result.Content.ReadAsByteArrayAsync();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                // data = encoding.GetString(bytes, 0, bytes.Length);
                data = await result.Content.ReadAsStringAsync();
                result.EnsureSuccessStatusCode();
                if (data != null)
                {
                    return true;
                }
            }
            return false;
            throw new Exception("LogIn is not correct");
        }

        public static async Task<bool> MyLogIn()
        {
            // var webRequest = WebRequest.Create(Uri);
            // if (webRequest != null)
            // {
            //     webRequest.Method = "POST";
            //     webRequest.Headers.Add("Host", "sm-web33.espp.gvc.rzd");
            //     webRequest.Headers.Add("UserAgent",
            //         "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0");
            //     webRequest.Headers.Add("Accept", "");
            //     webRequest.Headers.Add("AcceptLanguage", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            //     webRequest.Headers.Add("AcceptEncoding", "gzip, deflate");
            //     webRequest.ContentType = "application/x-www-form-urlencoded";
            //     webRequest.ContentLength = 101;
            //     webRequest.Headers.Add("Origin", "http://sm-web33.espp.gvc.rzd");
            //     webRequest.Headers.Add("DNT", "1");
            //     webRequest.Headers.Add("Connection", "keep-alive");
            //     webRequest.Headers.Add("Referer", "http://sm-web33.espp.gvc.rzd/sm/index.do");
            //     webRequest.Headers.Add("Upgrade-Insecure-Requests", "1");
            // }
            //
            
            var cookieContainer = new CookieContainer();

            using var handler = new HttpClientHandler() {CookieContainer = cookieContainer};
            using (var client = new HttpClient() {BaseAddress = Uri})
            {
                client.DefaultRequestHeaders.Host = "sm-web33.espp.gvc.rzd";
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0");
                client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
                client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("ContentLength","101");
                client.DefaultRequestHeaders.Add("Origin", "http://sm-web33.espp.gvc.rzd");
                client.DefaultRequestHeaders.Add("DNT","1");
                client.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");
                
                cookieContainer.Add(Uri, new Cookie("lang", "en"));
                
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("user.id","МИИТ-030"),
                    new KeyValuePair<string, string>("old.password", "123456789"),
                    new KeyValuePair<string, string>("l.language","ru"),
                    new KeyValuePair<string, string>("type", "login"),
                    new KeyValuePair<string, string>("xHtoken", ""),
                    new KeyValuePair<string, string>("event", "0"), 
                });

                var result = await client.PostAsync(Uri, content);
                var bytes = await result.Content.ReadAsByteArrayAsync();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                var data = encoding.GetString(bytes, 0, bytes.Length);
                // var data = await result.Content.ReadAsStringAsync();
                result.EnsureSuccessStatusCode();

                Console.WriteLine(data);
            }
            
            
            

            return false;
        }
        
        public bool LogOut()
        {
            return false;
        }
        
        static string LoadPage(string url)
        {
            var result = string.Empty;
            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    StreamReader streamReader;
                    if (response.CharacterSet != null)
                        streamReader = new StreamReader(responseStream, Encoding.GetEncoding(response.CharacterSet));
                    else
                        streamReader = new StreamReader(responseStream);

                    result = streamReader.ReadToEnd();
                    streamReader.Close();


                }
            }
            {
                
            }
            return result;
        }
        
        
        
        static async void HttpClientAuth()
        {
            string loginUri = "http://sm-web33.espp.gvc.rzd/sm/index.do";
            string username = "МИИТ-030";
            string password = "123456789";

            CookieContainer cc = new CookieContainer();
            var handler = new HttpClientHandler {CookieContainer = cc};
            var request = new HttpRequestMessage(HttpMethod.Post, loginUri);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", username},
                {"password", password}
            });
            
            var client = new HttpClient(handler);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }    
        
        static void Auth()
        {
            string loginUri = "http://sm-web33.espp.gvc.rzd/sm/index.do";
            const string username = "МИИТ-030";
            const string password = "123456789";
            string reqString = "user.id=" + username + "&old.password" + password + "L.language=ru&type=login&xHtoken=&event=0";
            byte[] requestData = Encoding.UTF8.GetBytes(reqString);

            CookieContainer cc = new CookieContainer();
            var request = (HttpWebRequest)WebRequest.Create(loginUri);
            request.Proxy = null!;
            request.CookieContainer = cc;
            request.Method = "POST";

            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestData.Length;

            using (Stream S = request.GetRequestStream())
                S.Write(requestData, 0, requestData.Length);
            
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie c in response.Cookies)
                {
                    Console.WriteLine(c.Name + " = " + c.Value);
                }
            }
            
            
        }

        static void WriteToFile(string input)
        {
            const string fileLoc = @"C:\Danila\AppFolder\espp_answer.txt";
            DirectoryInfo directoryInfo = new DirectoryInfo(fileLoc);

            using (StreamWriter sw = new StreamWriter(fileLoc, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(input.Trim());
            }
        }

    }
}
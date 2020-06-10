using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace WebClientParser
{
    public static class Request
    {
        private static string Login { get; set; } = "МИИТ-030";
        private static string Password { get; set; } = "123456789";
        private const string Url = @"http://sm-web33.espp.gvc.rzd";
        private const string Uri = @"http://sm-web33.espp.gvc.rzd/sm/index.do";
        
        public static async void LogIn()
        {
        // var cc = new CookieContainer();
        // var handler = new HttpClientHandler();
        // var request = new HttpRequestMessage();
        //
        // request.Content = new FormUrlEncodedContent(new Dictionary<string, string>(
        // {
        //     {"user.id", Login},
        //     {"L.Language", "ru"},
        //     {"type", "login"},
        //     {"xHtoken", ""},
        //     {"old.password", Password},
        //     {"event", "0"}
        // });
        //
        // var client = new HttpClient();
        // var response = await client.SendAsync(request);
        // response.EnsureSuccessStatusCode();
        
        
        string reqString = "user.id=" + Login + "&old.password" + Password + "L.language=ru&type=login&xHtoken=&event=0";
        byte[] requestData = Encoding.UTF8.GetBytes(reqString);

        CookieContainer cc = new CookieContainer();
        var request = (HttpWebRequest)WebRequest.Create(Uri);
        request.Proxy = null!;
        request.CookieContainer = cc;
        request.Method = "POST";
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = requestData.Length;

        await using (Stream S = request.GetRequestStream())
            await S.WriteAsync(requestData, 0, requestData.Length);
            
        using (var response = (HttpWebResponse)request.GetResponse())
        {
            foreach (Cookie c in response.Cookies)
            {
                Console.WriteLine(c.Name + " = " + c.Value);
                Console.WriteLine(response.ToString());
            }
        }
        }

        public static async void ReLogIn()
        {
            string reqString = "targetAppmode = index.do" +
                               " & modeSwitchtoken = 17232e2189d-6d09a2ee " +
                               "& serverChanged = false";

            byte[] requestData = Encoding.UTF8.GetBytes(reqString);
            var request = (HttpWebRequest) WebRequest.Create(Uri);
            var cc = new CookieContainer();

            request.Proxy = null;
            request.CookieContainer = cc;
            request.ContentLength = requestData.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Connection = "keep-alive";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
            request.Referer = "http://sm-web33.espp.gvc.rzd/sm/index.do";
            request.Method = "POST";
            
            await using (Stream S = request.GetRequestStream())
                await S.WriteAsync(requestData, 0, requestData.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie c in response.Cookies)
                {
                    Console.WriteLine(c.Name + " = " + c.Value);
                    Console.WriteLine(response.ToString());
                }
            }
        }
public static async void LogOut()
        {
            var reqString = "";   
            byte[] requestData = Encoding.UTF8.GetBytes(reqString);
            
            var cc = new CookieContainer();
            var request = (HttpWebRequest) WebRequest.Create(Uri);
            request.Proxy = null!;
            request.CookieContainer = cc;
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestData.Length;
            request.Accept = "*/*";
            request.Connection = "keep-alive";
            request.Referer = "http://sm-web33.espp.gvc.rzd/sm/index.do";
            
            await using (Stream S = request.GetRequestStream())
                await S.WriteAsync(requestData, 0, requestData.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie c in response.Cookies)
                {
                    Console.WriteLine(c.Name + " = " + c.Value);
                    Console.WriteLine(response.ToString());
                }
            }
        }
        
        

    }
}
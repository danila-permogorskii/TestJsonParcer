using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp
{
    public class User
    {
        public string Name { get; set; } = "МИИТ-030";
        public string Password { get; set; } = "123456789";
        public string CsrfToken { get; set; }
        
        //ESPP links
        public readonly string Url = @"http://sm-web33.espp.gvc.rzd";
        public readonly string Uri = @"http://sm-web33.espp.gvc.rzd/sm/index.do";

        public User()
        {
            Name = null;
            Password = null;
            CsrfToken = null;
        }

        public User(string name, string password, string csrfToken)
        {
            Name = name;
            Password = password;
            CsrfToken = csrfToken;
        }
    }
    
    
    public static class Espp
    {

        
        public static async Task LogIn()
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
        
        //
        // var user = new User();
        //
        // string reqString = "user.id=" + user.Name + "&old.password" + user.Password + "L.language=ru&type=login&xHtoken=&event=0";
        // byte[] requestData = Encoding.UTF8.GetBytes(reqString);
        //
        // CookieContainer cc = new CookieContainer();
        // var request = (HttpWebRequest)WebRequest.Create(user.Uri);
        // request.Proxy = null!;
        // request.CookieContainer = cc;
        // request.Method = "POST";
        // request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
        // request.ContentType = "application/x-www-form-urlencoded";
        // request.ContentLength = requestData.Length;
        //
        // await using (Stream s = request.GetRequestStream())
        //     await s.WriteAsync(requestData, 0, requestData.Length);
        //     
        // using (var response = (HttpWebResponse)request.GetResponse())
        //     Console.WriteLine(response);

        //TODO: This request after tutorial on http://zetcode.com/csharp/httpclient/
        var userName = "МИИТ-030";
        var password = "123456789";
        var url = @"http://sm-web33.espp.gvc.rzd/sm/index.do";
        
        using var client = new HttpClient();

        var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

        var result = await client.GetAsync(url);

        var content = await result.Content.ReadAsStringAsync();
        Console.WriteLine(content.Trim());


        }

        public static async void ReLogIn()
        {
            var user = new User();
            
            string reqString = "targetAppmode = index.do" +
                               " & modeSwitchtoken = 17232e2189d-6d09a2ee " +
                               "& serverChanged = false";

            byte[] requestData = Encoding.UTF8.GetBytes(reqString);
            var request = (HttpWebRequest) WebRequest.Create(user.Uri);
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
            
            await using (Stream s = request.GetRequestStream())
                await s.WriteAsync(requestData, 0, requestData.Length);

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
            var user = new User();
            
            var reqString = "";   
            byte[] requestData = Encoding.UTF8.GetBytes(reqString);
            
            var cc = new CookieContainer();
            var request = (HttpWebRequest) WebRequest.Create(user.Uri);
            request.Proxy = null!;
            request.CookieContainer = cc;
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestData.Length;
            request.Accept = "*/*";
            request.Connection = "keep-alive";
            request.Referer = "http://sm-web33.espp.gvc.rzd/sm/index.do";
            
            await using (Stream s = request.GetRequestStream())
                await s.WriteAsync(requestData, 0, requestData.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                foreach (Cookie c in response.Cookies)
                {
                    Console.WriteLine(c.Name + " = " + c.Value);
                    Console.WriteLine(response.ToString());
                }
            }
        }

        // public static async void VpList()
        // {
        //     var reqString =
        //         "dataOnly=true&columnNames=number;status;contact_name;title;assignment;assignee;affected_item;hpc_sla_expiration;category;open_time;&" +
        //         "event=1000&thread=1&focus=instance/hpc.sla.expiration&clearCache=true&transaction=0&row=0&aftk=" + _csrfToken;
        //     byte[] requestData = Encoding.UTF8.GetBytes(reqString);
        //     
        //     var cc = new CookieContainer();
        //     var request = (HttpWebRequest) WebRequest.Create(Uri);
        //     request.CookieContainer = cc;
        //     request.Method = "Post";
        //     request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0";
        //     request.ContentType = "application/x-www-form-urlencoded";
        //     request.ContentLength = requestData.Length;
        //     request.Accept = "*/*";
        //     request.Connection = "keep-alive";
        //     request.Referer = "http://sm-web33.espp.gvc.rzd/sm/index.do";            
        //
        //     await using (Stream s = request.GetRequestStream())
        //         await s.WriteAsync(requestData, 0, requestData.Length);
        //
        //     using (var response = (HttpWebResponse)request.GetResponse())
        //     {
        //         foreach (Cookie c in response.Cookies)
        //         {
        //             Console.WriteLine(c.Name + " = " + c.Value);
        //             Console.WriteLine(response.ToString());
        //         }
        //     }
        }
        
        

    }

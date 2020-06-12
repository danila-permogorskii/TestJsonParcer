using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SupportApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Espp.LogIn();
            Espp.LogOut();
        }
    }

}
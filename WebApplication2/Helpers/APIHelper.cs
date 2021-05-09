using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2.Helpers
{
    public static class APIHelper
    {
        public static async Task<string> GetAsync(string url)
        {
            string resultado;
            using (var cliente = new HttpClient())
            {
                var minutos = 60;
                cliente.Timeout = new TimeSpan(0, minutos, 0);
                cliente.BaseAddress = new Uri(url);

                var result = await cliente.GetAsync(url);
                resultado = await result.Content.ReadAsStringAsync();
            }
            return resultado;
        }

        public static async Task<string> PostAsync(string url)
        {
            Console.WriteLine("Llamando a " + url);
            string resultado;
            using (var cliente = new HttpClient())
            {
                var minutos = 60;
                cliente.Timeout = new TimeSpan(0, minutos, 0);
                cliente.BaseAddress = new Uri(ConfigurationSettings.AppSettings["Core"]);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("usuarioId", ConfigurationSettings.AppSettings["usuarioId"]),
                    new KeyValuePair<string, string>("tokenId", ConfigurationSettings.AppSettings["tokenId"]),
                    new KeyValuePair<string, string>("version", ConfigurationSettings.AppSettings["version"])
                });
                var result = await cliente.PostAsync(url, content);
                resultado = await result.Content.ReadAsStringAsync();
                Console.WriteLine("Finalizado");
            }
            Environment.Exit(0);
            return resultado;
        }


        public static async Task<string> GetActores()
        {
            return await GetAsync("https://randomuser.me/api/?nat=us,es,gb&inc=name&results=100");
        }

    }
}
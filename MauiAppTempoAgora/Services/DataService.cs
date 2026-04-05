using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Data;


namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "7076d73184e207e5088ee7780f8da1e1";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);


                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = DateTimeOffset.FromUnixTimeSeconds((long)rascunho["sys"]["sunrise"]).LocalDateTime.ToString(),
                        sunset = DateTimeOffset.FromUnixTimeSeconds((long)rascunho["sys"]["sunset"]).LocalDateTime.ToString()
                    };
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    throw new Exception("Cidade não encontrada. Verifique o nome e tente novamente.");
                } else
                {
                    throw new Exception($"Erro: {resp.StatusCode}");
                }
            }

            return t;
        }
    }
}

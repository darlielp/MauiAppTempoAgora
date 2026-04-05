using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Linq.Expressions;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try{
                if(!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);
                    if (t != null) {
                        string dados_previsao = "";
                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max}°C \n" +
                                         $"Temp Mín: {t.temp_min}°C \n" +
                                         $"Descrição do clima: {t.description} \n" +
                                         $"Velocidade do vento: {t.speed} \n" +
                                         $"Visibilidade: {t.visibility / 1000} Km \n";

                        lbl_res.Text = dados_previsao;
                    } else
                    {
                        lbl_res.Text = "Sem dados de previsão.";
                    }

                } else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            } catch(HttpRequestException)
            {
                await DisplayAlert("Erro de conexão", "Sem conexão com a internet. Verifique sua rede.", "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}

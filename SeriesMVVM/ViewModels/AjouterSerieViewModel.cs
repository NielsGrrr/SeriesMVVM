using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using SeriesMVVM.Models.EntityFramework;
using SeriesMVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesMVVM.ViewModels
{
    public class AjouterSerieViewModel : ObservableObject
    {
        public IRelayCommand BtnAddSerie { get; set; }

        private Serie serieToAdd;
        public List<Serie> Series;
        public Serie SerieToAdd
        {
            get { return serieToAdd; }
            set
            {
                serieToAdd = value;
                OnPropertyChanged(nameof(SerieToAdd));
            }
        }
        public AjouterSerieViewModel()
        {
            GetDataOnLoadAsync();
            BtnAddSerie = new RelayCommand(ActionAddSerie);
        }
        public void ActionAddSerie()
        {
            if (SerieToAdd.Titre == null || SerieToAdd.Titre == "")
            {
                MessageAsync("Veuillez entrer un titre", "Erreur de création");
            }
            else if (SerieToAdd.Nbsaisons == null || SerieToAdd.Nbsaisons < 1)
            {
                MessageAsync("Le nombre de saisons doit être supérieur ou égal à 1", "Erreur de création");
            }
            else if (SerieToAdd.Nbepisodes == null || SerieToAdd.Nbepisodes < 1)
            {
                MessageAsync("Le nombre d'épisodes doit être supérieur ou égal à 1", "Erreur de création");
            }
            else
            {
                
            }
        }
        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7192/api/");
            List<Serie> result = await service.GetSeriesAsync("series");
            if (result == null)
            {
                await MessageAsync("API non disponible", "Erreur");
            }
            else
            {
                Series.Clear();
                foreach (var devise in result)
                {
                    Series.Add(devise);
                }
            }
        }
        private async Task MessageAsync(string content, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                XamlRoot = App.MainRoot.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}

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

        private Serie serieToAdd = new Serie();
        public List<Serie> Series = new List<Serie>();
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
            BtnAddSerie = new AsyncRelayCommand(ActionAddSerie);
        }
        public async Task ActionAddSerie()
        {
            if (SerieToAdd.Titre == null || SerieToAdd.Titre == "")
            {
                await MessageAsync("Veuillez entrer un titre", "Erreur de création");
            }
            else if (SerieToAdd.Nbsaisons < 1)
            {
                await MessageAsync("Le nombre de saisons doit être supérieur ou égal à 1", "Erreur de création");
            }
            else if (SerieToAdd.Nbepisodes < 1)
            {
                await MessageAsync("Le nombre d'épisodes doit être supérieur ou égal à 1", "Erreur de création");
            }
            else if (SerieToAdd.Anneecreation < 1900 || SerieToAdd.Anneecreation > DateTime.Now.Year)
            {
                await MessageAsync("L'année de création doit être comprise entre 1900 et l'année en cours", "Erreur de création");
            }
            else
            {
                bool confirm = await ConfirmMessageAsync("Confirmez-vous l'ajout de cette série ?", "Confirmation d'ajout");
                if (confirm)
                {
                    await new WSService("https://localhost:7141/api/").PostSerieAsync("series", SerieToAdd);
                    await MessageAsync("Série ajoutée avec succès", "Succès");
                    SerieToAdd = new Serie();
                }

            }
        }
        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7141/api/");
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
        public async Task MessageAsync(string content, string title)
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

        private async Task<bool> ConfirmMessageAsync(string content, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Annuler",
                PrimaryButtonText = "Confirmer",
                XamlRoot = App.MainRoot.XamlRoot
            };
            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }
    }
}

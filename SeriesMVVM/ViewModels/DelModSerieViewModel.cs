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
    public class DelModSerieViewModel : ObservableObject
    {
        public IRelayCommand BtnShowSerie { get; set; }
        public IRelayCommand BtnModifySerie { get; set; }
        public IRelayCommand BtnDeleteSerie { get; set; }
        private Serie serie = new Serie();
        private int selectedId;
        public List<Serie> Series = new List<Serie>();
        public Serie Serie
        {
            get { return serie; }
            set
            {
                serie = value;
                OnPropertyChanged(nameof(Serie));
            }
        }
        public int SelectedId
        {
            get { return selectedId; }
            set
            {
                selectedId = value;
                OnPropertyChanged(nameof(SelectedId));
            }
        }
        public DelModSerieViewModel()
        {
            GetDataOnLoadAsync();
            BtnShowSerie = new AsyncRelayCommand(ActionShowSerie);
            BtnModifySerie = new AsyncRelayCommand(ActionModifySerie);
            BtnDeleteSerie = new AsyncRelayCommand(ActionDeleteSerie);
        }

        public async Task ActionShowSerie()
        {
            WSService service = new WSService("https://localhost:7141/api/");
            Serie? result = await service.GetSerieAsync("series", SelectedId);
            if (result == null)
            {
                await MessageAsync("Série non trouvée", "Erreur");
            }
            else
            {
                Serie = result;
            }
        }
        public async Task ActionModifySerie()
        {
            WSService service = new WSService("https://localhost:7141/api/");
            if (SelectedId == null)
            {
                await MessageAsync("Veuillez sélectionner une série", "Erreur de modification");
            }
            else if (Serie.Titre == null)
            {
                await MessageAsync("Le titre ne peut pas être vide", "Erreur de modification");
            }
            else if (Serie.Nbsaisons < 1)
            {
                await MessageAsync("Le nombre de saisons doit être supérieur ou égal à 1", "Erreur de modification");
            }
            else if (Serie.Nbepisodes < 1)
            {
                await MessageAsync("Le nombre d'épisodes doit être supérieur ou égal à 1", "Erreur de modification");
            }
            else if (Serie.Anneecreation < 1900 || Serie.Anneecreation > DateTime.Now.Year)
            {
                await MessageAsync("L'année de création doit être comprise entre 1900 et l'année en cours", "Erreur de modification");
            }
            else
            {
                bool confirm = await ConfirmMessageAsync("Confirmez-vous la modification de cette série ?", "Confirmation de modification");
                if (confirm)
                {
                    await service.PutSerieAsync("series", SelectedId, Serie);
                    await MessageAsync("Série modifiée avec succès", "Succès");
                }
            }
        }

        public async Task ActionDeleteSerie()
        {
            WSService service = new WSService("https://localhost:7141/api/");
            if (SelectedId == null)
            {
                await MessageAsync("Veuillez sélectionner une série", "Erreur de suppression");
            }
            else
            {
                bool confirm = await ConfirmMessageAsync("Confirmez-vous la suppression de cette série ?", "Confirmation de suppression");
                if (confirm)
                {
                    await service.DeleteSerieAsync("series", SelectedId);
                    await MessageAsync("Série supprimée avec succès", "Succès");
                    Serie = new Serie();
                    GetDataOnLoadAsync();
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
        protected virtual async Task MessageAsync(string content, string title)
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
        protected virtual async Task<bool> ConfirmMessageAsync(string content, string title)
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

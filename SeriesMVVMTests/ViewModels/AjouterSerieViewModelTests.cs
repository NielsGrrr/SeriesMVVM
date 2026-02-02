using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesMVVM.Services;
using SeriesMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesMVVM.ViewModels.Tests
{
    [TestClass()]
    public class AjouterSerieViewModelTests
    {
        private TestableAjouterSerieViewModel viewModel;
        public class TestableAjouterSerieViewModel : AjouterSerieViewModel
        {
            public string MessageRecu { get; private set; }
            public string TitreRecu { get; private set; }
            public bool ReponseConfirmation { get; set; } = true; // Par défaut on dit OUI

            // On "écrase" (override) la méthode MessageAsync
            protected override Task MessageAsync(string content, string title)
            {
                MessageRecu = content;
                TitreRecu = title;
                return Task.CompletedTask; // On n'ouvre pas de fenêtre
            }

            protected override Task<bool> ConfirmMessageAsync(string content, string title)
            {
                return Task.FromResult(ReponseConfirmation); // On retourne la valeur de ReponseConfirmation
            }

        }
        [TestInitialize()]
        public void Initialize()
        {
            viewModel = new TestableAjouterSerieViewModel();
        }
        [TestMethod()]
        public async Task ActionAddSerieTest_ValidObject_ReturnsCreated()
        {
            // Arrange
            viewModel.SerieToAdd.Titre = "Test Serie";
            viewModel.SerieToAdd.Nbsaisons = 2;
            viewModel.SerieToAdd.Nbepisodes = 20;
            viewModel.SerieToAdd.Anneecreation = 2020;

            // Act
            await viewModel.ActionAddSerie();

            // Assert
            Assert.AreEqual("Succès", viewModel.TitreRecu, "Le titre du message devrait être 'Succès'");
            Assert.AreEqual("Série ajoutée avec succès", viewModel.MessageRecu);

            await viewModel.ActionAddSerie();
        }
    }
}
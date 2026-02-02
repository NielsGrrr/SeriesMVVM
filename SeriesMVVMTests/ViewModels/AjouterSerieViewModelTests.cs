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
        private AjouterSerieViewModel viewModel;
        [TestInitialize()]
        public void Initialize()
        {
            viewModel = new AjouterSerieViewModel();
        }
        [TestMethod()]
        public async Task ActionAddSerieTest_ValidObject_ReturnsCreated()
        {
            // Arrange
            viewModel.SerieToAdd.Titre = "Test Serie";
            viewModel.SerieToAdd.Nbsaisons = 2;
            viewModel.SerieToAdd.Nbepisodes = 20;
            viewModel.SerieToAdd.Anneecreation = 2020;

            string contenuMessage, titreMessage;

            // Act
            await viewModel.ActionAddSerie();

            contenuMessage = viewModel.MessageContent;
            // Assert
            Assert.AreEqual("Succès", titreMessage, "Le titre du message devrait être 'Succès'");
            Assert.AreEqual("Série ajoutée avec succès", contenuMessage);

            await viewModel.ActionAddSerie();
        }
    }
}
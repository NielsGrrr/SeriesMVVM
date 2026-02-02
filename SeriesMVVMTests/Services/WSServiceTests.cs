using ABI.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesMVVM.Models.EntityFramework;
using SeriesMVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SeriesMVVM.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        private WSService service;

        [TestInitialize()]
        public void InitializeTests()
        {
            service = new WSService("https://localhost:7141/api/");


        }

        [TestMethod()]
        public async Task GetSeriesAsyncTest()
        {
            var result = await service.GetSeriesAsync("series");

            Assert.IsInstanceOfType(result, typeof(List<Serie>));
            var listeSeriesRecuperees = result.Where(s => s.Serieid <= 3).OrderBy(s => s.Serieid).ToList();
            List<Serie> series = new List<Serie>
            {
                new Serie {
                    Serieid = 1,
                    Titre = "Scrubs",
                    Resume = "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                    Nbsaisons = 9, Nbepisodes = 184, Anneecreation = 2001, Network = "ABC (US)"
                },
                new Serie {
                    Serieid = 2,
                    Titre = "James May's 20th Century",
                    Resume = "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",
                    Nbsaisons = 1, Nbepisodes = 6, Anneecreation = 2007, Network = "BBC Two"
                },
                new Serie {
                    Serieid = 3,
                    Titre = "True Blood",
                    Resume = "Ayant trouvé un substitut pour se nourrir sans tuer (du sang synthétique), les vampires vivent désormais parmi les humains. Sookie, une serveuse capable de lire dans les esprits, tombe sous le charme de Bill, un mystérieux vampire. Une rencontre qui bouleverse la vie de la jeune femme...",
                    Nbsaisons = 7, Nbepisodes = 81, Anneecreation = 2008, Network = "HBO"
                }
            };
            CollectionAssert.AreEqual(listeSeriesRecuperees, series, "pas les memes listes");
        }

        [TestMethod()]
        public async Task GetSerieAsyncTest()
        {
            var result = await service.GetSerieAsync("series", 1);
            Serie serie = new Serie
            {
                Serieid = 1,
                Titre = "Scrubs",
                Resume = "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                Nbsaisons = 9,
                Nbepisodes = 184,
                Anneecreation = 2001,
                Network = "ABC (US)"
            };
            Assert.IsInstanceOfType(result, typeof(Serie));
            Assert.AreEqual(serie, result, "Les séries ne sont pas identiques.");
        }

        [TestMethod]
        public async Task PostSerieAsyncTest()
        {
            var nouvelleSerie = new Serie
            {
                Titre = "Test Unit MVP",
                Resume = "Test de création",
                Nbsaisons = 1
            };

            bool succes = await service.PostSerieAsync("series", nouvelleSerie);
            Assert.IsTrue(succes, "L'ajout de la série a échoué.");
        }

        [TestMethod]
        public async Task PutSerieAsyncTest()
        {
            Serie serie = await service.GetSerieAsync("series", 1);
            var serieModifiee = new Serie
            {
                Serieid = 1,
                Titre = "Scrubs modifié",
                Resume = "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                Nbsaisons = 9,
                Nbepisodes = 184,
                Anneecreation = 2001,
                Network = "ABC (US)"
            };
            var result = await service.PutSerieAsync("series", serieModifiee.Serieid, serieModifiee);
            await service.PutSerieAsync("series", serie.Serieid, serie);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result, "La modification de la série a échoué.");
        }
    }
}
using ABI.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private readonly HttpClient client = new HttpClient();

        [TestInitialize()]
        public void InitializeTests()
        {
            
        }
        [TestMethod()]
        public void GetSeriesAsyncTest()
        {
            
        }
    }
}
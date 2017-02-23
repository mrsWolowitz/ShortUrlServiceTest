using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortUrlServiceWeb.Controllers;
using ShortUrlService.Service;
using Moq;
using ShortUrlService.Model;
using ShortUrlServiceWeb.ViewModels;
using ShortUrlServiceWeb.Mappings;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace ShortUrlServiceWeb.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        public List<HistoryRecord> historyRecords => new List<HistoryRecord> {
                        new HistoryRecord {
                        Id = 5,
                        UrlShort = "http://localhost:5864/Home/Record/60ral78pv5",
                        UrlLong = "https://fake/page/1",
                        CreateDate = DateTime.Now,
                        HitCount = 0},
                        new HistoryRecord {
                        Id = 6,
                        UrlShort = "http://localhost:5864/Home/Record/70y856ilox",
                        UrlLong = "https://fake/page/1",
                        CreateDate = DateTime.Now,
                        HitCount = 0},
                        };


        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/values");
            //var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            //var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "values" } });
            var route = config.Routes.MapHttpRoute("FakeApi", "fake/{page}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "page", "1" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void Get_Records_Sucsess()
        {
            // Arrange
            Mock<IHistoryRecordService> mock = new Mock<IHistoryRecordService>();
            mock.Setup(m => m.GetHistoryRecords()).Returns(historyRecords);
            var historyRecordService = mock.Object;

            var controller = new RecordsController(historyRecordService);

            AutoMapperConfiguration.Configure();

            // Act
            IEnumerable<HistoryRecordViewModel> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

        }

        [TestMethod]
        public void Get_Url_Sucsess()
        {
            // Arrange
            Mock<IHistoryRecordService> mock = new Mock<IHistoryRecordService>();
            mock.Setup(m => m.GetHistoryRecord(It.IsAny<long>())).Returns(historyRecords[1]);
            var historyRecordService = mock.Object;

            var controller = new RecordsController(historyRecordService);
            SetupControllerForTests(controller);

            string hash = "70y856ilox";

            // Act
            RedirectResult result = (RedirectResult)controller.Get(hash);

            // Assert
            //Assert.AreEqual(System.Net.HttpStatusCode.Moved, result.StatusCode);
            Assert.AreEqual("https://fake/page/1", result.Location.AbsoluteUri);
        }

        [TestMethod, ExpectedException(typeof(HttpResponseException))]
        public void Get_Url_Throw_BadRequest()
        {
            // Arrange
            Mock<IHistoryRecordService> mock = new Mock<IHistoryRecordService>();
            mock.Setup(m => m.GetHistoryRecord(It.IsAny<long>())).Returns(historyRecords[1]);
            var historyRecordService = mock.Object;

            var controller = new RecordsController(historyRecordService);
            SetupControllerForTests(controller);

            string hash = "";

            // Act
            try
            {
                var result = controller.Get(hash);
            }

            // Assert
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, ex.Response.StatusCode);
                throw;
            }
        }

        [TestMethod, ExpectedException(typeof(HttpResponseException))]
        public void Get_Url_Throw_NotFound()
        {
            // Arrange
            Mock<IHistoryRecordService> mock = new Mock<IHistoryRecordService>();
            mock.Setup(m => m.GetHistoryRecord(It.IsAny<long>())).Returns((HistoryRecord)null);
            var historyRecordService = mock.Object;

            var controller = new RecordsController(historyRecordService);
            SetupControllerForTests(controller);

            string hash = "70y856ilox";

            // Act
            try
            {
                var result = controller.Get(hash);
            }

            // Assert
            catch (HttpResponseException ex)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.NotFound, ex.Response.StatusCode);
                throw;
            }
        }


        [TestMethod]
        public void Add_Url_Sucsess()
        {
            // Arrange
            List<HistoryRecord> historyRecords = new List<HistoryRecord> {
                        new HistoryRecord {
                        Id = 5,
                        UrlShort = "http://localhost:5864/Home/Record/60ral78pv5",
                        UrlLong = "https://fake/page/1",
                        CreateDate = DateTime.Now,
                        HitCount = 0},
                        new HistoryRecord {
                        Id = 6,
                        UrlShort = "http://localhost:5864/Home/Record/70y856ilox",
                        UrlLong = "https://fake/page/1",
                        CreateDate = DateTime.Now,
                        HitCount = 0},
                        };
            Mock<IHistoryRecordService> mock = new Mock<IHistoryRecordService>();
            mock.Setup(m => m.CreateHistoryRecord(It.IsAny<HistoryRecord>()))
                .Callback<HistoryRecord>(c =>
                {
                    c.Id = 7;
                    c.CreateDate = DateTime.Now;
                    historyRecords.Add(c);
                });

            mock.Setup(m => m.UpdateHistoryRecord(It.IsAny<HistoryRecord>()))
                .Callback<HistoryRecord>(u =>
                {
                    var original = historyRecords.Single(q => q.Id == u.Id);
                    original.Id = u.Id;
                    original.UrlShort = u.UrlShort;
                }).Verifiable();
            var historyRecordService = mock.Object;

            AutoMapperConfiguration.Configure();
            HistoryRecordViewModel viewModelHistoryRecord = new HistoryRecordViewModel
            {
                UrlLong = "https://fake/page/1"
            };

            var controller = new RecordsController(historyRecordService);
            SetupControllerForTests(controller);

            // Act
            controller.Post(viewModelHistoryRecord);

            // Assert
            Assert.AreEqual(3, historyRecords.Count());            
        }


    }
}

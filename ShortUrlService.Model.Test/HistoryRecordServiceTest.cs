using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortUrlService.Data.Infrastructure;
using System.Collections.Generic;
using Moq;
using System.Linq;
using ShortUrlService.Service;
using ShortUrlService.Data.Repositories;
using ShortUrlService.Model;

namespace ShortUrlService.Service.Test
{
    [TestClass]
    public class HistoryRecordServiceTest
    {
        private IHistoryRecordRepository _CreateMockRepository()
        {
            var historyRecords = new List<HistoryRecord> {
        new HistoryRecord {
                        Id = 1,
                        UrlShort = "http://localhost:5864/Home/Record/60ral78pv5",
                        UrlLong = "https://fake/page/1",
                        CreateDate = DateTime.Now,
                        HitCount = 0},
                        new HistoryRecord {
                        Id = 2,
                        UrlShort = "http://localhost:5864/Home/Record/70y856ilox",
                        UrlLong = "https://fake/page/1",
                        CreateDate = DateTime.Now,
                        HitCount = 1}
        };

            Mock<IHistoryRecordRepository> mock = new Mock<IHistoryRecordRepository>();

            mock.Setup(m => m.GetAll()).Returns(historyRecords);

            mock.Setup(m => m.Add(It.IsAny<HistoryRecord>()))
                .Callback<HistoryRecord>(c =>
                {
                    c.Id = historyRecords.Count + 1;
                    c.CreateDate = DateTime.Now;
                    historyRecords.Add(c);
                }).Verifiable();

            mock.Setup(m => m.GetById(It.IsAny<long>()))
                .Returns<long>(c => historyRecords.FirstOrDefault(q => q.Id == c));

            mock.Setup(m => m.Update(It.IsAny<HistoryRecord>()))
                .Callback<HistoryRecord>(u => {
                    var original = historyRecords.Single(q => q.Id == u.Id);
                    original.Id = u.Id;
                    original.UrlShort = u.UrlShort;
                }).Verifiable();

            return mock.Object;
        }

        private IUnitOfWork _CreateMockUnitOfWork()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Commit());
            return mock.Object;
        }

        [TestMethod]
        public void GetHistoryRecords()
        {
            // Arrange
            var mockRepository = _CreateMockRepository();
            var mockUnitOfWork = _CreateMockUnitOfWork();
            HistoryRecordService model = new HistoryRecordService(mockRepository, mockUnitOfWork);

            // Act
            var result = model.GetHistoryRecords();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetHistoryRecord()
        {
            // Arrange
            var mockRepository = _CreateMockRepository();
            var mockUnitOfWork = _CreateMockUnitOfWork();
            HistoryRecordService model = new HistoryRecordService(mockRepository, mockUnitOfWork);
            int id = 2;

            // Act
            var result = model.GetHistoryRecord(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public void CreateHistoryRecord()
        {
            // Arrange
            var mockRepository = _CreateMockRepository();
            var mockUnitOfWork = _CreateMockUnitOfWork();
            HistoryRecordService model = new HistoryRecordService(mockRepository, mockUnitOfWork);
            int expectedCount = 3;
            HistoryRecord historyRecord = new HistoryRecord
            {
                CreateDate = DateTime.Now,
                UrlShort = $"hhhhhh/rrrrr",
                UrlLong = "http://stackoverflow.com"
            };

            // Act
            model.CreateHistoryRecord(historyRecord);

            // Assert
            Assert.AreEqual(expectedCount, model.GetHistoryRecords().Count());
        }

        [TestMethod]
        public void UpdateHistoryRecord()
        {
            // Arrange
            var mockRepository = _CreateMockRepository();
            var mockUnitOfWork = _CreateMockUnitOfWork();
            HistoryRecordService model = new HistoryRecordService(mockRepository, mockUnitOfWork);
            long id = 2;
            string urlShort = "changed";

            HistoryRecord historyRecord = new HistoryRecord
            {
                Id = id,
                CreateDate = DateTime.Now,
                UrlShort = urlShort,
                UrlLong = "http://stackoverflow.com"
            };

            // Act
            model.UpdateHistoryRecord(historyRecord);

            // Assert
            Assert.AreEqual(urlShort, model.GetHistoryRecord(id).UrlShort);
        }

    }
}

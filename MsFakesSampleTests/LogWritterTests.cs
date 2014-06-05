using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsFakesSample;

namespace MsFakesSampleTests
{
    [TestClass]
    public class LogWritterTests
    {
        [TestMethod]
        public void WriteTest()
        {
            const string logData = "test message";

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => new DateTime(2012, 6, 5);

                var fileWriter = new MsFakesSample.Fakes.StubIFileWriter();
                var dataProvider = new MsFakesSample.Fakes.StubIDataProvider
                {
                    GetData = () => logData
                };

                var logWritter = new LogWritter(dataProvider, fileWriter);

                var expected = new Log(logData);
                var actual = logWritter.AddLog();

                Assert.AreEqual(expected.Message, actual.Message);
                Assert.AreEqual(expected.CreatedDate, actual.CreatedDate);                
            }
        }
    }
}



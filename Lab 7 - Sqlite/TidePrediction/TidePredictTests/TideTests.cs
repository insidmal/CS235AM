using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TideDBClasses;
using NUnit.Framework;
using XMLtoSQLLiteConverter;
using TidePredictionClasses;
using System.IO;

namespace TidePredictTests
{
    [TestFixture]
    public class TideTests {

        string dir = System.AppDomain.CurrentDomain.BaseDirectory + "/Files/TidesDB.s3db";

        TideDbAccess tideDb;

        [SetUp]
        public void SetUpTest()
        {
            tideDb = new TideDbAccess(dir);
        }

        [Test]
        public void TestSetup()
        {
            tideDb.Create();
            Assert.Greater(tideDb.CountEntries(), 5);
        }

        [Test]
        public void TestAdd()
        {


            Assert.Greater(tideDb.CountEntries(), 5555555555);

        }



    }
}

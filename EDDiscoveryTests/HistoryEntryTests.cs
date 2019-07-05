using EliteDangerousCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EDDiscoveryTests
{

    [TestFixture(TestOf = typeof(HistoryEntry))]
    public class HistoryEntryTests
    {
        HistoryEntry he;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith("historyentry.json"));
            String jsonEntry;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                jsonEntry = reader.ReadToEnd();
            }
            he = HistoryEntry.FromJSON(jsonEntry);
        }

        [Test]
        public void testShipInformation()
        {
            Assert.That(he.ShipInformation.ShipType, Is.EqualTo("Anaconda"));
        }

        [Test]
        public void testThereIsOneCurrentMission()
        {
            Assert.That(he.MissionList.GetAllCurrentMissions(DateTime.Parse("2019-06-09T00:30:18Z")).Count, Is.EqualTo(1));
        }

    }
}

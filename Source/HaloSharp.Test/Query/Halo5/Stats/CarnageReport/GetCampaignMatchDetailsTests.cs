﻿using System;
using System.IO;
using System.Threading.Tasks;
using HaloSharp.Exception;
using HaloSharp.Extension;
using HaloSharp.Model;
using HaloSharp.Model.Halo5.Stats.CarnageReport;
using HaloSharp.Query.Halo5.Stats.CarnageReport;
using HaloSharp.Test.Config;
using HaloSharp.Test.Utility;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;

namespace HaloSharp.Test.Query.Halo5.Stats.CarnageReport
{
    [TestFixture]
    public class GetCampaignMatchDetailsTests
    {
        private IHaloSession _mockSession;
        private CampaignMatch _campaignMatch;

        [SetUp]
        public void Setup()
        {
            _campaignMatch = JsonConvert.DeserializeObject<CampaignMatch>(File.ReadAllText(Halo5Config.CampaignMatchJsonPath));

            var mock = new Mock<IHaloSession>();
            mock.Setup(m => m.Get<CampaignMatch>(It.IsAny<string>()))
                .ReturnsAsync(_campaignMatch);

            _mockSession = mock.Object;
        }

        [Test]
        [TestCase("71fe5636-9db1-41fe-871d-515cd5e0ed87")]
        public void Uri_MatchesExpected(string guid)
        {
            var query = new GetCampaignMatchDetails(new Guid(guid));

            Assert.AreEqual($"https://www.haloapi.com/stats/h5/campaign/matches/{guid}", query.Uri);
        }

        [Test]
        [TestCase("71fe5636-9db1-41fe-871d-515cd5e0ed87")]
        public async Task Query_DoesNotThrow(string guid)
        {
            var query = new GetCampaignMatchDetails(new Guid(guid))
                .SkipCache();

            var result = await _mockSession.Query(query);

            Assert.IsInstanceOf(typeof(CampaignMatch), result);
            Assert.AreEqual(_campaignMatch, result);
        }

        [Test]
        [TestCase("71fe5636-9db1-41fe-871d-515cd5e0ed87")]
        public async Task GetCampaignMatchDetails_DoesNotThrow(string guid)
        {
            var query = new GetCampaignMatchDetails(new Guid(guid))
                .SkipCache();

            var result = await Global.Session.Query(query);

            Assert.IsInstanceOf(typeof(CampaignMatch), result);
        }

        [Test]
        [TestCase("71fe5636-9db1-41fe-871d-515cd5e0ed87")]
        public async Task GetCampaignMatchDetails_SchemaIsValid(string guid)
        {
            var weaponsSchema = JSchema.Parse(File.ReadAllText(Halo5Config.CampaignMatchJsonSchemaPath), new JSchemaReaderSettings
            {
                Resolver = new JSchemaUrlResolver(),
                BaseUri = new Uri(Path.GetFullPath(Halo5Config.CampaignMatchJsonSchemaPath))
            });

            var query = new GetCampaignMatchDetails(new Guid(guid))
                .SkipCache();

            var jArray = await Global.Session.Get<JObject>(query.Uri);

            SchemaUtility.AssertSchemaIsValid(weaponsSchema, jArray);
        }

        [Test]
        [TestCase("71fe5636-9db1-41fe-871d-515cd5e0ed87")]
        public async Task GetCampaignMatchDetails_ModelMatchesSchema(string guid)
        {
            var schema = JSchema.Parse(File.ReadAllText(Halo5Config.CampaignMatchJsonSchemaPath), new JSchemaReaderSettings
            {
                Resolver = new JSchemaUrlResolver(),
                BaseUri = new Uri(Path.GetFullPath(Halo5Config.CampaignMatchJsonSchemaPath))
            });

            var query = new GetCampaignMatchDetails(new Guid(guid))
                .SkipCache();

            var result = await Global.Session.Query(query);

            var json = JsonConvert.SerializeObject(result);
            var jContainer = JsonConvert.DeserializeObject<JContainer>(json);

            SchemaUtility.AssertSchemaIsValid(schema, jContainer);
        }

        [Test]
        [TestCase("71fe5636-9db1-41fe-871d-515cd5e0ed87")]
        public async Task GetCampaignMatchDetails_IsSerializable(string guid)
        {
            var query = new GetCampaignMatchDetails(new Guid(guid))
                .SkipCache();

            var result = await Global.Session.Query(query);

            SerializationUtility<CampaignMatch>.AssertRoundTripSerializationIsPossible(result);
        }

        [Test]
        public async Task GetCampaignMatchDetails_InvalidGuid(string guid)
        {
            var query = new GetCampaignMatchDetails(Guid.NewGuid())
                .SkipCache();

            try
            {
                await Global.Session.Query(query);
                Assert.Fail("An exception should have been thrown");
            }
            catch (HaloApiException e)
            {
                Assert.AreEqual((int)Enumeration.Halo5.StatusCode.NotFound, e.HaloApiError.StatusCode);
            }
            catch (System.Exception e)
            {
                Assert.Fail("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message);
            }
        }
    }
}
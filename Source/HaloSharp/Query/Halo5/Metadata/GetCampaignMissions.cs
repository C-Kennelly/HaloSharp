﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HaloSharp.Model.Halo5.Metadata;

namespace HaloSharp.Query.Halo5.Metadata
{
    public class GetCampaignMissions : IQuery<List<CampaignMission>>
    {
        private bool _useCache = true;

        public GetCampaignMissions SkipCache()
        {
            _useCache = false;

            return this;
        }

        public async Task<List<CampaignMission>> ApplyTo(IHaloSession session)
        {
            var uri = GetConstructedUri();

            var campaignMissions = _useCache
                ? Cache.Get<List<CampaignMission>>(uri)
                : null;

            if (campaignMissions == null)
            {
                campaignMissions = await session.Get<List<CampaignMission>>(uri);

                Cache.AddMetadata(uri, campaignMissions);
            }

            return campaignMissions;
        }

        public string GetConstructedUri()
        {
            var builder = new StringBuilder("metadata/h5/metadata/campaign-missions");

            return builder.ToString();
        }
    }
}
﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HaloSharp.Model.Metadata;

namespace HaloSharp.Query.Metadata
{
    /// <summary>
    ///     Construct a query to retrieve detailed Flexible Stat Metadata. Use them to translate IDs from other APIs.
    /// </summary>
    public class GetFlexibleStats : IQuery<List<FlexibleStat>>
    {
        private const string CacheKey = "FlexibleStats";

        private bool _useCache = true;

        public GetFlexibleStats SkipCache()
        {
            _useCache = false;

            return this;
        }

        public async Task<List<FlexibleStat>> ApplyTo(IHaloSession session)
        {
            var flexibleStats = _useCache
                ? Cache.Get<List<FlexibleStat>>(CacheKey)
                : null;

            if (flexibleStats != null)
            {
                return flexibleStats;
            }

            flexibleStats = await session.Get<List<FlexibleStat>>(GetConstructedUri());

            Cache.Add(CacheKey, flexibleStats);

            return flexibleStats;
        }

        public string GetConstructedUri()
        {
            var builder = new StringBuilder("metadata/h5/metadata/flexible-stats");

            return builder.ToString();
        }
    }
}
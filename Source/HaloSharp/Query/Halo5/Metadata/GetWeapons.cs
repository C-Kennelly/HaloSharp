﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HaloSharp.Model.Halo5.Metadata;

namespace HaloSharp.Query.Halo5.Metadata
{
    public class GetWeapons : IQuery<List<Weapon>>
    {
        private bool _useCache = true;

        public GetWeapons SkipCache()
        {
            _useCache = false;

            return this;
        }

        public async Task<List<Weapon>> ApplyTo(IHaloSession session)
        {
            var uri = GetConstructedUri();

            var weapons = _useCache
                ? Cache.Get<List<Weapon>>(uri)
                : null;

            if (weapons == null)
            {
                weapons = await session.Get<List<Weapon>>(uri);

                Cache.AddMetadata(uri, weapons);
            }

            return weapons;
        }

        public string GetConstructedUri()
        {
            var builder = new StringBuilder("metadata/h5/metadata/weapons");

            return builder.ToString();
        }
    }
}
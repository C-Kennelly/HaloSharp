﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaloSharp.Query.Halo5Forge.Stats.Lifetime
{
    public class GetCustomServiceRecord : Halo5.Stats.Lifetime.GetCustomServiceRecord
    {
        public GetCustomServiceRecord(string gamertag) : base(gamertag) { }

        public GetCustomServiceRecord(IEnumerable<string> gamertags) : base(gamertags) { }

        public override string GetConstructedUri()
        {
            var builder = new StringBuilder("stats/h5pc/servicerecords/custom");

            if (Parameters.Any())
            {
                builder.Append("?");
                builder.Append(string.Join("&", Parameters.Select(p => $"{p.Key}={p.Value}")));
            }

            return builder.ToString();
        }
    }
}
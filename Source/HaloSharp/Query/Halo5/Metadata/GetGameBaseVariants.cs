﻿using HaloSharp.Model.Halo5.Metadata;
using System.Collections.Generic;

namespace HaloSharp.Query.Halo5.Metadata
{
    public class GetGameBaseVariants : Query<List<GameBaseVariant>>
    {
        public override string Uri => HaloUriBuilder.Build("metadata/h5/metadata/game-base-variants");
    }
}
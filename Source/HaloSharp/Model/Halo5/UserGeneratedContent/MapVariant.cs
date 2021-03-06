﻿using System;
using System.Collections.Generic;
using System.Linq;
using HaloSharp.Model.Common;
using HaloSharp.Model.Halo5.Common;
using Newtonsoft.Json;

namespace HaloSharp.Model.Halo5.UserGeneratedContent
{
    [Serializable]
    public class MapVariant : IEquatable<MapVariant>
    {
        [JsonProperty(PropertyName = "BaseMap")]
        public Variant BaseMap { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "AccessControl")]
        public Enumeration.Halo5.AccessControl AccessControl { get; set; }

        [JsonProperty(PropertyName = "Links")]
        public Dictionary<string, Link> Links { get; set; }

        [JsonProperty(PropertyName = "CreationTimeUtc")]
        public ISO8601 CreationTimeUtc { get; set; }

        [JsonProperty(PropertyName = "LastModifiedTimeUtc")]
        public ISO8601 LastModifiedTimeUtc { get; set; }

        [JsonProperty(PropertyName = "Banned")]
        public bool Banned { get; set; }

        [JsonProperty(PropertyName = "Identity")]
        public Variant Identity { get; set; }

        [JsonProperty(PropertyName = "Stats")]
        public Common.Stats Stats { get; set; }

        public bool Equals(MapVariant other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(BaseMap, other.BaseMap)
                && string.Equals(Name, other.Name)
                && string.Equals(Description, other.Description)
                && AccessControl == other.AccessControl
                && Links.OrderBy(l => l.Key).SequenceEqual(other.Links.OrderBy(l => l.Key))
                && Equals(CreationTimeUtc, other.CreationTimeUtc)
                && Equals(LastModifiedTimeUtc, other.LastModifiedTimeUtc)
                && Banned == other.Banned
                && Equals(Identity, other.Identity)
                && Equals(Stats, other.Stats);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(MapVariant))
            {
                return false;
            }

            return Equals((MapVariant) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (BaseMap != null ? BaseMap.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Description?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (int) AccessControl;
                hashCode = (hashCode*397) ^ (Links?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (CreationTimeUtc != null ? CreationTimeUtc.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastModifiedTimeUtc != null ? LastModifiedTimeUtc.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Banned.GetHashCode();
                hashCode = (hashCode*397) ^ (Identity != null ? Identity.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Stats != null ? Stats.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(MapVariant left, MapVariant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MapVariant left, MapVariant right)
        {
            return !Equals(left, right);
        }
    }
}

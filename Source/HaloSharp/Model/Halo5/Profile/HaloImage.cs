﻿using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace HaloSharp.Model.Halo5.Profile
{
    [Serializable]
    public class HaloImage : IEquatable<HaloImage>
    {
        [JsonProperty(PropertyName = "Uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "Image")]
        public Image Image { get; set; }

        public bool Equals(HaloImage other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            string firstBitmap;
            string secondBitmap;
            using (var memoryStream = new MemoryStream())
            {
                Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                firstBitmap = Convert.ToBase64String(memoryStream.ToArray());

                memoryStream.Position = 0;

                other.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                secondBitmap = Convert.ToBase64String(memoryStream.ToArray());
            }

            return string.Equals(Uri, other.Uri)
                && firstBitmap.Equals(secondBitmap);
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

            if (obj.GetType() != typeof (HaloImage))
            {
                return false;
            }

            return Equals((HaloImage) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Image?.GetHashCode() ?? 0)*397) ^ (Uri?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(HaloImage left, HaloImage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HaloImage left, HaloImage right)
        {
            return !Equals(left, right);
        }
    }
}

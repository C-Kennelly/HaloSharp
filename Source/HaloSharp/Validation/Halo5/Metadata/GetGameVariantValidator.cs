﻿using System;
using HaloSharp.Exception;
using HaloSharp.Model;
using HaloSharp.Query.Halo5.Metadata;

namespace HaloSharp.Validation.Halo5.Metadata
{
    public static class GetGameVariantValidator
    {
        public static void Validate(this GetGameVariant query)
        {
            var validationResult = new ValidationResult();

            if (query.GameVariantId == default(Guid))
            {
                validationResult.Messages.Add("GetGameVariant query requires a GameVariantId to be set.");
            }

            if (!validationResult.Success)
            {
                throw new ValidationException(validationResult.Messages);
            }
        }
    }
}

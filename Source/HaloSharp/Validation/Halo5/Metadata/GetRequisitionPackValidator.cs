﻿using System;
using HaloSharp.Exception;
using HaloSharp.Model;
using HaloSharp.Query.Halo5.Metadata;

namespace HaloSharp.Validation.Halo5.Metadata
{
    public static class GetRequisitionPackValidator
    {
        public static void Validate(this GetRequisitionPack query)
        {
            var validationResult = new ValidationResult();

            if (query.RequisitionPackId == default(Guid))
            {
                validationResult.Messages.Add("GetRequisitionPack query requires a RequisitionPackId to be set.");
            }

            if (!validationResult.Success)
            {
                throw new ValidationException(validationResult.Messages);
            }
        }
    }
}

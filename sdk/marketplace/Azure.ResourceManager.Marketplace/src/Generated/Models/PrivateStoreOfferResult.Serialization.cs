// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Text.Json;
using Azure;
using Azure.Core;

namespace Azure.ResourceManager.Marketplace.Models
{
    public partial class PrivateStoreOfferResult
    {
        internal static PrivateStoreOfferResult DeserializePrivateStoreOfferResult(JsonElement element)
        {
            Optional<string> uniqueOfferId = default;
            Optional<string> offerDisplayName = default;
            Optional<string> publisherDisplayName = default;
            Optional<ETag> eTag = default;
            Optional<Guid> privateStoreId = default;
            Optional<DateTimeOffset> createdAt = default;
            Optional<DateTimeOffset> modifiedAt = default;
            Optional<IReadOnlyList<string>> specificPlanIdsLimitation = default;
            Optional<bool> updateSuppressedDueIdempotence = default;
            Optional<IReadOnlyDictionary<string, Uri>> iconFileUris = default;
            Optional<IReadOnlyList<PrivateStorePlan>> plans = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("uniqueOfferId"))
                {
                    uniqueOfferId = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("offerDisplayName"))
                {
                    offerDisplayName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("publisherDisplayName"))
                {
                    publisherDisplayName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("eTag"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    eTag = new ETag(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("privateStoreId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    privateStoreId = property.Value.GetGuid();
                    continue;
                }
                if (property.NameEquals("createdAt"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    createdAt = property.Value.GetDateTimeOffset("O");
                    continue;
                }
                if (property.NameEquals("modifiedAt"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    modifiedAt = property.Value.GetDateTimeOffset("O");
                    continue;
                }
                if (property.NameEquals("specificPlanIdsLimitation"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<string> array = new List<string>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(item.GetString());
                    }
                    specificPlanIdsLimitation = array;
                    continue;
                }
                if (property.NameEquals("updateSuppressedDueIdempotence"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    updateSuppressedDueIdempotence = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("iconFileUris"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    Dictionary<string, Uri> dictionary = new Dictionary<string, Uri>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, new Uri(property0.Value.GetString()));
                    }
                    iconFileUris = dictionary;
                    continue;
                }
                if (property.NameEquals("plans"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<PrivateStorePlan> array = new List<PrivateStorePlan>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(PrivateStorePlan.DeserializePrivateStorePlan(item));
                    }
                    plans = array;
                    continue;
                }
            }
            return new PrivateStoreOfferResult(uniqueOfferId.Value, offerDisplayName.Value, publisherDisplayName.Value, Optional.ToNullable(eTag), Optional.ToNullable(privateStoreId), Optional.ToNullable(createdAt), Optional.ToNullable(modifiedAt), Optional.ToList(specificPlanIdsLimitation), Optional.ToNullable(updateSuppressedDueIdempotence), Optional.ToDictionary(iconFileUris), Optional.ToList(plans));
        }
    }
}

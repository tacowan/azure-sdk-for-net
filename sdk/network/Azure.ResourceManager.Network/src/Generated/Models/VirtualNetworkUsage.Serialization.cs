// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Network.Models
{
    public partial class VirtualNetworkUsage
    {
        internal static VirtualNetworkUsage DeserializeVirtualNetworkUsage(JsonElement element)
        {
            Optional<double> currentValue = default;
            Optional<ResourceIdentifier> id = default;
            Optional<double> limit = default;
            Optional<VirtualNetworkUsageName> name = default;
            Optional<string> unit = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("currentValue"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    currentValue = property.Value.GetDouble();
                    continue;
                }
                if (property.NameEquals("id"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    id = new ResourceIdentifier(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("limit"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    limit = property.Value.GetDouble();
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    name = VirtualNetworkUsageName.DeserializeVirtualNetworkUsageName(property.Value);
                    continue;
                }
                if (property.NameEquals("unit"))
                {
                    unit = property.Value.GetString();
                    continue;
                }
            }
            return new VirtualNetworkUsage(Optional.ToNullable(currentValue), id.Value, Optional.ToNullable(limit), name.Value, unit.Value);
        }
    }
}

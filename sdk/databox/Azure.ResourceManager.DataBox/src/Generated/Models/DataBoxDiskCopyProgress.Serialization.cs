// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.DataBox.Models
{
    public partial class DataBoxDiskCopyProgress
    {
        internal static DataBoxDiskCopyProgress DeserializeDataBoxDiskCopyProgress(JsonElement element)
        {
            Optional<string> serialNumber = default;
            Optional<long> bytesCopied = default;
            Optional<int> percentComplete = default;
            Optional<DataBoxCopyStatus> status = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("serialNumber"))
                {
                    serialNumber = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("bytesCopied"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    bytesCopied = property.Value.GetInt64();
                    continue;
                }
                if (property.NameEquals("percentComplete"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    percentComplete = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("status"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    status = new DataBoxCopyStatus(property.Value.GetString());
                    continue;
                }
            }
            return new DataBoxDiskCopyProgress(serialNumber.Value, Optional.ToNullable(bytesCopied), Optional.ToNullable(percentComplete), Optional.ToNullable(status));
        }
    }
}

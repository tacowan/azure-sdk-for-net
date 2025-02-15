// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.DataFactory.Models
{
    public partial class ScriptActivityTypePropertiesLogSettings : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("logDestination");
            writer.WriteStringValue(LogDestination.ToString());
            if (Optional.IsDefined(LogLocationSettings))
            {
                writer.WritePropertyName("logLocationSettings");
                writer.WriteObjectValue(LogLocationSettings);
            }
            writer.WriteEndObject();
        }

        internal static ScriptActivityTypePropertiesLogSettings DeserializeScriptActivityTypePropertiesLogSettings(JsonElement element)
        {
            ScriptActivityLogDestination logDestination = default;
            Optional<LogLocationSettings> logLocationSettings = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("logDestination"))
                {
                    logDestination = new ScriptActivityLogDestination(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("logLocationSettings"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    logLocationSettings = LogLocationSettings.DeserializeLogLocationSettings(property.Value);
                    continue;
                }
            }
            return new ScriptActivityTypePropertiesLogSettings(logDestination, logLocationSettings.Value);
        }
    }
}

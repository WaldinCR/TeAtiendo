using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Services.Json
{
    public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string Format = "HH:mm:ss";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            if (string.IsNullOrWhiteSpace(s)) return default;

            if (TimeOnly.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var t))
                return t;

            return TimeOnly.ParseExact(s, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
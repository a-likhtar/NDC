using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using NDC.Domain.Enums;
using NDC.Domain.Models;

namespace NDC.Domain.JsonConverters;

public class MeteoriteConverter : JsonConverter<MeteoriteDto>
{
    public override MeteoriteDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var meteoriteDto = new MeteoriteDto();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return meteoriteDto;

            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;

            var propertyName = reader.GetString();
            reader.Read();
            switch (propertyName)
            {
                case "id":
                    meteoriteDto.MeteoriteId = int.TryParse(reader.GetString(), out var id)
                        ? id
                        : throw new JsonException("Invalid id");
                    break;
                case "nametype":
                    meteoriteDto.NameType = Enum.TryParse<NameType>(reader.GetString(), out var nameType)
                        ? nameType
                        : throw new JsonException("Invalid name type");
                    break;
                case "falltype":
                    meteoriteDto.FallType = Enum.TryParse<FallType>(reader.GetString(), out var fallType)
                        ? fallType
                        : throw new JsonException("Invalid fall type");
                    break;
                case "mass":
                    meteoriteDto.Mass = double.TryParse(reader.GetString(), NumberStyles.Float,
                        CultureInfo.InvariantCulture, out var mass)
                        ? mass
                        : throw new JsonException("Invalid mass");
                    break;
                case "year":
                    meteoriteDto.ObservationYear = DateOnly.TryParse(reader.GetString(), out var date)
                        ? date
                        : throw new JsonException("Invalid date");
                    break;
                case "reclat":
                    meteoriteDto.Reclat = Decimal.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture,out var reclat)
                        ? reclat
                        : throw new JsonException("Invalid Reclat");
                    break;
                case "reclong":
                    meteoriteDto.Reclong = Decimal.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var reclong)
                        ? reclong
                        : throw new JsonException("Invalid Reclong");
                    break;
                case ":@computed_region_cbhk_fwbd":
                    meteoriteDto.ComputedRegionCbhk = int.TryParse(reader.GetString(), out var chhk)
                        ? chhk
                        : throw new JsonException("Invalid Cbhk");
                    break;
                case ":@computed_region_nnqa_25f4":
                    meteoriteDto.ComputedRegionNnqa = int.TryParse(reader.GetString(), out var nnqa)
                        ? nnqa
                        : throw new JsonException("Invalid Nnqa");
                    break;
                case "name":
                    meteoriteDto.Name = reader.GetString() ?? string.Empty;
                    break;
                case "recclass":
                    meteoriteDto.MeteoriteClass = reader.GetString() ?? string.Empty;
                    break;
                case "geolocation":
                    meteoriteDto.GeoLocation = JsonSerializer.Deserialize<GeoLocation>(ref reader, JsonSerializerOptions.Default);
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }

        return meteoriteDto;
    }

    public override void Write(Utf8JsonWriter writer, MeteoriteDto value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
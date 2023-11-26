using System.Text.Json;
using System.Text.Json.Serialization;

struct Config
{
    public string Cookie { get; set; }

    public int Year { get; set; }
    
    [JsonConverter(typeof(DaysConverter))]
    public int[] Days { get; set; }

    private void SetDefaults()
    {
        //Make sure we're looking at EST, or it might break for most of the US
        var currentEst = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
        if (Cookie == default) Cookie = "";
        if (Year == default) Year = currentEst.Year;
        if (Days == default(int[])) Days = (currentEst.Month == 12 && currentEst.Day <= 25) ? [currentEst.Day] : [0];
    }

    public static Config Get(string path = "config.json")
    {
        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        Config config;
        if (File.Exists(path))
        {
            config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path), options);
            config.SetDefaults();
        }
        else
        {
            config = new Config();
            config.SetDefaults();
            File.WriteAllText(path, JsonSerializer.Serialize(config, options));
        }

        return config;
    }
}

class DaysConverter : JsonConverter<int[]>
{
    public override int[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IEnumerable<string> tokens;

        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return [reader.GetInt16()];

            case JsonTokenType.String:
                tokens = new string[] { reader.GetString() ?? "" };
                break;

            default:
                var obj = JsonSerializer
                    .Deserialize<object[]>(ref reader);

                tokens = obj != null
                    ? obj.Select(o => o.ToString() ?? "")
                    : Array.Empty<string>();
                break;
        }

        var days = tokens.SelectMany(ParseString);
        if (days.Contains(0)) return [0];

        return days.Where(v => v < 26 && v > 0).OrderBy(day => day).ToArray();
    }

    private IEnumerable<int> ParseString(string str)
    {
        return str.Split(",").SelectMany(str =>
        {
            if (str.Contains(".."))
            {
                var split = str.Split("..");
                int start = int.Parse(split[0]);
                int stop = int.Parse(split[1]);
                return Enumerable.Range(start, stop - start + 1);
            }
            else if (int.TryParse(str, out int day))
            {
                return new int[] { day };
            }

            return Array.Empty<int>();
        });
    }

    public override void Write(Utf8JsonWriter writer, int[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (int val in value) writer.WriteNumberValue(val);
        writer.WriteEndArray();
    }
}

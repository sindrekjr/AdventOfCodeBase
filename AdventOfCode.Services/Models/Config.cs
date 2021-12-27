using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventOfCode.Services.Models;

struct Config
{
    public string Cookie { get; set; }

    public int Year { get; set; }
    
    [JsonConverter(typeof(DaysConverter))]
    public int[] Days { get; set; }

    public void setDefaults()
    {
        //Make sure we're looking at EST, or it might break for most of the US
        var currentEst = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
        if (Cookie == default(string)) Cookie = "";
        if (Year == default(int)) Year = currentEst.Year;
        if (Days == default(int[])) Days = (currentEst.Month == 12 && currentEst.Day <= 25) ? new int[] { currentEst.Day } : new int[] { 0 };
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
                return new int[] { reader.GetInt16() };

            case JsonTokenType.String:
                tokens = new string[] { reader.GetString() ?? "" };
                break;

            default:
                var obj = JsonSerializer
                    .Deserialize<object[]>(ref reader);

                tokens = obj != null
                    ? obj.Select<object, string>(o => o.ToString() ?? "")
                    : new string[] { };
                break;
        }

        var days = tokens.SelectMany<string, int>(ParseString);
        if (days.Contains(0)) return new[] { 0 };

        return days.Where(v => v < 26 && v > 0).OrderBy(day => day).ToArray();
    }

    private IEnumerable<int> ParseString(string str)
    {
        return str.Split(",").SelectMany<string, int>(str =>
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

            return new int[0];
        });
    }

    public override void Write(Utf8JsonWriter writer, int[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (int val in value) writer.WriteNumberValue(val);
        writer.WriteEndArray();
    }
}

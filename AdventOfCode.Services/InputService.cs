using System;
using System.IO;
using System.Net;

namespace AdventOfCode.Services
{
    public static class InputService
    {
        readonly static HttpClientHandler handler = new HttpClientHandler { UseCookies = true };
        readonly static HttpClient client = new HttpClient(handler);
        readonly static string cookie = ConfigService.GetCookie();

        public static string FetchInput(int day, int year)
        {
            string INPUT_FILEPATH = GetSolutionPath(year, day) + "/input";
            string INPUT_URL = GetAocInputUrl(year, day);
            string input = "";

            if (File.Exists(INPUT_FILEPATH) && new FileInfo(INPUT_FILEPATH).Length > 0)
            {
                input = File.ReadAllText(INPUT_FILEPATH);
            }
            else
            {
                try
                {
                    DateTime CURRENT_EST = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
                    if (CURRENT_EST < new DateTime(year, 12, day)) throw new InvalidOperationException();

                    handler.CookieContainer.Add(new Cookie { Name = "session", Domain = ".adventofcode.com", Value = cookie });

                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri(INPUT_URL),
                        Method = HttpMethod.Get,
                    };

                    var response = client.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();

                    input = response.Content.ReadAsStringAsync().Result;

                    File.WriteAllText(INPUT_FILEPATH, input);
                }
                catch (HttpRequestException e)
                {
                    var statusCode = e.StatusCode;
                    var colour = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (statusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine($"Day {day}: Received 400 when attempting to retrieve puzzle input. Your session cookie is probably not recognized.");

                    }
                    else if (statusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Day {day}: Received 404 when attempting to retrieve puzzle input. The puzzle is probably not available yet.");
                    }
                    else
                    {
                        Console.ForegroundColor = colour;
                        Console.WriteLine(e.ToString());
                    }
                    Console.ForegroundColor = colour;
                }
                catch (InvalidOperationException)
                {
                    var colour = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Day {day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
                    Console.ForegroundColor = colour;
                }
            }
            return input;
        }

        public static string FetchDebugInput(int day, int year)
        {
            string INPUT_FILEPATH = GetSolutionPath(year, day) + "/debug";
            return (File.Exists(INPUT_FILEPATH) && new FileInfo(INPUT_FILEPATH).Length > 0)
                ? File.ReadAllText(INPUT_FILEPATH)
                : "";
        }

        static string GetSolutionPath(int year, int day) =>
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../AdventOfCode.Solutions/Year{year}/Day{day:D2}"));

        static string GetAocInputUrl(int year, int day) => $"https://adventofcode.com/{year}/day/{day}/input";
    }
}
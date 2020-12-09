using System;
using System.IO;
using System.Net;
using AdventOfCode.Infrastructure.Models;

namespace AdventOfCode.Infrastructure.Helpers
{
    static class InputHelper
    {
        readonly static string cookie = Config.Get("config.json").Cookie;

        public static string LoadInput(int day, int year)
        {
            string INPUT_FILEPATH = GetDayPath(day, year) + "/input";
            string INPUT_URL = GetAocInputUrl(day, year);
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

                    using (var client = new WebClient())
                    {
                        client.Headers.Add(HttpRequestHeader.Cookie, cookie);
                        input = client.DownloadString(INPUT_URL).Trim();
                        File.WriteAllText(INPUT_FILEPATH, input);
                    }
                }
                catch (WebException e)
                {
                    var statusCode = ((HttpWebResponse)e.Response).StatusCode;
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

        public static string LoadDebugInput(int day, int year)
        {
            string INPUT_FILEPATH = GetDayPath(day, year) + "/debug";
            return (File.Exists(INPUT_FILEPATH) && new FileInfo(INPUT_FILEPATH).Length > 0)
                ? File.ReadAllText(INPUT_FILEPATH)
                : "";
        }

        static string GetDayPath(int day, int year)
            => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Solutions/Year{year}/Day{day.ToString("D2")}"));

        static string GetAocInputUrl(int day, int year)
            => $"https://adventofcode.com/{year}/day/{day}/input";
    }
}
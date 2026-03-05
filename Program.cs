using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class GameStats
{
    public string GameName { get; set; }
    public decimal Kills { get; set; }
    public decimal Deaths { get; set; }
    public decimal AvgKills { get; set; }
}

class Program
{
    static string filePath = "stats.json";
    static List<GameStats> statsList = new List<GameStats>();

    static string[] games =
    {
        "CS2",
        "Valorant",
        "Fortnite",
        "Brawl Stars",
        "Dota 2",
    };

    static void Main()
    {
        LoadStats();

        while (true)
        {
            Console.WriteLine("\n---Меню---");
            Console.WriteLine("1. Добавить статистику");
            Console.WriteLine("2. Показать статистику");
            Console.WriteLine("3. Выход");

            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStats();
                    SaveStats();
                    break;

                case "2":
                    ShowStats();
                    break;

                case "3":
                    return;
            }
        }
    }

    static void AddStats()
    {
        Console.WriteLine("\nВыберите игру:");

        for (int i = 0; i < games.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {games[i]}");
        }

        Console.WriteLine("Номер игры: ");
        int gameIndex = int.Parse(Console.ReadLine()) - 1;

        Console.WriteLine("Киллы: ");
        decimal kills = int.Parse(Console.ReadLine());

        Console.WriteLine("Смерти: ");
        decimal deaths = int.Parse(Console.ReadLine());

        decimal roundavgKills = kills/deaths;
        decimal avgKills = Math.Round(roundavgKills, 2);

        GameStats stats = new GameStats
        {
            GameName = games[gameIndex],
            Kills = kills,
            Deaths = deaths,
            AvgKills = avgKills
        };

        statsList.Add(stats);

        Console.WriteLine("Статистика добавлена!");
    }

    static void ShowStats()
    {
        Console.WriteLine("\n---Статистика---");

        if (statsList.Count == 0)
        {
            Console.WriteLine("Нет сохранённых данных.");
            return;
        }

        foreach (var stat in statsList)
        {
            Console.WriteLine($"Игра: {stat.GameName} | Киллы: {stat.Kills} | Смерти: {stat.Deaths} | Средние киллы: {stat.AvgKills}");
        }
    }

    static void SaveStats()
    {
        string json = JsonSerializer.Serialize(statsList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    static void LoadStats()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            statsList = JsonSerializer.Deserialize<List<GameStats>>(json) ?? new List<GameStats>();
        }
    }
}
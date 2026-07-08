using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasyPrice.NET.Utils;

namespace EasyPrice.NET.Models;

public class Settings
{
    public float DolarPrice { get; set; }
    public string Password { get; set; }
    public int Server { get; set; }         //0-works individually, 1-works as server, 2-works as client
    public IPAddress? ServerIp { get; set; }
    public string? ServerPassword { get; set; }
    public DateTime LastUpdate { get; set; }

    public Settings(){}

    public static Settings Get()
    {
        Settings settings;
        
        if (File.Exists("settings.json"))
        {
            try
            {
                var json = File.ReadAllText("settings.json");
                settings = JsonSerializer.Deserialize<Settings>(json) ?? throw new InvalidOperationException();
            }
            catch (Exception e)
            {
                settings = ResetSettings();
            }
        }
        else
        {
            settings = ResetSettings();
        }

        return settings;
    }

    private static Settings ResetSettings()
    {
        var settings = new Settings();
        settings.DolarPrice = 1;
        settings.Password = Crypto.Hash("1234");
        settings.Server = 0;
        settings.ServerIp = null;
        settings.ServerPassword = null;
        settings.LastUpdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        SaveToFile(settings);
        return settings;
    }

    public void UpdateDolarPrice(float dolarPrice)
    {
        DolarPrice = dolarPrice;
        SaveToFile(this);
    }
    
    public void UpdatePassword(string password)
    {
        Password = Crypto.Hash(password);
        SaveToFile(this);
    }

    private static void SaveToFile(Settings settings)
    {
        var options = new JsonSerializerOptions{WriteIndented=true, };
        var json = JsonSerializer.Serialize(settings, options);
        File.WriteAllText("settings.json", json);
    }
}
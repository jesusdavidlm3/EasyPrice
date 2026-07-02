using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasyPrice.NET.Utils;

namespace EasyPrice.NET.Models;

public class Settings
{
    public float DolarPrice { get; set; }
    public string Password { get; set; }

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
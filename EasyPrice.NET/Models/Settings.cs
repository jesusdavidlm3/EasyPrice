using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyPrice.NET.Models;

public class Settings
{
    public float DolarPrice { get; set; }

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
        var options = new JsonSerializerOptions{WriteIndented=true, };
        var json = JsonSerializer.Serialize(settings, options);
        File.WriteAllText("settings.json", json);
        return settings;
    }

    public void UpdateDolarPrice(float dolarPrice)
    {
        DolarPrice = dolarPrice;
        var options = new JsonSerializerOptions{WriteIndented=true, };
        var json = JsonSerializer.Serialize(this, options);
        File.WriteAllText("settings.json", json);
    }
}
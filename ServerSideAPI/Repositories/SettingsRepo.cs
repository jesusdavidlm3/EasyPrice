using System.Text.Json;
using Libs;
using Models;
using ServerSideAPI.Interfaces;
using ServerSideAPI.Services;

namespace ServerSideAPI.Repositories;

public class SettingsRepo : ISettingsRepo
{
    private string SettingsFile { get; set; }

    public SettingsRepo(bool? desktop = false)
    {
        if (desktop == true)
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appDataFolder, "EasyPrice", "api");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            SettingsFile = Path.Combine(folder, "settings.json");
        }
        else
        {
            SettingsFile = Path.Combine("/", "EasyPrice", "settings.json");
        }
    }
    
    public Settings GetSettings()
    {
        Settings settings;
        try
        {
            var json = File.ReadAllText(SettingsFile);
            settings = JsonSerializer.Deserialize<Settings>(json) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            settings = SaveSettings();
        }

        return settings;
    }
    
    public Settings SaveSettings(Settings? newSettings = null)
    {
        var settings = new Settings();
        settings.DolarPrice = newSettings?.DolarPrice ?? 1;
        settings.AdminPassword = newSettings?.AdminPassword ?? Crypto.Hash("1234");
        settings.ConnPassword = newSettings?.ConnPassword ?? Crypto.Hash("1234");
        settings.Secret = newSettings?.Secret ?? "123";
        settings.Token = newSettings?.Token ?? null;
        settings.WorkMode = newSettings?.WorkMode ?? 1;
        settings.ServerIp = newSettings?.ServerIp ?? null;
        settings.LastUpdate = newSettings?.LastUpdate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        
        SaveToFile(settings);
        return settings;
    }
    
    private void SaveToFile(Settings settings)
    {
        var options = new JsonSerializerOptions{WriteIndented=true, };
        var json = JsonSerializer.Serialize(settings, options);
        File.WriteAllText(SettingsFile, json);
    }
}
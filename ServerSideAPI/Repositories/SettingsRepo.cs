using System.Text.Json;
using Libs;
using Models;
using ServerSideAPI.Interfaces;
using ServerSideAPI.Services;

namespace ServerSideAPI.Repositories;

public class SettingsRepo : ISettingsRepo
{
    private Settings _current { get; set; }

    public Settings Current
    {
        get => _current;
    }
    private string SettingsFile { get; set; }

    public SettingsRepo(bool? desktop = true)
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

        _current = GetSettings();
    }
    
    private Settings GetSettings()
    {
        Settings settings;
        try
        {
            var json = File.ReadAllText(SettingsFile);
            settings = JsonSerializer.Deserialize<Settings>(json) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            settings = ResetSettings();
        }

        return settings;
    }
    
    private Settings ResetSettings(Settings? newSettings = null)
    {
        var settings = new Settings();
        settings.DolarPrice = newSettings?.DolarPrice ?? 1;
        settings.AdminPassword = newSettings?.AdminPassword ?? Crypto.Hash("1234");
        settings.ConnPassword = newSettings?.ConnPassword ?? Crypto.Hash("123456");
        settings.WorkMode = newSettings?.WorkMode ?? 1;
        settings.ServerIp = newSettings?.ServerIp ?? null;
        settings.LastUpdate = newSettings?.LastUpdate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        
        SaveToFile(settings);
        return settings;
    }

    public void UpdateLastUpdate(DateTime newLastUpdate)
    {
        var newSettings = Current;
        newSettings.LastUpdate = newLastUpdate;
        SaveToFile(newSettings);
        _current = newSettings;
    }

    public void UpdateDolarPrice(double newDolarPrice)
    {
        var newSettings = Current;
        newSettings.DolarPrice = newDolarPrice;
        SaveToFile(newSettings);
        _current = newSettings;
    }

    public bool UpdateConnPassword(string newConnPassword)
    {
        var encryptedPassword = Crypto.Hash(newConnPassword);
        if (encryptedPassword.Equals(Current.AdminPassword))
        {
            return false;
        }
        else
        {
            var newSettings =  Current;
            newSettings.ConnPassword = encryptedPassword;
            SaveToFile(newSettings);
            _current = newSettings;
            return true;
        }
    }

    public bool UpdateAdminPassword(string newAdminPassword)
    {
        var encryptedPassword = Crypto.Hash(newAdminPassword);
        if (encryptedPassword.Equals(Current.ConnPassword))
        {
            return false;
        }
        else
        {
            var newSettings = Current;
            newSettings.AdminPassword = encryptedPassword;
            SaveToFile(newSettings);
            _current = newSettings;
            return true;
        }
    }
    
    private void SaveToFile(Settings settings)
    {
        var options = new JsonSerializerOptions{WriteIndented=true, };
        var json = JsonSerializer.Serialize(settings, options);
        File.WriteAllText(SettingsFile, json);
    }
}
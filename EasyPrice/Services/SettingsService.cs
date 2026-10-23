using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.Json;
using Avalonia.Controls.ApplicationLifetimes;
using Models;
using EasyPrice.Utils;

namespace EasyPrice.Services;

public class SettingsService
{
    private static Settings _settings { get; set; }   
    public static Settings Settings
    {
        get
        {
            if (_settings == null)
            {
                GetSettings();
            }
            return _settings;
        }
    }
    
    private static string SettingsFile
    {
        get
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            {
                file = Path.Combine(file, "EasyPrice");
            }
            file = Path.Combine(file, "Settings.json");
            return file;
        }
    }

    private static void GetSettings()
    {
        try
        {
            var json = File.ReadAllText(SettingsFile);
            _settings = JsonSerializer.Deserialize<Settings>(json) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            InitSettings();
        }
    }

    private static void InitSettings()
    {
        var settings = new Settings();
        settings.DolarPrice = 1;
        settings.AdminPassword = Crypto.Hash("1234");
        settings.ServerIp = null;
        settings.LastUpdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTimeKind.Utc);

        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
        {
            settings.WorkMode = 0;
        }
        else
        {
            settings.WorkMode = 2;
        }

        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EasyPrice"));
        }
        
        SaveToFile(settings);
        _settings = settings;
    }
    
    private static void SaveToFile(Settings settings)
    {
        var options = new JsonSerializerOptions{WriteIndented=true, };
        var json = JsonSerializer.Serialize(settings, options);
        File.WriteAllText(SettingsFile, json);
    }

    public static void UpdateDolarPrice(double  dolarPrice)
    {
        if (_settings.DolarPrice.Equals(dolarPrice))
            return;
        _settings.DolarPrice = dolarPrice;
        SaveToFile(_settings);
    }

    public static void UpdateAdminPassword(string adminPassword)
    {
        _settings.AdminPassword = Crypto.Hash(adminPassword);
        SaveToFile(_settings);
    }

    public static void UpdateConnPassword(string connPassword)
    {
        _settings.ConnPassword = Crypto.Hash(connPassword);
        SaveToFile(_settings);
    }
    
    public static void UpdateWorkMode(int newWorkMode)
    {
        _settings.WorkMode = newWorkMode;
        SaveToFile(_settings);
    }

    public static void UpdateServerIp(string newServerIp)
    {
        _settings.ServerIp = newServerIp;
        SaveToFile(_settings);
    }

    public static void UpdateLastUpdate(DateTime newLastUpdate)
    {
        var kind = newLastUpdate.Kind;
        if (kind.Equals(DateTimeKind.Local))
        {
            newLastUpdate.Deconstruct(out DateOnly dateOnly, out TimeOnly timeOnly);
            _settings.LastUpdate = new DateTime(dateOnly, timeOnly, DateTimeKind.Utc);
        }
        else
        {
            _settings.LastUpdate = newLastUpdate;
        }
        SaveToFile(_settings);
    }
}
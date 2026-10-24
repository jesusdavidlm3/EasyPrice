using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Data.Sqlite;

namespace EasyPrice.Services;

public class DBService
{
    private static SqliteConnection connection { get; set; }

    static DBService()
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = appDataFolder;
        if(App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            filePath = Path.Combine(appDataFolder, "EasyPrice");
        if(!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);
        filePath = Path.Combine(filePath, "db.db");
        connection = new SqliteConnection($"Data Source={filePath};Mode=ReadWriteCreate");
        connection.Open();
        InitDb();
    }

    private static void InitDb()
    {
        var products = connection.CreateCommand();
        products.CommandText = "CREATE TABLE IF NOT EXISTS products(Id TEXT PRIMARY KEY, Name string, PackPrice REAL,PackQuantity INTEGER, Gain INTEGER, LastUpdate DATETIME)";
        products.ExecuteNonQuery();
    }
    
    public static SqliteDataReader Query(string query, List<(string, object)>? parameters = null)
    {
        var command = connection.CreateCommand();
        command.CommandText = query;
        if(parameters != null)
        {
            foreach (var param in parameters)
            {
                var parameter = new SqliteParameter(param.Item1, param.Item2);
                command.Parameters.Add(parameter);
            }
        }
        return command.ExecuteReader();
    }
    
    public static void Execute(string query, List<(string, object)>? parameters = null)
    {
        using var command = connection.CreateCommand();
        command.CommandText = query;
        if(parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Item1, param.Item2);
            }
        }

        command.ExecuteNonQuery();
    }
}
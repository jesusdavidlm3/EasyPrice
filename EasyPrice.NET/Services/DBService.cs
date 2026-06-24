using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace EasyPrice.NET.Services;

public class DBService
{
    private static SqliteConnection connection = new SqliteConnection("Data Source=db.db;Mode=ReadWriteCreate");

    static DBService()
    {
        connection.Open();
        InitDb();
    }

    private static void InitDb()
    {
        var command =  connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS products(Id TEXT, Name string, PackPrice REAL,PackQuantity INTEGER, Gain INTEGER)";
        command.ExecuteNonQuery();
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
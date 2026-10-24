using Microsoft.Data.Sqlite;
using ServerSideAPI.Interfaces;

namespace ServerSideAPI.Services;

public class DbService : IDbService
{
    private string FilePath { get; set; }
    private SqliteConnection _sqliteConnection;

    public DbService(bool? desktop = false)
    {
        if (desktop == true)
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appDataFolder, "EasyPrice", "api");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            FilePath = Path.Combine(folder, "db.db");
        }
        else
        {
            FilePath = Path.Combine("/", "EasyPrice", "db.db");
        }

        _sqliteConnection = new SqliteConnection($"DataSource={FilePath};Mode=ReadWriteCreate;");
        _sqliteConnection.Open();
        var products = _sqliteConnection.CreateCommand();
        products.CommandText =
            "CREATE TABLE IF NOT EXISTS products(Id TEXT PRIMARY KEY, Name string, PackPrice REAL,PackQuantity INTEGER, Gain INTEGER, LastUpdate DATETIME, Deleted BOOL DEFAULT 0 NOT NULL )";
        products.ExecuteNonQuery();
    }

    public SqliteDataReader Query(string query, List<(string, object)>? parameters = null)
    {
        var command = _sqliteConnection.CreateCommand();
        command.CommandText = query;
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                var parameter = new SqliteParameter(param.Item1, param.Item2);
                command.Parameters.Add(parameter);
            }
        }

        return command.ExecuteReader();
    }

    public void Execute(string query, List<(string, object)>? parameters = null)
    {
        using var command = _sqliteConnection.CreateCommand();
        command.CommandText = query;
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Item1, param.Item2);
            }
        }

        command.ExecuteNonQuery();
    }
}
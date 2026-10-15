using Microsoft.Data.Sqlite;

namespace ServerSideAPI.Interfaces;

public interface IDbService
{
    public SqliteDataReader Query(string query, List<(string, object)> parameters);
    public void Execute(string query,  List<(string, object)> parameters);
    
}
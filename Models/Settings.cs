using System.Net;

namespace Models;

public class Settings
{
    public double DolarPrice { get; set; }
    public string? AdminPassword { get; set; } //Used to manage products and settings, etc
    public string? ConnPassword { get; set; } //Used by clients to connect to the server
    public int WorkMode { get; set; } //0-works individually, 1-works as server, 2-works as client
    public IPAddress? ServerIp { get; set; }
    public DateTime LastUpdate { get; set; }
}
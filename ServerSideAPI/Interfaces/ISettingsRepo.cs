using System.Text.Json;
using Models;
using ServerSideAPI.Repositories;

namespace ServerSideAPI.Interfaces;

public interface ISettingsRepo
{
    public Settings Current { get; }
    public void UpdateLastUpdate(DateTime newLastUpdate);
    public void UpdateDolarPrice(double newDolarPrice);
    public bool UpdateConnPassword(string newConnPassword);
    public bool UpdateAdminPassword(string newAdminPassword);
}
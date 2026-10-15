using System.Text.Json;
using Models;

namespace ServerSideAPI.Interfaces;

public interface ISettingsRepo
{
    public Settings GetSettings();
    public Settings SaveSettings(Settings settings);
}
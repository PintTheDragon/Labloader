namespace Labloader.API.Enums
{
    public enum PluginPriority
    {
        Highest = int.MaxValue-1,
        High = 1000,
        Normal = 0,
        Low = -1000,
        Lowest = int.MinValue+1
    }
}
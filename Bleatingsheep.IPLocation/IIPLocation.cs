namespace Bleatingsheep.IPLocation
{
    public interface IIPLocation
    {
        string Provider { get; }
        string Region { get; }
    }
}
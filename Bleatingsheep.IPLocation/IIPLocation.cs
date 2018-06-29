namespace Bleatingsheep.IPLocation
{
    public interface IIPLocation
    {
        string Provider { get; }
        string Location { get; }

        string ToString();
    }
}
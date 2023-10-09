namespace Domain;

public class EventNotFoundException : Exception
{
    public EventNotFoundException(string message) : base(message)
    {
    }
}

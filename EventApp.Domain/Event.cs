namespace Domain;

/// <summary>
/// Event
/// </summary>
public class Event
{
    /// <summary>
    /// Event Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Date of the event
    /// </summary>
    public DateTime DateOfEvent { get; set; }
    
    /// <summary>
    /// Limit of users that can register to particular event
    /// </summary>
    public int UserLimit { get; set; }
}

/// <summary>
/// Registered user of the event
/// </summary>
public class EventUsers
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Id of related event
    /// </summary>
    public Guid EventId { get; set; }
    
    /// <summary>
    /// User's first name
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// User's last name
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// User's identity code
    /// </summary>
    public string IdentityCode { get; set; }
}
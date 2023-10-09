namespace EventApp.DTO;

/// <summary>
/// Base event dto
/// </summary>
public class EventDto
{
    /// <summary>
    /// Event Id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Event title
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Limit of users that can register to event
    /// </summary>
    public int UsersLimit { get; set; }

    /// <summary>
    /// Date of the event
    /// </summary>
    public DateTime DateOfEvent { get; set; }
}

/// <summary>
/// Dto for registering user to the given event
/// </summary>
public class RegisterToEventDto
{
    /// <summary>
    /// User's identity code
    /// </summary>
    public string IdentityCode { get; set; }
    
    /// <summary>
    /// User's first name 
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// User's last name 
    /// </summary>
    public string LastName { get; set; }
}

/// <summary>
/// <inheritdoc cref="RegisterToEventDto"/>
/// </summary>
public class UserEventDto : RegisterToEventDto
{
   
}
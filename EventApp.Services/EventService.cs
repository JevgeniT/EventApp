using Domain;
using EventApp.DTO;
using Repository;

namespace Services;

public interface IEventService
{
    /// <summary>
    /// Creates Event
    /// </summary>
    /// <param name="eventDto"><inheridoc name="eventDto"/></param>
    Task CreateEvent(EventDto eventDto);

    /// <summary>
    /// Returns all events
    /// </summary>
    /// <returns>List of events</returns>
    Task<IEnumerable<EventDto>> GetAllEventAsync();
    
    /// <summary>
    /// Returns all users that is part of the event
    /// </summary>
    /// <param name="id">Event id</param>
    /// <returns>List of users</returns>
    Task<IEnumerable<UserEventDto>> GetUserEventsAsync(Guid id);

    /// <summary>
    /// Adds user to the given event
    /// </summary>
    /// <param name="eventId"> Id of event</param>
    /// <param name="dto">Dto with registration data</param>
    Task AddUserToEventAsync(Guid eventId, RegisterToEventDto dto);
}

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task CreateEvent(EventDto eventDto)
    {
        if (eventDto.Title.Trim().IsNullOrEmpty())
            throw new ArgumentNullException(nameof(eventDto.Title));

        if (eventDto.DateOfEvent <= DateTime.Now)
            throw new ArgumentException("Cannot be in the past");
        
        if (await EventExistAsync(eventDto.Title))
            throw new ArgumentException("Event exists");
        
        await _eventRepository.CreateEventAsync(eventDto);
    }
    
    public async Task<IEnumerable<EventDto>> GetAllEventAsync() =>
        (await _eventRepository.GetAllEventsAsync()).Select(x => new EventDto
        {
            Id = x.Id,
            Title = x.Title,
            UsersLimit = x.UserLimit,
            DateOfEvent = x.DateOfEvent
        });
    
    public async Task<IEnumerable<UserEventDto>> GetUserEventsAsync(Guid id) =>
        (await _eventRepository.GetAllEventsUsers(id)).Select(x => new UserEventDto
        {
            IdentityCode = x.IdentityCode,
            FirstName = x.FirstName,
            LastName = x.LastName
        });
    
    public async Task AddUserToEventAsync(Guid eventId, RegisterToEventDto dto)
    {
        var @event = await _eventRepository.GetEventByIdAsync(eventId)
                     ?? throw new EventNotFoundException($"{nameof(eventId)}: {eventId}");
        var usersCount = await _eventRepository.GetCountOfUsersInEventAsync(eventId);

        if (usersCount >= @event.UserLimit) 
            throw new ArgumentException("Limit of users exceeded");
            
        if (await _eventRepository.IsUserRegisteredToEvent(eventId, dto.IdentityCode))
            throw new ArgumentException($"User {dto.IdentityCode} already in event");
        
        await _eventRepository.AddUserToEventAsync(eventId, dto);
    }

    private async Task<bool> EventExistAsync(string title)
        => await _eventRepository.EventExistsAsync(title);
}
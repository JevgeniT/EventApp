using Domain;
using EventApp.DTO;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _dbContext;

    public EventRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateEventAsync(EventDto eventDto)
    {
        await _dbContext.Events.AddAsync(new Event
        {
                DateOfEvent = eventDto.DateOfEvent,
                Id = Guid.NewGuid(),
                Title = eventDto.Title,
                UserLimit = eventDto.UsersLimit
        });
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddUserToEventAsync(Guid eventId, RegisterToEventDto dto)
    {
        await _dbContext.EventUsers.AddAsync(new EventUsers
        {
            EventId = eventId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IdentityCode = dto.IdentityCode
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync() 
        => await _dbContext.Events.ToListAsync();

    public async Task<IEnumerable<EventUsers>> GetAllEventsUsers(Guid id) 
        => await _dbContext.EventUsers.Where(x => x.EventId == id).ToListAsync();

    public async Task<Event> GetEventByIdAsync(Guid eventId) 
        => await _dbContext.Events.FindAsync(eventId);

    public async Task<bool> EventExistsAsync(string title)
    {
        return await _dbContext.Events.AnyAsync(x => x.Title == title);
    }

    public async Task<bool> IsUserRegisteredToEvent(Guid eventId, string identityCode)
        => await _dbContext.EventUsers.AnyAsync(
            x => x.EventId == eventId && x.IdentityCode == identityCode);

    public async Task<int> GetCountOfUsersInEventAsync(Guid id)
       => await _dbContext.EventUsers.CountAsync(x => x.EventId == id);
}

public interface IEventRepository
{
    Task CreateEventAsync(EventDto eventDto);

    Task AddUserToEventAsync(Guid eventId, RegisterToEventDto dto);

    Task<IEnumerable<Event>> GetAllEventsAsync();

    Task<Event> GetEventByIdAsync(Guid eventId);
    
    Task<bool> EventExistsAsync(string title);

    Task<IEnumerable<EventUsers>> GetAllEventsUsers(Guid id);

    Task<bool> IsUserRegisteredToEvent(Guid eventId, string identityCode);

    Task<int> GetCountOfUsersInEventAsync(Guid id);
}
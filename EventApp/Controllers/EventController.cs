using EventApp.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace EventApp.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    
    public EventController(IEventService eventService) 
        => _eventService = eventService;

    /// <summary>
    /// Returns all events
    /// </summary>
    /// <returns>List of events</returns>
    [HttpGet]
    public async Task<IEnumerable<EventDto>> Get() 
        => await _eventService.GetAllEventAsync();

    /// <summary>
    /// Returns all users that are part of given event
    /// </summary>
    /// <param name="id">Event id</param>
    /// <returns>List of users</returns>
    [HttpGet("{id:guid}")]
    public async Task<IEnumerable<UserEventDto>> GetUserEvents(Guid id) 
        => await _eventService.GetUserEventsAsync(id);

    /// <summary>
    /// Creates event
    /// </summary>
    /// <param name="eventDto">Dto with event data</param>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody]EventDto eventDto)
    {
        await _eventService.CreateEvent(eventDto);
        return Ok();
    }

    /// <summary>
    /// Adds user to event
    /// </summary>
    /// <param name="id">Event id</param>
    /// <param name="dto"><inheritdoc cref="RegisterToEventDto"/></param>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody]RegisterToEventDto dto)
    {
        await _eventService.AddUserToEventAsync(id, dto);
        return NoContent();
    }
}

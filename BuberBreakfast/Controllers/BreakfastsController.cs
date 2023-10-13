using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }
    
    [HttpPost]
    public IActionResult CreateBreakfast([FromBody] CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);
        
        // TODO: save breakfast to database
        _breakfastService.CreateBreakfast(breakfast);

        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        if (createBreakfastResult.IsError)
        {
            return Problem(createBreakfastResult.Errors);
        }
        
        return CreatedAtAction(actionName: nameof(GetBreakfast), routeValues: new { id = breakfast.Id}, value: MapBreakfastResponse(breakfast));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);
        return getBreakfastResult.Match(
            onValue: breakfast => Ok(MapBreakfastResponse(breakfast)),
            onError: Problem
        );
    }
    
    [HttpPut]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
        _breakfastService.UpsertBreakfast(breakfast);
        
        // TODO:: return 201 if a new breakfast was created
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedResult = _breakfastService.DeleteBreakfast(id);


        return deletedResult.Match(
            onValue: _ => NoContent(),
            onError: Problem);
    }
    
    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet);
    }
    
    
}
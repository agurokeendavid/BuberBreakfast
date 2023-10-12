namespace BuberBreakfast.Contracts.Breakfast;

public record BreakfastResponse(Guid id, string name, string description, DateTime startDateTime, DateTime endDateTime, DateTime lastModifiedDateTime, List<string> savory, List<string> sweet);
namespace BuberBreakfast.Contracts.Breakfast;

public record UpsertBreakfastRequest(string name, string description, DateTime startDateTime, DateTime endDateTime, List<string> savory, List<string> sweet);
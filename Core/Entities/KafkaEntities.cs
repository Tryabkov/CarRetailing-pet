namespace Core.Entities;

public class CarEvent
{
    public string? EventType { get; init; }
    public uint? UserId { get; init; } = null;
    public uint? CarId { get; init; } = null;
    public string? Mark { get; init; } = null;
    public string? Model { get; init; } = null;
    public decimal? Price { get; init; } = null;

    public CarEvent(string eventType, CarEntity car)
    {
        EventType = eventType;
        CarId = car.Id;
        UserId = car.UserId;
        Mark = car.Mark;
        Model = car.Model;
        Price = car.Price;
    }
}

public class SearchEvent(string eventType, CarFilters filters)
{
    public string EventType { get; init; } = eventType;
    public CarFilters Filters { get; init; } = filters;
}
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Application;

public class KafkaNotifyService
{
    private readonly IEventBus<CarEvent> _carEventBus;
    private readonly IEventBus<SearchEvent> _searchEventBus;
    private readonly ICarService _carService;
    private readonly CancellationToken ct;

    public KafkaNotifyService(
        IEventBus<CarEvent> carEventBus, 
        IEventBus<SearchEvent> searchEventBus, 
        ICarService carService, IHostApplicationLifetime lifetime)
    {
        _carEventBus = carEventBus;
        _searchEventBus = searchEventBus;
        _carService = carService;
        ct = lifetime.ApplicationStopping;
        
        _carService.OnCarCreated += (sender, entity) 
            => _carEventBus.PublishAsync(new CarEvent("created", entity), ct);
        
        _carService.OnCarUpdated += (sender, entity) 
            => _carEventBus.PublishAsync(new CarEvent("updated", entity), ct);
        
        _carService.OnFilterSearch += (sender, filters) 
            => _searchEventBus.PublishAsync(new SearchEvent("search", filters), ct);
    }
    
    
}
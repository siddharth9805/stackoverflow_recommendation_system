namespace stackoverflow_recommendation_system.Services;

using Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

public class DatabasePollingService : BackgroundService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IConfiguration configuration;
    private int _lastCount = 0;

    public DatabasePollingService(IHubContext<NotificationHub> hubContext,IConfiguration config)
    {
        this.configuration = config;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var newCount = GetTableCount();
            if (newCount > _lastCount)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Admin", "New User Joined");
                _lastCount = newCount; // Update last count
            }

            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken); // Wait for 5 minutes
        }
    }

    private int GetTableCount()
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        using (var connection = new SqlConnection(connectionString))
        {
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Users");
            return count;
        }
    }
}

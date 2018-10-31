using CAStest.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CAStest.Services
{
    public class DataRefreshService : HostedService
    {
        private readonly TrackingTermTerminationContracts _trackingTermTerminationContracts;
        private readonly PasswordTrackingService _passwordTrackingService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DataRefreshService(TrackingTermTerminationContracts trackingTermTerminationContracts, 
            PasswordTrackingService passwordTrackingService,
            IServiceScopeFactory serviceScopeFactory)
        {
            _trackingTermTerminationContracts = trackingTermTerminationContracts;
            _passwordTrackingService = passwordTrackingService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    foreach (var user in context.Users.ToList())
                    {
                        _trackingTermTerminationContracts.TermContractChecking(user.ContractNotifications);
                        _passwordTrackingService.PasswordNotification(user.PasswordNotifications);
                        await Task.Delay(TimeSpan.FromSeconds(120), cancellationToken);
                    }
                }
            }
        }
    }
}

using CAStest.Data;
using CAStest.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Services
{
    public class NotificationsForUsersProfile
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public NotificationsForUsersProfile(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void NatificationSend(Message message, string userId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                Notification notification = new Notification()
                {
                    Message = message.Text,
                    DateOfCreate = DateTime.Now,
                    UserId = userId
                };
                context.Notifications.Add(notification);
                context.SaveChanges();
            }
        }
    }
}

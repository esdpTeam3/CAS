using CAStest.Data;
using CAStest.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Services
{
    public class PasswordTrackingService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public PasswordTrackingService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void PasswordNotification(int timeSend)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                EmailSender sender = new EmailSender();
                NotificationsForUsersProfile notificationsForUsersProfile = new NotificationsForUsersProfile(serviceScopeFactory);
                foreach (var user in context.Users.ToList())
                {
                    if (DateTime.Now.AddDays(timeSend) >= user.DatePassword && user.Email != null)
                    {
                        Message message = new Message()
                        {
                            RecipientEmail = user.Email,
                            Subject = "ФинансСофт.Уведомление системы",
                            Text = $"Уважаемый пользователь {user.UserName} подошел срок действия вашего пароля." +
                            $" Обратитесь к администратору."
                        };
                        sender.SendEmail(message);
                        notificationsForUsersProfile.NatificationSend(message, user.Id); 
                    }
                }
            }
        }
    }
}

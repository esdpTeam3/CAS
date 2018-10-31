using CAStest.Data;
using CAStest.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CAStest.Services
{
    public class TrackingTermTerminationContracts 
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public TrackingTermTerminationContracts(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void TermContractChecking(int timeSend)
        {
            //Console.WriteLine("*****************************ПРошло успешно***********************************************************");
            using (var scope = serviceScopeFactory.CreateScope())
            {

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                foreach (var contract in context.Contracts.ToList())
                {
                    if (contract.Autorolongation == true)
                    {
                        contract.ContractTime.AddYears(1);

                        EmailSender sender = new EmailSender();
                        NotificationsForUsersProfile notificationsForUsersProfile = new NotificationsForUsersProfile(serviceScopeFactory);
                        foreach (var user in context.Users.Include(u => u.Favorites).ToList())
                        {
                            foreach (var fav in user.Favorites)
                            {
                                if (user.Email != null && fav.Contract == contract)
                                {
                                    Message message = new Message()
                                    {
                                        RecipientEmail = user.Email,
                                        Subject = "ФинансСофт. Уведомление системы",
                                        Text = $"Договор под номером {contract.ContractNumber} был атоматически продлён."
                                    };
                                    sender.SendEmail(message);
                                    notificationsForUsersProfile.NatificationSend(message, user.Id);
                                }
                            }
                        }
                    }
                    else if (DateTime.Now.AddDays(timeSend) >= contract.ContractTime)
                    {
                        EmailSender sender = new EmailSender();
                        NotificationsForUsersProfile notificationsForUsersProfile = new NotificationsForUsersProfile(serviceScopeFactory);
                        foreach (var user in context.Users.Include(u => u.Favorites).ToList())
                        {
                            foreach(var fav in user.Favorites)
                            {
                                if (user.Email != null && fav.Contract == contract)
                                {
                                    Message message = new Message()
                                    {
                                        RecipientEmail = user.Email,
                                        Subject = "ФинансСофт. Уведомление системы",
                                        Text = $"Дата окончания договора под номером {contract.ContractNumber} подходит к окончанию."
                                    };
                                    sender.SendEmail(message);
                                    notificationsForUsersProfile.NatificationSend(message, user.Id);

                                }
                            } 
                        }
                    }
                }
            ;
            }
        }

    }
}

using BotApp.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class AppContext : DbContext
    {
        public AppContext() : base()
        {

        } 
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public DbSet<BotFieldInput> BotFieldInputs { get; set; }
        public DbSet<BotFieldInputType> BotFieldInputTypes { get; set; }
        public DbSet<BotMenu> BotMenus { get; set; }
        public DbSet<BotSession> BotSessions { get; set; }
        public DbSet<BotFieldValidationType> BotFieldValidationTypes { get; set; }
        public DbSet<BotFieldInputValidation> BotFieldInputValidations { get; set; }
        public DbSet<BotMenuFieldInputRequestHeader> BotMenuFieldInputRequestHeaders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<WhatsAppPhone> WhatsAppPhones { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=156.38.224.15;database=imonitor_whatsapp;user=imonitor_whatsapp;password=7b!og09C");
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OrderApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesOnContainers.Services.OrderApi.Data
{
    public static class MigrateDatabase
    {
        public static void EnsureCreated(OrdersContext context)
        {
            System.Console.WriteLine("Creating database...");
            context.Database.Migrate();


            System.Console.WriteLine("Database and tables' creation complete.....");
        }
    }
}

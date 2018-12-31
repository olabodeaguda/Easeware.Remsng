using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Easeware.Remsng.Entities.Entities;
using System.Linq;

namespace Easeware.Remsng.Entities
{
    public class SeedData
    {
        public static void Initialized(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<RemsDbContext>();
            context.Database.EnsureCreated();

            var ctry = context.Countries.Include(x => x.States)
                .FirstOrDefault(x => x.CountryCode == "NGN");
            if (ctry == null)
            {
                Country c = new Country()
                {
                    CountryCode = "NGN",
                    CountryName = "Nigeria",
                    States = new List<State>()
                };
                context.Countries.Add(c);
            }

            var stats = context.States.ToList();
            if (!stats.Any(x => x.StateCode == "LAG"))
            {
                State state = new State()
                {
                    StateName = "Lagos State",
                    StateCode = "LAG"
                };

                if (ctry == null)
                {
                    ctry = context.Countries.FirstOrDefault(x => x.CountryCode == "NGN");
                }
                ctry.States.Add(state);
            }

            context.SaveChanges();
        }
    }
}

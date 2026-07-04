using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void SeedworkSpecialities(
            this Dsw2026Ej15DbContext context,
            string file)
        {
            if (context.Specialities.Any())
            {
                return;
            }

            string jsonPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Sources",
                file);

            if (!File.Exists(jsonPath))
            {
                return;
            }

            string json = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<Speciality>? specialities = 
                JsonSerializer.Deserialize<List<Speciality>>(json, options);

            if (specialities == null)
                return;

            context.Specialities.AddRange(specialities);
            context.SaveChanges();
            
        }
    }
}

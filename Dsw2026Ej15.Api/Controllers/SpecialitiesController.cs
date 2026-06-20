using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor para uso normal en el código
        public Speciality(string name, string description, Guid? id = null) : base(id)
        {
            Name = name;
            Description = description;
        }

        // Constructor privado para que JsonSerializer pueda deserializar
        [JsonConstructor]
        private Speciality() : base(null)
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
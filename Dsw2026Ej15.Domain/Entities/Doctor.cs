using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; private set; }
        public string LicenseNumber { get; private set; }
        public bool IsActive { get; private set; }

        public Guid? SpecialityId { get; private set; }

        public Speciality? Speciality { get; private set; }

        private Doctor() { }

        public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)

        {
            Name = name;
            LicenseNumber = licenseNumber;
            Speciality = speciality;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Update(string name, string licenseNumber, Speciality speciality)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            Speciality = speciality;
            SpecialityId = speciality.Id;
        }
    }
}

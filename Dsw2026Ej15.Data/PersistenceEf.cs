using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) 
                return;
            doctor.Deactivate();
            await _context.SaveChangesAsync();

        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<List<Speciality>> GetSpecialitiesAsync()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await _context.Specialities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Update(doctor);
            await _context.SaveChangesAsync();
        }
    }
}

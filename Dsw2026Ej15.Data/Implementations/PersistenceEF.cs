using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Implementations
{
    public class PersistenceEF : IPersistence
    {
        private readonly AppDbContext _context;

        public PersistenceEF(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Speciality>> GetSpecialitiesAsync()
        {
            return await _context.Specialities.ToListAsync();
        }
        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await _context.Specialities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _context.Entry(doctor.Speciality!).State = EntityState.Unchanged;
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}

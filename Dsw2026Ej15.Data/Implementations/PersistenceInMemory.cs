using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Implementations
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Speciality> _specialities = [];
        private readonly List<Doctor> _doctors = [];

        public PersistenceInMemory()
        {
            _specialities = LoadSpecialities();
            _doctors = new List<Doctor>();
        }

        public async Task<List<Speciality>> GetSpecialitiesAsync()
        {
            return await Task.FromResult(_specialities);
        }
        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await Task.FromResult(_specialities.SingleOrDefault(s => s.Id == id));
        }
        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await Task.FromResult(_doctors);
        }
        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await Task.FromResult(_doctors.SingleOrDefault(d => d.Id == id));
        }
        public async Task AddDoctorAsync(Doctor doctor)
        {
            _doctors.Add(doctor);
            await Task.CompletedTask;
        }
        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            var index = _doctors.FindIndex(d => d.Id == doctor.Id);
            if (index != -1)
            {
                _doctors[index] = doctor;
            }
            await Task.CompletedTask;
        }
        public async Task DeleteDoctorAsync(Guid id)
        {
            var doctor = _doctors.SingleOrDefault(d => d.Id == id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
            await Task.CompletedTask;
        }
        private List<Speciality> LoadSpecialities()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Sources", "specialities.json");
            if (!File.Exists(path))
            {
                return new List<Speciality>();
            }
            var json = File.ReadAllText(path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Speciality>>(json, options) ?? new List<Speciality>();
        }
    }
}

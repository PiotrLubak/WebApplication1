using Api.Abstractions;
using Api.Dtos;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class PatientService : IPatientService
    {
        private readonly S22581Context _context;

        public PatientService(S22581Context context)
        {
            _context = context;
        }

        public PatientDto GetPatientWithVisists(int idPatient)
        {
            var patient = _context.Patients
            .Include(p => p.Visits)
            .ThenInclude(v => v.IdDoctorNavigation)
            .Where(p => p.IdPatient == idPatient)
            .Select(p => new PatientDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                TotalAmountMoneySpent = p.Visits.Sum(v => v.Price).ToString("F2") + " z³",
                NumberOfVisists = p.Visits.Count,
                Visists = p.Visits.Select(v => new VisitDto
                {
                    IdVisit = v.IdVisit,
                    Doctor = v.IdDoctorNavigation.FirstName + " " + v.IdDoctorNavigation.LastName,
                    Date = v.Date.ToString("yyyy-MM-dd HH:mm"),
                    Price = v.Price.ToString("F2") + " z³"
                }).ToList()
            })
            .FirstOrDefault();

            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            return patient;
        }
    }
}
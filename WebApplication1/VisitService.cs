using Api.Abstractions;
using Api.Dtos;
using Api.Models;

namespace Api.Services
{
    public class VisitService : IVisitService
    {
        private readonly S22581Context _context;

        public VisitService(S22581Context context)
        {
            _context = context;
        }



        int IVisitService.AddNewVisit(CreateVisitDto dto)
        {
            if (!_context.Patients.Any(p => p.IdPatient == dto.IdPatient))
            {
                throw new Exception("Patient doesn't exist");
            }
            else if (!_context.Doctors.Any(d => d.IdDoctor == dto.IdDoctor))
            {
                throw new Exception("Doctor doesn't exist");
            }
            else if (!_context.Schedules.Any(d => d.IdDoctor == dto.IdDoctor && dto.Date >= d.DateFrom && dto.Date <= d.DateTo))
            {
                throw new Exception("Doctor doesn't work this day");
            }
            else if (_context.Visits
               .Where(v => v.IdDoctor == dto.IdDoctor && v.Date.Date == dto.Date.Date)
               .Count() > 5)
            {
                throw new Exception("Doctor have more then 5 visists");
            }
            else if (_context.Visits.Any(v => v.IdPatient == dto.IdPatient && v.Date > DateTime.Now))
            {
                throw new Exception("Patient already have visit assigned");
            }
            var previousVisitsCount = _context.Visits
              .Count(v => v.IdPatient == dto.IdPatient && v.Date < dto.Date);

            var doctor = _context.Doctors
               .Where(d => d.IdDoctor == dto.IdDoctor)
               .Select(d => new { d.PriceForVisit })
               .FirstOrDefault();

            decimal price = doctor.PriceForVisit;
            if (previousVisitsCount >= 10)
            {
                price *= 0.90m;
            }
            int newVisitId = _context.Visits.Max(v => v.IdVisit);
            var newVisit = new Visit
            {
                IdVisit = newVisitId + 1,
                Date = dto.Date,
                IdPatient = dto.IdPatient,
                IdDoctor = dto.IdDoctor,
                Price = price
            };

            _context.Visits.Add(newVisit);
            _context.SaveChanges();

            return newVisit.IdVisit;
        }
    }
}
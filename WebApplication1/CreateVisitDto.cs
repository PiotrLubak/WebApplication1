namespace Api.Dtos
{
    public class CreateVisitDto
    {
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public DateTime Date { get; set; }
    }
}
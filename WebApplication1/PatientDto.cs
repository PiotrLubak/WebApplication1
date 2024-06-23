namespace Api.Dtos
{
    public class PatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TotalAmountMoneySpent { get; set; }
        public int NumberOfVisists { get; set; }
        public List<VisitDto> Visists { get; set; }
    }
}
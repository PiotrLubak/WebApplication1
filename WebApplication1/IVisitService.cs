using Api.Dtos;
namespace Api.Abstractions
{
    public interface IVisitService
    {
        int AddNewVisit(CreateVisitDto dto);
    }
}
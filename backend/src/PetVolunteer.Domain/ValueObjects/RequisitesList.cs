using PetVolunteer.Domain.Models;

namespace PetVolunteer.Domain.ValueObjects;

public record RequisitesList
{
    public IReadOnlyList<Requisite> Requisites {  get; } = default!;
    
    private RequisitesList() { }
    
    private RequisitesList(IEnumerable<Requisite> list) => Requisites = list.ToList();
    
    public static RequisitesList Create(IEnumerable<Requisite> list) => new(list);
}
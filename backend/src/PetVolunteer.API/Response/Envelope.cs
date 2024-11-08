using PetVolunteer.Domain.Shared;

namespace PetVolunteer.API.Response;

public record Envelope
{
    public object? Result { get; }
    public ErrorList? Errors { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }
    
    public static Envelope Ok(object? result) => new Envelope(result, null); 
    
    public static Envelope Error(ErrorList errors) => new Envelope(null, errors); 
    
    
    
}
namespace PetVolunteer.Application.Messaging;

public interface IMessageQueue<TMessage>
{
    public Task WriteAsync(TMessage fileNames, CancellationToken cancellationToken = default);
    public Task<TMessage> ReadAsync(CancellationToken cancellationToken = default);
}
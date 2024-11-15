namespace PetVolunteer.Infrastructure.MessageQueues;

public interface IMessageQueue<TMessage>
{
    public Task WriteAsync(TMessage fileNames, CancellationToken cancellationToken = default);
    public Task<TMessage> ReadAsync(CancellationToken cancellationToken = default);
}
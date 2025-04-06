namespace SimpleCqrs.Command;
using System.Text.Json.Serialization;

public interface ICommand {
    public Guid CorrelationId { get; }
}

public abstract record CommandBase : ICommand {
    
    /// <summary>
    ///     Correlation Id associated with a given command object.
    /// </summary>
    /// <remarks>
    ///     This value is used for tracing requests back to their origin.
    ///     Each event should contain a correlation Id indicating which command resulted in its creation.
    /// </remarks>
    public Guid CorrelationId { get; init; }
    
    /// <summary>
    /// Default Parameterless Constructor
    /// </summary>
    [JsonConstructor]
    protected CommandBase() {
        CorrelationId = Guid.NewGuid();
    }
}
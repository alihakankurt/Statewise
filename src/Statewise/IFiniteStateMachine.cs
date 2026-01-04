namespace Statewise;

/// <summary>
/// Represents an abstract context that manages a finite set of states.
/// </summary>
/// <typeparam name="TKey">The key type of the states.</typeparam>
public interface IFiniteStateMachine<TKey>
    where TKey : notnull
{
    /// <summary>
    /// Gets the key of the current state.
    /// </summary>
    TKey Current { get; }

    /// <summary>
    /// Gets the key of the previous state.
    /// </summary>
    TKey Previous { get; }

    /// <summary>
    /// Enters the current state by invoking the associated logic.
    /// </summary>
    /// <remarks>
    /// Consider calling once when starting the state machine.
    /// </remarks>
    void EnterState();

    /// <summary>
    /// Exits the current state by invoking the associated logic.
    /// </summary>
    /// <remarks>
    /// Consider calling once when stopping the state machine.
    /// </remarks>
    void ExitState();

    /// <summary>
    /// Updates the current state, typically called once per frame or tick.
    /// </summary>
    /// <param name="deltaTime">The elapsed time in seconds since the last update.</param>
    void UpdateState(double deltaTime);

    /// <summary>
    /// Changes the current state by transitioning to a new one.
    /// </summary>
    /// <param name="key">The key of the target state.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when a state with the <paramref name="key"/> does not exist.
    /// </exception>
    void ChangeState(TKey key);
}

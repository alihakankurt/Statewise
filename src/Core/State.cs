using System;
using System.Runtime.CompilerServices;

namespace Core;

/// <summary>
/// Represents an abstract lifecycle behavior within a finite state machine.
/// </summary>
/// <typeparam name="TKey">The key type.</typeparam>
public abstract class State<TKey>
    where TKey : notnull
{
    /// <summary>
    /// Gets the unique key that identifies this state.
    /// </summary>
    public abstract TKey Key { get; }

    /// <summary>
    /// Gets the finite state machine this state belongs to.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when trying to access without a finite state machine.
    /// </exception>
    public IFiniteStateMachine<TKey> Owner
    {
        get => field ?? throw new InvalidOperationException("This state does not belong to a state machine");
        internal set;
    }

    /// <summary>
    /// Called by the finite state machine on entering this state.
    /// <para>Override this method to implement entering behavior.</para>
    /// </summary>
    protected internal virtual void Enter()
    {
    }

    /// <summary>
    /// Called by the finite state machine on exiting this state.
    /// <para>Override this method to implement exiting behavior.</para>
    /// </summary>
    protected internal virtual void Exit()
    {
    }

    /// <summary>
    /// Called by the finite state machine on each update while this state is active.
    /// <para>Override this method to implement per-frame behavior.</para>
    /// </summary>
    /// <param name="deltaTime">The elapsed time in seconds since the last update.</param>
    protected internal virtual void Update(double deltaTime)
    {
    }

    /// <summary>
    /// Changes the current active state of the finite state machine.
    /// <para>Alternative to the <see cref="IFiniteStateMachine{TKey}.ChangeState(TKey)"/>.</para>
    /// </summary>
    /// <param name="key">The key of the target state.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void TransitionTo(TKey key)
    {
        Owner.ChangeState(key);
    }
}

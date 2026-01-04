using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace Statewise;

/// <summary>
/// Fluent builder pattern for <see cref="IFiniteStateMachine{TKey}"/> implementation.
/// </summary>
/// <typeparam name="TKey">The key type of the states.</typeparam>
public readonly ref struct FiniteStateMachineBuilder<TKey>
    where TKey : notnull
{
    private readonly Dictionary<TKey, State<TKey>> _states;
    private readonly State<TKey> _initialState;

    /// <exception cref="NotImplementedException"/>
    [Obsolete($"Please, use {nameof(WithInitialState)} methods to create a builder.", true)]
    public FiniteStateMachineBuilder()
    {
        throw new NotImplementedException();
    }

    private FiniteStateMachineBuilder(State<TKey> initialState)
    {
        _states = [];
        _states[initialState.Key] = initialState;
        _initialState = initialState;
    }

    /// <summary>
    /// Creates a new builder with an instance of <typeparamref name="TState"/> as the initial state,
    /// instantiating is made using its parameterless constructor.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <returns>A new instance of <see cref="FiniteStateMachineBuilder{TKey}"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static FiniteStateMachineBuilder<TKey> WithInitialState<TState>()
        where TState : State<TKey>, new()
    {
        var state = new TState();
        return WithInitialState(state);
    }

    /// <summary>
    /// Creates a new builder with the <paramref name="state"/> as the initial state.
    /// </summary>
    /// <param name="state">The state instance.</param>
    /// <returns>A new instance of <see cref="FiniteStateMachineBuilder{TKey}"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static FiniteStateMachineBuilder<TKey> WithInitialState(State<TKey> state)
    {
        ArgumentNullException.ThrowIfNull(state);
        return new FiniteStateMachineBuilder<TKey>(state);
    }

    /// <summary>
    /// Adds a new instance of <typeparamref name="TState"/> to the states,
    /// instantiating is made using its parameterless constructor.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <returns>The instance of <see cref="FiniteStateMachineBuilder{TKey}"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    public readonly FiniteStateMachineBuilder<TKey> WithState<TState>()
        where TState : State<TKey>, new()
    {
        var state = new TState();
        return WithState(state);
    }

    /// <summary>
    /// Adds the <paramref name="state"/> to the states.
    /// </summary>
    /// <param name="state">The state instance.</param>
    /// <returns>The instance of <see cref="FiniteStateMachineBuilder{TKey}"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    public readonly FiniteStateMachineBuilder<TKey> WithState(State<TKey> state)
    {
        ArgumentNullException.ThrowIfNull(state);
        if (_states.TryAdd(state.Key, state))
        {
            return this;
        }

        throw new InvalidOperationException($"A state with this key is already exist.");
    }

    /// <summary>
    /// Builds a finite state machine with registered states.
    /// </summary>
    /// <returns>A new instance of <see cref="IFiniteStateMachine{TKey}"/>.</returns>
    /// <exception cref="InvalidOperationException"/>
    public readonly IFiniteStateMachine<TKey> Build()
    {
        if (_states.Count == 0)
        {
            throw new InvalidOperationException("A finite state machine is already built.");
        }

        var states = _states.ToFrozenDictionary();
        _states.Clear();

        return new FiniteStateMachine<TKey>(states, _initialState);
    }
}

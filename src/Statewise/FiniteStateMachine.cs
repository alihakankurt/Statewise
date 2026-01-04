using System;
using System.Collections.Frozen;
using System.Runtime.CompilerServices;

namespace Statewise;

internal class FiniteStateMachine<TKey> : IFiniteStateMachine<TKey>
    where TKey : notnull
{
    private readonly FrozenDictionary<TKey, State<TKey>> _states;
    private State<TKey> _currentState;

    public TKey Current => _currentState.Key;

    public TKey Previous { get; private set; }

    internal FiniteStateMachine(FrozenDictionary<TKey, State<TKey>> states, State<TKey> initialState)
    {
        _states = states;
        _currentState = initialState;
        Previous = default!;

        foreach (State<TKey> state in _states.Values)
        {
            state.Owner = this;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnterState()
    {
        _currentState.Enter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ExitState()
    {
        _currentState.Exit();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UpdateState(double deltaTime)
    {
        _currentState.Update(deltaTime);
    }

    public void ChangeState(TKey key)
    {
        State<TKey> state = FindState(key);

        _currentState.Exit();

        Previous = Current;
        _currentState = state;

        _currentState.Enter();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private State<TKey> FindState(TKey key)
    {
        if (_states.TryGetValue(key, out var state))
        {
            return state;
        }

        throw new ArgumentException("Could not find a state with the specified key");
    }
}

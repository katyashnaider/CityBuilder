using System;
using System.Collections.Generic;

namespace CityBuilder.Workers.StateMachine
{
    public class StateMachine
    {

        private static readonly List<Transition> EmptyTransitions = new List<Transition>(0);
        private readonly List<Transition> _anyTransitions = new List<Transition>();
        private IState _currentState;
        private List<Transition> _currentTransitions = new List<Transition>();
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

        private readonly Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();

        public void Tick()
        {
            Transition transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.To);
            }

            _currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
            {
                return;
            }

            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);

            if (_currentTransitions == null)
            {
                _currentTransitions = EmptyTransitions;
            }

            _currentState.OnEnter();
        }

        public void SetState<T>() where T : IState
        {
            _currentState?.OnExit();
            _currentState = _states[typeof(T)];

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);

            if (_currentTransitions == null)
            {
                _currentTransitions = EmptyTransitions;
            }

            _currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out List<Transition> transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            if (!_states.TryGetValue(from.GetType(), out IState _))
            {
                _states.Add(from.GetType(), from);
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (Transition transition in _anyTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

            foreach (Transition transition in _currentTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

            return null;
        }

        private class Transition
        {

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
            public Func<bool> Condition { get; }
            public IState To { get; }
        }
    }
}
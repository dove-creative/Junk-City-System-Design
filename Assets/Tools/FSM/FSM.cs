using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JunkCity.Tools
{
    public partial class FSM<T>
    {
        public bool IsActive { get; private set; } = false;
        public event Action<string> OnStateChanged;

        private T owner;

        private Dictionary<string, State> states;
        private string defaultState;
        private string currentState;


        public FSM(T owner, IEnumerable<State> states, string defaultState = null)
        {
            if (!states.Any())
                throw new ArgumentException("FSM의 생성에는 하나 이상의 states가 필요합니다.", nameof(states));
            if (defaultState != null && !states.Any(s => s.Name == defaultState))
                throw new ArgumentException($"입력 states에 defaultState인 '{defaultState}'이(가) 포함되어 있지 않습니다.", nameof(defaultState));

            this.owner = owner;
            this.states = states.ToDictionary(s => s.Name);
            this.defaultState = defaultState ?? states.First().Name;

            foreach (var state in states)
                state.SetParent(this);
        }

        public void Open()
        {
            if (IsActive)
                return;

            IsActive = true;
            SetState(defaultState);
        }

        public void Invoke()
        {
            if (!IsActive)
                return;

            states[currentState].Invoke();
        }

        public void SetState(string name)
        {
            if (!IsActive)
            {
                Debug.LogWarning("Active 상태가 아닌 FSM의 SetState 메서드를 호출할 수 없습니다.");
                return;
            }

            if (!states.ContainsKey(name))
                throw new ArgumentException($"해당 FSM에 입력 state인 '{name}'이(가) 포함되어 있지 않습니다.", nameof(name));

            if (!string.IsNullOrEmpty(currentState))
                states[currentState].Close();

            currentState = name;
            states[name].Open();

            OnStateChanged?.Invoke(name);
        }

        public void Close()
        {
            if (!IsActive)
                return;

            states[currentState].Close();
            currentState = null;

            IsActive = false;
        }
    }
}

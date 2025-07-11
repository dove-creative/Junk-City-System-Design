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
                throw new ArgumentException("FSM�� �������� �ϳ� �̻��� states�� �ʿ��մϴ�.", nameof(states));
            if (defaultState != null && !states.Any(s => s.Name == defaultState))
                throw new ArgumentException($"�Է� states�� defaultState�� '{defaultState}'��(��) ���ԵǾ� ���� �ʽ��ϴ�.", nameof(defaultState));

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
                Debug.LogWarning("Active ���°� �ƴ� FSM�� SetState �޼��带 ȣ���� �� �����ϴ�.");
                return;
            }

            if (!states.ContainsKey(name))
                throw new ArgumentException($"�ش� FSM�� �Է� state�� '{name}'��(��) ���ԵǾ� ���� �ʽ��ϴ�.", nameof(name));

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

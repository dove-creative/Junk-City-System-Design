using System;

namespace JunkCity.Tools
{
    public partial class FSM<T>
    {
        public abstract class State
        {
            public string Name { get; private set; }

            public event Action OnOpen;
            public event Action OnInvoke;
            public event Action OnClose;

            private FSM<T> parent;
            protected T Owner => parent.owner;


            public State(string name) => Name = name;
            internal void SetParent(FSM<T> parent) => this.parent = parent;

            public virtual void Open() => OnOpen?.Invoke();
            public virtual void Invoke() => OnInvoke?.Invoke();
            public virtual void Close() => OnClose?.Invoke();
        }
    }
}

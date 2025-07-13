using System;

namespace JunkCity.Tools
{
    public partial class FSM<T>
    {
        public class State
        {
            public string Name { get; private set; }

            public Action OnOpen;
            public Action OnInvoke;
            public Action OnClose;

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

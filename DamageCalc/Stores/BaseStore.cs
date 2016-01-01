using DamageCalc.Components;
using DamageCalc.Dispatcher;
using System;

namespace DamageCalc.Stores
{
    abstract class BaseStore
    {
        public event EventHandler<Payload> EmitChange;

        public BaseStore(PluginDispatcher d)
        {
            dispatcher = d;
            dispatcher.Dispatch += new EventHandler<Payload>(OnDispatch);
        }

        protected PluginDispatcher dispatcher;

        protected BaseStore() { }

        abstract protected void OnDispatch(object sender, Payload e);

        protected virtual void EmitChanges(Payload changeType)
        {
            if (EmitChange != null)
                EmitChange(this, changeType);
        }
    }
}

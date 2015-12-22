﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ghostpunch.OnlyDown
{
    public class WeakAction
    {
        private Action _staticAction;

        protected MethodInfo Method { get; set; }

        public virtual string MethodName
        {
            get
            {
                if (_staticAction != null)
                    return _staticAction.Method.Name;

                return Method.Name;
            }
        }

        protected WeakReference ActionReference { get; set; }

        protected WeakReference Reference { get; set; }

        public bool IsStatic { get { return _staticAction != null; } }

        public virtual bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null)
                    return false;

                if (_staticAction != null)
                {
                    if (Reference != null)
                        return Reference.IsAlive;

                    return true;
                }

                return Reference.IsAlive;
            }
        }

        public object Target
        {
            get
            {
                if (Reference == null)
                    return null;

                return Reference.Target;
            }
        }

        protected object ActionTarget
        {
            get
            {
                if (ActionReference == null)
                    return null;

                return ActionReference.Target;
            }
        }

        #region Constructors 

        protected WeakAction() { }

        public WeakAction(Action action) : this(action == null ? null : action.Target, action) { }

        public WeakAction(object target, Action action)
        {
            if (action.Method.IsStatic)
            {
                _staticAction = action;

                if (target != null)
                    Reference = new WeakReference(target);

                return;
            }

            Method = action.Method;
            ActionReference = new WeakReference(action.Target);
            Reference = new WeakReference(target);
        }

        #endregion

        public void Execute()
        {
            if (_staticAction != null)
            {
                _staticAction();
                return;
            }

            var actionTarget = ActionTarget;

            if (IsAlive)
            {
                if (Method != null &&
                    ActionReference != null &&
                    ActionTarget != null)
                {
                    Method.Invoke(actionTarget, null);

                    return;
                }
            }
        }

        public void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            Method = null;
            _staticAction = null;
        }
    }
}

using System;

namespace Ghostpunch.OnlyDown.Messaging
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null) { }
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object param)
        {
            if (_canExecute == null)
                return true;

            if (param == null &&
#if NETFX_CORE
                typeof(T).GetTypeInfo().IsValueType)
#else
                typeof(T).IsValueType)
#endif
            {
                return _canExecute(default(T));
            }

            if (param == null || param is T)
                return _canExecute((T)param);

            return false;
        }

        public void Execute(object param)
        {
            var val = param;

#if !NETFX_CORE
            if (param != null && param.GetType() != typeof(T))
            {
                if (param is IConvertible)
                    val = Convert.ChangeType(param, typeof(T), null);
            }
#endif

            if (CanExecute(val) && _execute != null)
            {
                if (val == null)
                {
#if NETFX_CORE
                    if (typeof(T).GetTypeInfo().IsValueType)
#else
                    if (typeof(T).IsValueType)
#endif
                    {
                        _execute(default(T));
                        return;
                    }
                }

                _execute((T)val);
            }
        }
    }
}

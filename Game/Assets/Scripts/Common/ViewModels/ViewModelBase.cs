using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.mediation.impl;

namespace Ghostpunch.OnlyDown.Common.ViewModels
{
    public abstract class ViewModelBase<V> : Mediator where V : ViewBase
    {
        [Inject]
        public V View { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View.Initialize();
        }
    }

    /// <summary>
    /// A base class for the ViewModel classes in the MVVM pattern.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs after a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Provides access to the PropertyChanged event handler to derived classes.
        /// </summary>
        protected PropertyChangedEventHandler PropertyChangedHandler
        {
            get { return PropertyChanged; }
        }

        public void VerifyPropertyName(string propertyName)
        {
            var myType = GetType();

#if NETFX_CORE
            if (!string.IsNullOrEmpty(propertyName) && myType.GetTypeInfo().GetDeclaredProperty(propertyName) == null)
                throw new ArgumentException("Property not found", propertyName);
#else
            if (!String.IsNullOrEmpty(propertyName) && myType.GetProperty(propertyName) == null)
            {
                var descriptor = this as ICustomTypeDescriptor;

                if (descriptor != null)
                {
                    if (descriptor.GetProperties()
                        .Cast<PropertyDescriptor>()
                        .Any(property => property.Name == propertyName))
                        return;
                }

                throw new ArgumentException("Property not found", propertyName);
            }
#endif
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("Invalid argument", "propertyExpression");

            var property = body.Member as PropertyInfo;

            if (property == null)
                throw new ArgumentException("Argument is not a property", "propertyExpression");

            return property.Name;
        }

        protected bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;

            RaisePropertyChanged(propertyName);

            return true;
        }

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;

            RaisePropertyChanged(propertyExpression);

            return true;
        }
    }
}

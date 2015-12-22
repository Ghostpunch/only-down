using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    public abstract class BaseView<TViewModel> : MonoBehaviour where TViewModel : ObservableObject
    {
        protected TViewModel ViewModel { get; private set; }

        protected virtual void Awake()
        {
            ViewModel = gameObject.AddComponent<TViewModel>();
        }
    }
}

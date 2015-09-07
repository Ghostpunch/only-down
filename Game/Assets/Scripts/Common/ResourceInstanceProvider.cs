using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.framework.api;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Common
{
    public class ResourceInstanceProvider : IInstanceProvider
    {
        // The GameObject instantiated from the prefab
        GameObject _prototype;

        // The name of the resource in Unity's resources folder
        private string _resourceName;

        // The Render Layer to which the GameObjects will be assigned
        private int _layer;

        // An id tacked on to the name to make it easier to track individual instances
        private int _id = 0;

        public ResourceInstanceProvider(string name, int layer)
        {
            _resourceName = name;
            _layer = layer;
        }

        #region IInstanceProvider implementation

        public object GetInstance(Type key)
        {
            if (_prototype == null)
            {
                // Get the resource from Unity
                _prototype = Resources.Load<GameObject>(_resourceName);
                _prototype.transform.localScale = Vector3.one;
            }

            // Copy the prototype
            var go = GameObject.Instantiate<GameObject>(_prototype);
            go.layer = _layer;
            go.name = String.Format("{0}_{1}", _resourceName, _id++);

            return go;
        }

        public T GetInstance<T>()
        {
            return (T)GetInstance(typeof(T));
        }

        #endregion
    }
}

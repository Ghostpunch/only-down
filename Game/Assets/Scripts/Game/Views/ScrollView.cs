using UnityEngine;
using System.Collections;
using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.signal.impl;

namespace Ghostpunch.OnlyDown.Game.Views
{
    public class ScrollView : ViewBase
    {
        public int _scrollAmount = 5;
        public float _scrollAnimationLength = 1f;

        public Signal _onScrollView = new Signal();

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.Player)
                _onScrollView.Dispatch();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common.Views;
using strange.extensions.signal.impl;

namespace Ghostpunch.OnlyDown.Game.Views
{
    public class EnvironmentView : ViewBase
    {
        public int _gridCellSize = 1;
        public int _gridWidth = 11;
        public int _gridHeight = 11;

        public Signal _animateUp = new Signal();

        internal override void Initialize()
        {
            base.Initialize();

            _animateUp.AddListener(OnAnimateUp);
        }

        private void OnAnimateUp()
        {
        }
    }
}

using System;
using Ghostpunch.OnlyDown.Common.ViewModels;
using Ghostpunch.OnlyDown.Game.Views;

namespace Ghostpunch.OnlyDown.Game.ViewModels
{
    public class ScrollViewModel : ViewModelBase<ScrollView>
    {
        [Inject]
        public LevelScrollSignal LevelScroll { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            View._onScrollView.AddListener(OnScrollView);
        }

        private void OnScrollView()
        {
            LevelScroll.Dispatch(View._scrollAnimationLength, View._scrollAmount);
        }
    }
}
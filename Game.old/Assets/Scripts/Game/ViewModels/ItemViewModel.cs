//using Ghostpunch.OnlyDown.Common.ViewModels;
//using Ghostpunch.OnlyDown.Game.Views;
//using strange.extensions.pool.api;
//using UnityEngine;

//namespace Ghostpunch.OnlyDown.Game.ViewModels
//{
//    public class ItemViewModel : ViewModelBase<ItemView>
//    {
//        [Inject(GameElement.ItemPool)]
//        public IPool<GameObject> ItemPool { get; set; }

//        private static Vector3 GTFO_POS = new Vector3(1000f, 0, 1000f);

//        public override void OnRegister()
//        {
//            base.OnRegister();

//            View._pickupSignal.AddListener(OnPickup);
//        }

//        private void OnPickup()
//        {
//            gameObject.SetActive(false);
//            gameObject.transform.localPosition = GTFO_POS;
//            ItemPool.ReturnInstance(gameObject);
//        }
//    }
//}
//using System;
//using System.Collections;
//using Ghostpunch.OnlyDown.Common.ViewModels;
//using Ghostpunch.OnlyDown.Game.Views;
//using strange.extensions.pool.api;
//using UnityEngine;

//namespace Ghostpunch.OnlyDown.Game.ViewModels
//{
//    public class EnvironmentViewModel : ViewModelBase<EnvironmentView>
//    {
//        [Inject]
//        public PlayerDigSignal PlayerDig { get; set; }

//        [Inject]
//        public LevelScrollSignal LevelScroll { get; set; }

//        [Inject(GameElement.EnemyPool)]
//        public IPool<GameObject> EnemyPool { get; set; }

//        [Inject(GameElement.SandPool)]
//        public IPool<GameObject> SandPool { get; set; }

//        [Inject(GameElement.ItemPool)]
//        public IPool<GameObject> ItemPool { get; set; }

//        private GameObject[] _levelGrid = null;
//        private Transform _transform = null;

//        private static Vector3 GTFO_POS = new Vector3(1000f, 0, 1000f);

//        public override void OnRegister()
//        {
//            base.OnRegister();

//            _transform = transform;

//            PlayerDig.AddListener(OnPlayerDig);
//            LevelScroll.AddListener(OnLevelScroll);

//            GenerateLevel();
//        }

//        public override void OnRemove()
//        {
//            base.OnRemove();

//            PlayerDig.RemoveListener(OnPlayerDig);
//            LevelScroll.RemoveListener(OnLevelScroll);

//            _levelGrid = null;
//        }

//        private void GenerateLevel()
//        {
//            var startingX = (_transform.localPosition.x - View._gridWidth * 0.5f) + 0.5f;
//            var startingY = (_transform.localPosition.y + View._gridHeight * 0.5f) - 0.5f;

//            _levelGrid = new GameObject[View._gridWidth * View._gridHeight];

//            var sandParent = _transform.FindChild("Sand") ?? CreateSandParent("Sand", _transform);

//            for (int y = 0; y < View._gridHeight; ++y)
//            {
//                //var rowName = "SandRow_" + y;
//                //var rowParent = sandParent.FindChild(rowName) ?? CreateSandParent(rowName, sandParent);
//                var row = GenerateRow(new Vector3(startingX, startingY - (y * View._gridCellSize), 0), sandParent);

//                for (int x = 0; x < row.Length; ++x)
//                {
//                    _levelGrid[x + View._gridWidth * y] = row[x];
//                }
//            }
//        }

//        private Transform CreateSandParent(string name, Transform parent)
//        {
//            var sandParent = new GameObject(name);
//            sandParent.transform.localPosition = Vector3.zero;
//            sandParent.transform.parent = parent;

//            return sandParent.transform;
//        }

//        private GameObject[] GenerateRow(Vector3 position, Transform parent)
//        {
//            var row = new GameObject[View._gridWidth];

//            for (int x = 0; x < View._gridWidth; ++x)
//            {
//                var newLocation = new Vector3(position.x + (x * View._gridCellSize), position.y, position.z + 1);
//                var gridUnitType = UnityEngine.Random.Range(0, 100);
//                GameObject gridUnitGO = null;

//                if (gridUnitType <= 95)
//                {
//                    gridUnitGO = SandPool.GetInstance();
//                    row[x] = gridUnitGO;
//                }
//                else if (gridUnitType <= 97)
//                    gridUnitGO = ItemPool.GetInstance();
//                else if (gridUnitType <= 100)
//                    gridUnitGO = EnemyPool.GetInstance();

//                gridUnitGO.transform.localPosition = newLocation;
//                gridUnitGO.transform.parent = parent;
//                gridUnitGO.SetActive(true);
//            }

//            return row;
//        }

//        #region Digging
//        private void OnPlayerDig(Vector3 playerLocation)
//        {
//            // i = x + gridWidth * y;
//            // x = i % gridWidth;
//            // y = i / gridWidth;
//            var xCoord = (int)(playerLocation.x + View._gridWidth * 0.5f);
//            var yCoord = (int)((View._gridHeight * 0.5f) - playerLocation.y);
//            var index = xCoord + View._gridWidth * yCoord;

//            DestroySand(_levelGrid[xCoord + View._gridWidth * (yCoord + 1)]);

//            View._animateUp.Dispatch();
//        }

//        private void DestroySand(GameObject sandTile)
//        {
//            sandTile.SetActive(false);
//            sandTile.transform.localPosition = GTFO_POS;
//            SandPool.ReturnInstance(sandTile);
//        }
//        #endregion

//        #region Scrolling
//        private void OnLevelScroll(float animationLength, int scrollAmount)
//        {
//            StartCoroutine(ScrollAndGenerateContent(animationLength, scrollAmount));
//        }

//        private IEnumerator ScrollAndGenerateContent(float animationLength, int scrollAmount)
//        {
//            var startingX = (_transform.localPosition.x - View._gridWidth * 0.5f) + 0.5f;
//            var startingY = (_transform.localPosition.y - View._gridHeight * 0.5f) - 0.5f;
//            Debug.Log(String.Format("({0}, {1})", startingX, startingY));

//            var sandParent = _transform.FindChild("Sand") ?? CreateSandParent("Sand", _transform);
//            var lastTimeScale = Time.timeScale;

//            Time.timeScale = 0.125f;
//            var oneOverScrollAmount = 1f / scrollAmount;
//            while (scrollAmount > 0)
//            {
//                // Generate extra row
//                var newRow = GenerateRow(new Vector3(startingX, startingY, 0), sandParent);

//                yield return null;

//                // Animate the level up
//                var elapsedTime = 0f;
//                while (elapsedTime < 1f)
//                {
//                    yield return null;

//                    AnimateArray(_levelGrid, Vector3.up, elapsedTime * oneOverScrollAmount);
//                    AnimateArray(newRow, Vector3.up, elapsedTime * oneOverScrollAmount);

//                    elapsedTime += Time.unscaledDeltaTime / animationLength;
//                }

//                yield return null;

//                // Destroy top row and shift everything up
//                for (int y = 0; y < View._gridHeight; ++y)
//                {
//                    for (int x = 0; x < View._gridWidth; ++x)
//                    {
//                        var currentIndex = x + y * View._gridWidth;
//                        if (y == 0)
//                        {
//                            DestroySand(_levelGrid[currentIndex]);
//                            continue;
//                        }
//                        else if (y == View._gridHeight - 1)
//                        {
//                            _levelGrid[currentIndex] = newRow[x];
//                            continue;
//                        }

//                        _levelGrid[currentIndex] = _levelGrid[x + (y + 1) * View._gridWidth];
//                    }
//                }

//                --scrollAmount;
//            }

//            Time.timeScale = lastTimeScale;
//        }

//        private void AnimateArray(GameObject[] array, Vector3 offset, float elapsedTime)
//        {
//            var positions = new Vector3[array.Length][];
//            for (int i = 0; i < positions.Length; ++i)
//            {
//                positions[i] = new Vector3[2];
//                positions[i][0] = array[i].transform.localPosition;
//                positions[i][1] = positions[i][0] + Vector3.up;
//            }

//            for (int i = 0; i < positions.Length; ++i)
//                array[i].transform.localPosition = Vector3.Lerp(positions[i][0], positions[i][1], elapsedTime);
//        }

//        private void ShiftGridUp()
//        {
//            //for (int x = 0; x < View._gridWidth; ++x)
//            //{
//            //    DestroySand(_levelGrid[x]);
//            //}

//            for (int y = 1; y < View._gridHeight - 1; ++y)
//            {
//                for (int x = 0; x < View._gridWidth; ++x)
//                {
//                    _levelGrid[x + y * View._gridWidth] = _levelGrid[x + (y + 1) * View._gridWidth];
//                }
//            }
//        }
//        #endregion

//    }
//}

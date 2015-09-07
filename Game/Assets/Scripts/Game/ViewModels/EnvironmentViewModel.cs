﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Common;
using Ghostpunch.OnlyDown.Game.Views;
using strange.extensions.pool.api;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Game.ViewModels
{
    public class EnvironmentViewModel : ViewModelBase<EnvironmentView>
    {
        [Inject]
        public PlayerDigSignal PlayerDig { get; set; }

        [Inject(GameElement.SandPool)]
        public IPool<GameObject> SandPool { get; set; }

        private GameObject[,] _levelGrid = null;
        private Transform _transform = null;

        private static Vector3 GTFO_POS = new Vector3(1000f, 0, 1000f);

        public override void OnRegister()
        {
            base.OnRegister();

            _transform = transform;

            PlayerDig.AddListener(OnPlayerDig);

            GenerateLevel();
        }

        private void GenerateLevel()
        {
            var startingX = (int)(transform.localPosition.x - View._gridWidth * 0.5f);
            var startingY = (int)(transform.localPosition.y + View._gridHeight * 0.5f);

            _levelGrid = new GameObject[View._gridWidth, View._gridHeight];

            var sandParent = _transform.FindChild("Sand") ?? CreateSandParent("Sand", _transform);

            for (int y = 0; y < View._gridHeight; ++y)
            {
                var rowName = "SandRow_" + y;
                var rowParent = sandParent.FindChild(rowName) ?? CreateSandParent(rowName, sandParent);
                GenerateSandRow(new Vector3(startingX, startingY - (y * View._gridCellSize), 0), y, rowParent);
            }
        }

        private Transform CreateSandParent(string name, Transform parent)
        {
            var sandParent = new GameObject(name);
            sandParent.transform.localPosition = Vector3.zero;
            sandParent.transform.parent = parent;

            return sandParent.transform;
        }

        private void GenerateSandRow(Vector3 position, int yCoord, Transform parent)
        {
            for (int x = 0; x < View._gridWidth; ++x)
            {
                var newLocation = new Vector3(position.x + (x * View._gridCellSize), position.y, position.z + 1);

                var sandGO = SandPool.GetInstance();
                sandGO.transform.localPosition = newLocation;
                sandGO.transform.parent = parent;
                sandGO.SetActive(true);

                _levelGrid[x, yCoord] = sandGO;
            }
        }

        private void OnPlayerDig(Vector3 playerLocation)
        {
            var xCoord = (int)(playerLocation.x + View._gridWidth * 0.5f);
            var yCoord = (int)(playerLocation.y + View._gridHeight * 0.5f);

            DestroySand(_levelGrid[xCoord, yCoord + 1]);
        }

        private void DestroySand(GameObject sandTile)
        {
            sandTile.SetActive(false);
            sandTile.transform.localPosition = GTFO_POS;
            SandPool.ReturnInstance(sandTile);
        }
    }
}

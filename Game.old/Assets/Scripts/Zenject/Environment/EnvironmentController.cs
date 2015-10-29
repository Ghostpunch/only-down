using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlyDown.Messaging;
using UnityEngine;
using Zenject;

namespace OnlyDown.Environment
{
    public class EnvironmentController : IInitializable
    {
        private EnvironmentView _view = null;
        private Settings _settings = null;
        private Transform _transform = null;
        private IMessageSystem _messageSystem = null;
        private Camera _mainCamera = null;

        #region Commands
        private RelayCommand<Collider> _onLevelScroll;
        private RelayCommand<Collider> OnLevelScroll
        {
            get
            {
                return _onLevelScroll ?? (_onLevelScroll = new RelayCommand<Collider>(collision =>
                {
                    // At this point we need to scroll the level up
                }));
            }
        }
        #endregion

        public EnvironmentController(EnvironmentView view, Settings settings, 
            [Inject("Main")]Camera camera, IMessageSystem messageSystem)
        {
            _view = view;
            _settings = settings;
            _mainCamera = camera;
            _messageSystem = messageSystem;
        }

        public void Initialize()
        {
            _transform = _view.transform;
            _messageSystem.Subscribe(GameMessages.PlayerHitScrollPoint, OnLevelScroll);
            _view.StartCoroutine(GenerateLevel());
            _view.StartCoroutine(GenerateWalls());
        }

        private IEnumerator GenerateLevel()
        {
            var sandParent = (new GameObject("Sand Parent")).transform;
            sandParent.localPosition = Vector3.zero;
            sandParent.parent = _transform;

            var aspectRatio = (float)Screen.width / (float)Screen.height;
            var camHorizontalExtent = _mainCamera.orthographicSize * aspectRatio;

            var bottomEdge = 0 - (_settings._gridHeight / 2);
            var leftEdge = Mathf.Ceil(-camHorizontalExtent) - .5f;
            var rightEdge = Mathf.Ceil(camHorizontalExtent) + .5f;
            for (int y = (int)_settings._startingYPos; y > bottomEdge; --y)
            {
                var sandVariation = UnityEngine.Random.Range(0, _settings._sandVariations - 1);
                var x = leftEdge;
                var i = 0;

                do
                {
                    if (i >= 6) i = 0;

                    var prototype = _settings._sandPrefabs[sandVariation * _settings._sandVariations + i];
                    var newGo = GameObject.Instantiate<GameObject>(prototype);
                    var sprite = newGo.GetComponentInChildren<SpriteRenderer>();

                    newGo.transform.parent = sandParent;
                    newGo.transform.localPosition = new Vector3(x, y - 0.5f, -1);

                    x += sprite.bounds.size.x;
                    ++i;
                } while (x < rightEdge);
            }

            yield return new WaitForEndOfFrame();
        }

        private IEnumerator GenerateWalls()
        {
            var wallsParent = (new GameObject("Walls Parent")).transform;
            wallsParent.localPosition = Vector3.zero;
            wallsParent.parent = _transform;

            var aspectRatio = (float)Screen.width / (float)Screen.height;
            var camHorizontalExtent = _mainCamera.orthographicSize * aspectRatio;

            var bottomEdge = 0 - (_settings._gridHeight / 2);
            var leftEdge = Mathf.Floor(-camHorizontalExtent) + .5f;
            var rightEdge = Mathf.Floor(camHorizontalExtent) + .5f;

            var y = _settings._startingYPos;
            var i = 0;
            do
            {
                if (i >= 8) i = 0;

                // Left wall
                var prototype = _settings._leftWallPiecePrefabs[i];
                var newWall = GameObject.Instantiate<GameObject>(prototype);
                var sprite = newWall.GetComponentInChildren<SpriteRenderer>();

                newWall.transform.parent = wallsParent;
                newWall.transform.localPosition = new Vector3(leftEdge, y - 0.5f, -0.5f);

                // Right wall
                prototype = _settings._rightWallPiecePrefabs[i];
                newWall = GameObject.Instantiate<GameObject>(prototype);
                sprite = newWall.GetComponentInChildren<SpriteRenderer>();

                newWall.transform.parent = wallsParent;
                newWall.transform.localPosition = new Vector3(rightEdge, y - 0.5f, -0.5f);

                y -= sprite.bounds.size.y;
                ++i;
            } while (y > bottomEdge);

            yield return new WaitForEndOfFrame();
        }

        [Serializable]
        public class Settings
        {
            public float _cellSize;
            public int _sandVariations;
            public int _gridWidth;
            public int _gridHeight;
            public float _startingYPos;

            public GameObject[] _leftWallPiecePrefabs;
            public GameObject[] _rightWallPiecePrefabs;
            public GameObject[] _sandPrefabs;
        }
    }
}

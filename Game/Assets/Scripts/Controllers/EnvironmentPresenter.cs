using UnityEngine;
using System.Collections;
using System;
using Ghostpunch.OnlyDown.Messaging;
using Zenject;
using System.Collections.Generic;

namespace Ghostpunch.OnlyDown
{
    public class EnvironmentPresenter : IInitializable
    {
        private EnvironmentView _view = null;
        private Settings _settings = null;
        private IMessageSystem _messageSystem = null;
        private Camera _mainCamera = null;

        private Transform _transform = null;

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

        public EnvironmentPresenter(EnvironmentView view, Settings settings, Camera mainCamera, IMessageSystem messageSystem)
        {
            _view = view;
            _settings = settings;
            _mainCamera = mainCamera;
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

            var bottomEdge = -_mainCamera.orthographicSize;
            var leftEdge = Mathf.Ceil(-camHorizontalExtent) - 0.5f;
            var rightEdge = Mathf.Ceil(camHorizontalExtent) + 0.5f;

            for (int y = 0; y > bottomEdge; --y)
            {
                var x = leftEdge;
                var i = 0;

                do
                {
                    var prototype = _settings._sandPrefabs[i];
                    var newGo = GameObject.Instantiate<GameObject>(prototype);
                    var sprite = newGo.GetComponent<SpriteRenderer>();

                    newGo.transform.parent = sandParent;
                    newGo.transform.localPosition = new Vector3(x, y - 0.5f, 1f);

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

            var bottomEdge = -_mainCamera.orthographicSize;
            var leftEdge = Mathf.Ceil(-camHorizontalExtent);
            var rightEdge = Mathf.Floor(camHorizontalExtent);

            var y = _settings._startingYPos;
            var i = 0;

            do
            {
                if (i > _settings._leftWallPrefabs.Length) i = 0;

                // Left Wall
                var prototype = _settings._leftWallPrefabs[i];
                var newWall = GameObject.Instantiate<GameObject>(prototype);
                var sprite = newWall.GetComponent<SpriteRenderer>();

                newWall.transform.parent = wallsParent;
                newWall.transform.localPosition = new Vector3(leftEdge, y - 0.5f, 0);

                // Right Wall
                prototype = _settings._rightWallPrefabs[i];
                newWall = GameObject.Instantiate<GameObject>(prototype);

                newWall.transform.parent = wallsParent;
                newWall.transform.localPosition = new Vector3(rightEdge, y - 0.5f, 0);

                y -= sprite.bounds.size.y;
                ++i;
            } while (y > bottomEdge);

            yield return new WaitForEndOfFrame();
        }

        [Serializable]
        public class Settings
        {
            public float _startingYPos;
            public int _sandVariations, _variationWidth;
            public GameObject[] _sandPrefabs;
            public GameObject[] _leftWallPrefabs;
            public GameObject[] _rightWallPrefabs;
        }
    }
}

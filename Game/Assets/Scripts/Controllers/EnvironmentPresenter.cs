using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using Ghostpunch.OnlyDown.Messaging;
using Zenject;
using System.Collections.Generic;

namespace Ghostpunch.OnlyDown
{
    public class EnvironmentPresenter : IInitializable
    {
        private EnvironmentView _view = null;
        private EnvironmentView.ViewSettings _settings = null;
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

        public EnvironmentPresenter(EnvironmentView view, EnvironmentView.ViewSettings settings, Camera mainCamera, IMessageSystem messageSystem)
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
            var leftEdge = Mathf.Ceil(-camHorizontalExtent - 0.5f);
            var rightEdge = Mathf.Ceil(camHorizontalExtent - 0.5f);

            var sandVariation = UnityEngine.Random.Range(1, _settings.SandVariations + 1);
            for (int y = 0; y >= bottomEdge; --y)
            {
                var x = leftEdge;
                var i = 1;

                do
                {
                    if (i > _settings.CellsPerVariation)
                        i = 1;
                    var spriteName = String.Format("Sand_{0}_{1}.png", sandVariation, i);
                    var spriteAtlas = _settings.SpriteAtlases.Where(s => s.Contains(spriteName)).FirstOrDefault();

                    if (spriteAtlas == null)
                    {
                        Debug.LogErrorFormat("Could not find sprite: {0}", spriteName);
                        break;
                    }

                    var sandSprite = spriteAtlas.GetSprite(spriteName);
                    var spriteGO = new GameObject(spriteName, typeof(SpriteRenderer));
                    var spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = sandSprite;

                    spriteGO.transform.parent = sandParent;
                    spriteGO.transform.localPosition = new Vector3(x, y, 1f);

                    x += sandSprite.bounds.size.x;
                    ++i;
                } while (x <= rightEdge);

                sandVariation = UnityEngine.Random.Range(1, _settings.SandVariations);
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
            var leftEdge = Mathf.Ceil(-camHorizontalExtent) - 0.5f;
            var rightEdge = Mathf.Ceil(camHorizontalExtent) - 0.5f;

            var y = _view.transform.localPosition.y;
            var i = 0;

            do
            {
                if (i > 7) i = 0;

                // Left Wall
                var leftWall = CreateNewWall("Left", i);
                leftWall.transform.parent = wallsParent;
                leftWall.transform.localPosition = new Vector3(leftEdge, y, 0);

                // Right Wall
                var rightWall = CreateNewWall("Right", i);
                rightWall.transform.parent = wallsParent;
                rightWall.transform.localPosition = new Vector3(rightEdge, y, 0);

                y -= rightWall.bounds.size.y;
                ++i;
            } while (y >= bottomEdge);

            yield return new WaitForEndOfFrame();
        }

        private SpriteRenderer CreateNewWall(string side, int i)
        {
            var spriteName = String.Format("Wall_{0}_{1}.png", side, i);
            var spriteAtlas = _settings.SpriteAtlases.Where(s => s.Contains(spriteName)).FirstOrDefault();

            if (spriteAtlas == null)
            {
                Debug.LogErrorFormat("Could not find sprite: {0}", spriteName);
                return null;
            }

            var sandSprite = spriteAtlas.GetSprite(spriteName);
            var spriteGO = new GameObject(spriteName, typeof(SpriteRenderer));
            var spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sandSprite;

            return spriteRenderer;
        }
    }
}

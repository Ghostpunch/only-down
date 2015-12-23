using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostpunch.OnlyDown.Messaging;
using UnityEngine;

namespace Ghostpunch.OnlyDown
{
    public class EnvironmentViewModel : ObservableObject
    {
        private Transform _transform = null;
        private Camera _mainCamera = null;

        private EnvironmentModel _model = null;
        public EnvironmentModel Model
        {
            get { return _model; }
            set { Set(() => Model, ref _model, value); }
        }

        void Start()
        {
            _transform = transform;
            _mainCamera = Camera.main ?? Camera.allCameras[0];

            StartCoroutine(GenerateLevel());
            StartCoroutine(GenerateWalls());
        }

        void OnEnable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Subscribe<PlayerDigMessage>(OnPlayerDig);
        }

        void OnDisable()
        {
            var messageSystem = MessageSystem.Default;
            messageSystem.Unsubscribe<PlayerDigMessage>(OnPlayerDig);
        }

        private void OnPlayerDig(PlayerDigMessage obj)
        {
            var playerPosition = obj.PlayerPosition;
            var positionToCheckForTile = playerPosition + Vector3.down;
            RaycastHit hit;

            if (Physics.Raycast(positionToCheckForTile, Vector3.forward, out hit, 10f))
            {
                Destroy(hit.transform.gameObject);
            }
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

            var sandVariation = UnityEngine.Random.Range(1, Model.SandVariations + 1);
            for (int y = 0; y >= bottomEdge; --y)
            {
                var x = leftEdge;
                var i = 1;

                do
                {
                    if (i > Model.CellsPerVariation)
                        i = 1;
                    var spriteName = String.Format("Sand_{0}_{1}.png", sandVariation, i);
                    var spriteAtlas = Model.SpriteAtlases.Where(s => s.Contains(spriteName)).FirstOrDefault();

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

                    spriteGO.AddComponent<BoxCollider>();

                    x += sandSprite.bounds.size.x;
                    ++i;
                } while (x <= rightEdge);

                sandVariation = UnityEngine.Random.Range(1, Model.SandVariations);
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

            var y = _transform.localPosition.y;
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
            var spriteAtlas = Model.SpriteAtlases.Where(s => s.Contains(spriteName)).FirstOrDefault();

            if (spriteAtlas == null)
            {
                Debug.LogErrorFormat("Could not find sprite: {0}", spriteName);
                return null;
            }

            var sandSprite = spriteAtlas.GetSprite(spriteName);
            var spriteGO = new GameObject(spriteName, typeof(SpriteRenderer));
            var spriteRenderer = spriteGO.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sandSprite;

            spriteGO.tag = Tags.Walls;
            spriteGO.AddComponent<BoxCollider>();

            return spriteRenderer;
        }
    }
}

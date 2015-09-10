using UnityEngine;
using System.Collections;
using System;

public class Environment : MonoBehaviour
{
    public int _gridCellSize = 1;
    public int _gridWidth = 11;
    public int _gridHeight = 5;

    public Transform _sandParent = null;
    public Material _sandMaterial = null;

    //private Player _player = null;
    private GameObject[,] _levelGrid = null;

    // Use this for initialization
    void Start()
    {
        //_player = FindObjectOfType<Player>();

        BuildLevel();
    }

    //private int[] GetPlayerGridLocation()
    //{
    //    if (_player == null)
    //        throw new MissingReferenceException("Player object not found.");

    //    var playerPosition = _player.CurrentPosition;
    //    var xCoord = playerPosition.x + _gridWidth * 0.5f;
    //    var yCoord = playerPosition.y + _gridHeight * 0.5f;

    //    return new int[] { (int)xCoord, (int)yCoord };
    //}

    private void BuildLevel()
    {
        var startingX = (int)(0 - _gridWidth * 0.5f);
        var startingY = (int)(_gridHeight * 0.5f);

        _levelGrid = new GameObject[_gridWidth, _gridHeight];

        for (int j = 0; j < _gridHeight; ++j)
        {
            for (int i = 0; i < _gridWidth; ++i)
            {
                var sand = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var renderer = sand.GetComponent<MeshRenderer>();

                sand.transform.parent = _sandParent;
                sand.transform.position = new Vector3(startingX + (i * _gridCellSize), startingY - (j * _gridCellSize), 1f);
                renderer.material = _sandMaterial ?? renderer.material;

                _levelGrid[i, j] = sand;
            }
        }

        //var playerPos = GetPlayerGridLocation();
        //for (int x = 0; x < _gridWidth; ++x)
        //{
        //    var position = _levelGrid[x, playerPos[1] + 1].transform.position;
        //    position.z = 0;
        //    _levelGrid[x, playerPos[1] + 1].transform.position = position;
        //}
    }

    public void Dig()
    {
        var playerPos = new int[2];// GetPlayerGridLocation();

        Destroy(_levelGrid[playerPos[0], playerPos[1] + 1]);
        _levelGrid[playerPos[0], playerPos[1] + 1] = null;

        // Move everything one level higher and create a new layer
        for (int y = 0; y < _gridHeight; ++y)
        {
            for (int x = 0; x < _gridWidth; ++x)
            {
                if (y == 0)
                {
                    Destroy(_levelGrid[x, y]);
                    continue;
                }
                else if (y == _gridHeight - 1)
                {
                    var sand = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    var renderer = sand.GetComponent<MeshRenderer>();

                    sand.transform.parent = _sandParent;
                    sand.transform.position = new Vector3(_levelGrid[x, y - 1].transform.position.x, _levelGrid[x, y - 1].transform.position.y - _gridCellSize, 1f);
                    renderer.material = _sandMaterial ?? renderer.material;

                    _levelGrid[x, y] = sand;
                    continue;
                }

                _levelGrid[x, y] = _levelGrid[x, y + 1];

                if (_levelGrid[x, y] != null)
                {
                    var position = _levelGrid[x, y].transform.position;
                    position.y += _gridCellSize;
                    position.z = playerPos[1] + 1 == y ? 0f : 1f;
                    _levelGrid[x, y].transform.position = position;
                }
            }
        }

        //StartCoroutine(AnimateUp());
    }

    private IEnumerator AnimateUp()
    {
        var animating = true;
        var elapsedTime = 0f;
        float remaining = _gridCellSize;

        while (animating)
        {
            for (int y = 0; y < _gridHeight; ++y)
            {
                for (int x = 0; x < _gridWidth; ++x)
                {
                    if (_levelGrid[x, y] != null)
                    {
                        var lerp = Mathf.Lerp(0f, _gridCellSize, elapsedTime);
                        Debug.Log(lerp);
                        remaining -= lerp;
                        _levelGrid[x, y].transform.Translate(0, lerp, 0);
                    }
                }
            }

            yield return new WaitForSeconds(1/ 60f);

            elapsedTime += Time.deltaTime;

            if (remaining < 0f)
                animating = false;
        }
    }
}

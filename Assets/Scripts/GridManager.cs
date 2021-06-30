using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : Singleton<GridManager> {
    private int rows;
    private int cols;
    private int[,] cells;

    private Grid grid;
    public Grid Grid => grid;

    private GameObject rabbitObj;

    protected override void Awake() {
        base.Awake();

        grid = gameObject.GetComponent<Grid>();

        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

        rows = Mathf.RoundToInt(stageDimensions.x);
        cols = Mathf.RoundToInt(stageDimensions.y);

        cells = new int[rows, cols];

        for(int i = 0; i<rows; i++) {
            for(int j = 0; j<cols; j++) {
                cells[i, j] = 0;
            }
        }
    }

    private void Start() {
        

        rabbitObj = Instantiate(GameContext.Instance.rabbitPrefab);
        SpawnRabbit();
    }

    public void AddToGrid(Vector3Int pos) {
        cells[pos.x, pos.y] = 1;
    }

    public void MoveElement(Vector3Int prevPos, Vector3Int newPos) {
        cells[prevPos.x, prevPos.y] = 0;
        cells[newPos.x, newPos.y] = 1;
    }

    public bool IsPositionFree(Vector3Int pos) {
        if(pos.x < 0 || pos.x >= rows || pos.y < 0 || pos.y >= cols ) return false;

        else if(cells[pos.x, pos.y] == 1) return false;

        else if (cells[pos.x, pos.y] == 2) {
            cells[pos.x, pos.y] = 0;

            EventBroker.Instance.CallRabbitEaten();
            SpawnRabbit();

        }

        return true;
    }

    public void SpawnRabbit() {
        List<Vector2Int> freeLocations = new List<Vector2Int>();

        for(int i = 0; i<rows; i++) {
            for(int j = 0; j<cols; j++) {
                if(cells[i, j] == 0) freeLocations.Add(new Vector2Int(i, j));
            }
        }

        int arrayPos = Random.Range(0, freeLocations.Count - 1);
        Vector2Int pos = freeLocations[arrayPos];

        cells[pos.x, pos.y] = 2;
        rabbitObj.transform.position = grid.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
    }
}
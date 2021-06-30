using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment 
{

    protected GameObject obj;
    protected Vector3Int cell;
    protected Vector2Int direction;
    protected Vector2Int nextDirection;

    public Vector2Int Direction => direction;

    public Segment(Sprite sprite, Vector3Int cell, Vector2Int direction) {
        this.cell = cell;
        this.direction = direction;
        this.nextDirection = direction;

        obj = GameObject.Instantiate(GameContext.Instance.bodyPartPrefab); 
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
        obj.transform.position = GridManager.Instance.Grid.GetCellCenterWorld(cell);

        GridManager.Instance.AddToGrid(cell);
        RotateSprite();
        
    }

    protected virtual void RotateSprite()
    {
        float angle = 0f;
        if(direction.y == -1) angle = 180f;
        else if(direction.x != 0) angle = 90 * direction.x;

        obj.transform.rotation = Quaternion.identity;
        obj.transform.Rotate(Vector3.forward, -angle, Space.World);
    }

    public virtual void Move() 
    {
        direction = nextDirection;

        Vector3Int nextCell = new Vector3Int(cell.x + direction.x, cell.y + direction.y, cell.z);

        if(GridManager.Instance.IsPositionFree(nextCell)) {
            obj.transform.position = GridManager.Instance.Grid.GetCellCenterWorld(nextCell);
            GridManager.Instance.MoveElement(cell, nextCell);

            cell = nextCell;
        }
        else {
            EventBroker.Instance.CallGameOver();
        }


        RotateSprite();

    }

    public void SetSprite(Sprite sprite) {
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public Sprite GetSprite() {
        return obj.GetComponent<SpriteRenderer>().sprite;
    }

    public Vector3Int GetCell() {
        return cell;
    }
}

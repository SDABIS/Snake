using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySegment : Segment
{

    private Segment parent;
    public Segment Parent => parent;
    private Sprite nextSprite;

    public BodySegment(Sprite sprite, Vector3Int cell, Vector2Int direction, Segment parent)
            : base(sprite, cell, direction)
    {
        this.parent = parent;
    }

    public override void Move() {
        base.Move();

        if(nextSprite != null) {
            SetSprite(nextSprite);
            nextSprite = null;
            RotateSprite();
        }
        if(Parent.Direction != this.direction) {

            nextDirection = Parent.Direction;
            nextSprite = GetSprite();

            RotateSprite();
        }

    }

    protected override void RotateSprite() {
        if(direction != nextDirection) {

            Sprite spr = GetSprite();

            if(direction == Vector2Int.right  && nextDirection == Vector2Int.up) {
                spr = GameContext.Instance.leftUpSprite;
            }
            else if(direction == Vector2Int.right  && nextDirection == Vector2Int.down) {
                spr = GameContext.Instance.leftDownSprite;
            }
            else if(direction == Vector2Int.left  && nextDirection == Vector2Int.up) {
                spr = GameContext.Instance.rightUpSprite;
            }
            else if(direction == Vector2Int.left  && nextDirection == Vector2Int.down) {
                spr = GameContext.Instance.rightDownSprite;
            }
            else if(direction == Vector2Int.up  && nextDirection == Vector2Int.right) {
                spr = GameContext.Instance.rightDownSprite;
            }
            else if(direction == Vector2Int.up  && nextDirection == Vector2Int.left) {
                spr = GameContext.Instance.leftDownSprite;
            }
            else if(direction == Vector2Int.down  && nextDirection == Vector2Int.right) {
                spr = GameContext.Instance.rightUpSprite;
            }
            else if(direction == Vector2Int.down  && nextDirection == Vector2Int.left) {
                spr = GameContext.Instance.leftUpSprite;
            }

            obj.transform.rotation = Quaternion.identity;
            SetSprite(spr);
        }
        else base.RotateSprite();
    }

    public void SetNextSprite(Sprite sprite) {
        if(nextSprite == null) SetSprite(sprite);
        else nextSprite = sprite;
    }
}
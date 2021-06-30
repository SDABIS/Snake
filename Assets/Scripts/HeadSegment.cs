using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSegment : Segment 
{

    public HeadSegment(Sprite sprite, Vector3Int cell, Vector2Int direction) 
        : base(sprite, cell, direction)
        
    {
        EventBroker.Instance.OnTurnHead.AddListener(HandleTurnHead);
    }

    private void HandleTurnHead(Vector2Int dir) {
        if(this.direction == dir || this.direction == (dir * (-1))) return;

        nextDirection = dir;
    }

}

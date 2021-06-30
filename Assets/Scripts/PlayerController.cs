using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<Segment> segments;

    [Range(0.0f, 1.0f)]
    [SerializeField] float gameSpeed;
    private float timeToNextMove = 0f;
    

    private bool willIncrease = false;
    private Vector3Int lastCell;
    private bool isGameActive = true;
    // Start is called before the first frame update
    void Start()
    {
        Vector3Int initCell = GridManager.Instance.Grid.WorldToCell(transform.position);

        gameSpeed = 1 - gameSpeed;

        GameContext gc = GameContext.Instance;
        segments = new List<Segment>();
        Segment headSeg = new HeadSegment(gc.headSprite, initCell, new Vector2Int(1, 0));
        BodySegment bodySeg = new BodySegment(gc.bodySprite, initCell + Vector3Int.left, new Vector2Int(1, 0), headSeg);
        BodySegment tailSeg = new BodySegment(gc.tailSprite, initCell + Vector3Int.left * 2, new Vector2Int(1, 0), bodySeg);

        segments.Add(headSeg);
        segments.Add(bodySeg);
        segments.Add(tailSeg);

        EventBroker.Instance.OnRabbitEaten.AddListener(HandleRabbitEaten);
        EventBroker.Instance.OnGameOver.AddListener(HandleGameOver);

        StartCoroutine(MoveSnake());
    }

    private void HandleRabbitEaten()
    {
        willIncrease = true;
    }

    private void HandleGameOver()
    {
        isGameActive = false;
        StopAllCoroutines();
    }

    private void Update() {
        if(!isGameActive) return;
        TurnSnake();
    }

    private void TurnSnake() {
        bool hasChangedDirection = false;
        Vector2Int newDirection = new Vector2Int();  

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            hasChangedDirection = true;
            newDirection = new Vector2Int(0, 1);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            hasChangedDirection = true;
            newDirection = new Vector2Int(0, -1);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            hasChangedDirection = true;
            newDirection = new Vector2Int(1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            hasChangedDirection = true;
            newDirection = new Vector2Int(-1, 0);
        }

        if(hasChangedDirection) EventBroker.Instance.CallTurnHead(newDirection);
    }

    private IEnumerator MoveSnake() {

        while(true) {
            yield return new WaitForSeconds(gameSpeed);
            Segment lastSeg = segments[segments.Count - 1];
            lastCell = lastSeg.GetCell();
            foreach(Segment sg in segments) {
                sg.Move();
            }

            if(willIncrease) {
                ((BodySegment)lastSeg).SetNextSprite(GameContext.Instance.bodySprite);
                
                BodySegment newSeg = new BodySegment(GameContext.Instance.tailSprite, lastCell, lastSeg.Direction, lastSeg);
                segments.Add(newSeg);
                willIncrease = false;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBroker : Singleton<EventBroker> {
    [System.Serializable] public class EventTurnHead : UnityEvent<Vector2Int> { }
    [System.Serializable] public class EventRabbitEaten : UnityEvent { }
    [System.Serializable] public class EventGameOver : UnityEvent { }

    public EventTurnHead OnTurnHead;
    public EventRabbitEaten OnRabbitEaten;
    public EventGameOver OnGameOver;

    public void CallTurnHead(Vector2Int direction) {
        OnTurnHead.Invoke(direction);
    }

    public void CallRabbitEaten() {
        OnRabbitEaten.Invoke();
    }

    public void CallGameOver() {
        OnGameOver.Invoke();
    }
}
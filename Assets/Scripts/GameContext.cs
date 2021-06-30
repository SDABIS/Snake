using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : Singleton<GameContext>
{
    //Sprites
    [Header("Sprites")]
    public Sprite headSprite;
    public Sprite bodySprite;
    public Sprite tailSprite;

    public Sprite leftUpSprite;
    public Sprite leftDownSprite;
    public Sprite rightUpSprite;
    public Sprite rightDownSprite;

    //Prefabs
    [Header("Prefabs")]
    public GameObject bodyPartPrefab;
    public GameObject rabbitPrefab;

}

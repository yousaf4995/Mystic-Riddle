using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [Range(2,10)]
    public int rows = 2;
    [Range(2, 10)]
    public int colums = 2;
    public GameObject cardPrefab;
    public List<CardData> cardsInGamePlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

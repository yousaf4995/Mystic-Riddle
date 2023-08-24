using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GamePlayController gamePlayController;
    public GamePlayController GamePlayController => gamePlayController;

    [SerializeField] private ProgressionController progressionController;
    public ProgressionController ProgressionController => progressionController;

    [SerializeField] private GamePlayTimer gamePlayTimer;
    public GamePlayTimer GamePlayTimer => gamePlayTimer;

    [SerializeField] private Toast.ToastText toast;
    public Toast.ToastText Toast => toast;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public GameTimer gameTimer;


    void Awake()
    {
        switch (Game.typeOfDifficulties)
        {
            case Game.TypeOfDifficulties.Easy:
                mapGenerator.SetMapSize(17, 11);
                gameTimer.SetTimer(15);
                break;
            case Game.TypeOfDifficulties.Normal:
                mapGenerator.SetMapSize(35, 21);
                gameTimer.SetTimer(25);
                break;
            case Game.TypeOfDifficulties.Hard:
                mapGenerator.SetMapSize(51, 29);
                gameTimer.SetTimer(35);
                break;
            case Game.TypeOfDifficulties.Speed:
                mapGenerator.SetMapSize(35, 21);
                gameTimer.SetTimer(60);
                break;
        }

        
    }

}

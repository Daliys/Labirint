using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameTimer : MonoBehaviour
{

    public int time;
    public Text timeText;
    public GameUICanvas gameUICanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (time >= 0)
        {
            timeText.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }

        if (Game.typeOfDifficulties == Game.TypeOfDifficulties.Speed) gameUICanvas.ShowStatisticPanel();
        else gameUICanvas.ShowLosePanel();
    }

    public void SetTimer(int time)
    {
        this.time = time;
    }
}

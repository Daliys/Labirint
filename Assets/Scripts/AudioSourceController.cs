using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = Game.isEnableSound ? 80 : 0;
    }

}

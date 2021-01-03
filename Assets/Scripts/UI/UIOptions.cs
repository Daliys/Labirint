using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptions : MonoBehaviour
{
    public Animator animator;
    public GameObject fullScreenButton;
    public GameObject soundButton;
    public Animator animatorSound;
    public Animator animatorFullScreen;

    private void Start()
    {
        animatorFullScreen.SetBool("IsFullScreen", Game.isEnableFullScreen);
        animatorSound.SetBool("IsSound", Game.isEnableSound);
    }

    public void OnClickFullScreenModeButton()
    {
        Game.SaveFullScreenValue(!Game.isEnableFullScreen);

        if (!Game.isEnableFullScreen) Screen.fullScreen = false;
        else Screen.fullScreen = true;

        animatorFullScreen.SetBool("IsFullScreen", Game.isEnableFullScreen);
    }

    public void OnClickSoundButton()
    {
        Game.SaveSoundValue(!Game.isEnableSound);

        if (!Game.isEnableSound) AudioListener.volume = 0;
        else AudioListener.volume = 100;
        animatorSound.SetBool("IsSound", Game.isEnableSound);
        
    }

    public void OnClickResetDataButton()
    {
        PlayerPrefs.DeleteAll();
       
        Game.bestScore = 0;
        Game.isEnableFullScreen = true;
        Game.isEnableSound = true;
        Screen.fullScreen = true;

        animatorSound.SetBool("IsSound", Game.isEnableSound);
        animatorFullScreen.SetBool("IsFullScreen", Game.isEnableFullScreen);
       
    }

    public void OnClickBackButton()
    {
        animator.SetBool("IsOptionPanel", false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator animator;
  
   
    public void OnClickPlayButton()
    {
        animator.SetBool("IsDifficultiesPanel", true);
    }

    public void OnClickOptionsButton()
    {
        animator.SetBool("IsOptionPanel", true);
    }

    public void OnCkickExitButton()
    {
        Application.Quit();
    }

    public void OnCkickAboutButton()
    {
        animator.SetBool("IsDevelopersPanel", true);
    }

    private void Start()
    {
        Time.timeScale = 1;
    }
}

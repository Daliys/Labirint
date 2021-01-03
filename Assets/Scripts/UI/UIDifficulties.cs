using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIDifficulties : MonoBehaviour
{
    public Animator animator;

    public void OnClickDifficultButton(int typeOfDifficulties)
    {
        Game.typeOfDifficulties = (Game.TypeOfDifficulties)typeOfDifficulties;
        SceneManager.LoadScene(1);
    }

    public void OnClickBackButton()
    {
        animator.SetBool("IsDifficultiesPanel", false);
    }

}

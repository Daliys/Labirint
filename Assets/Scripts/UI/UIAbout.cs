using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAbout : MonoBehaviour
{
    public Animator animator;
    public void OnClickBackButton()
    {
        animator.SetBool("IsDevelopersPanel", false);
    }
}

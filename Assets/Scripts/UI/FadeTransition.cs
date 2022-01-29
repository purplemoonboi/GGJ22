using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    private int level;
    public Animator animator;

    void Update()
    {

    }

    public void fadeToLevel(int level)
    {
        this.level = level;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(level);
    }
}

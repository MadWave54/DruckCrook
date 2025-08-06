using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{

    [SerializeField] private GameObject Canvas;

    private Animator animator;

    private void Awake()
    {

        DontDestroyOnLoad(Canvas);

        animator = GetComponent<Animator>();

        animator.SetBool("isOpen", !animator.GetBool("isOpen"));

    }

    public void NextScene(string NameScene)
    {

        StartCoroutine(SwapAnimations(NameScene));

    }

    private IEnumerator SwapAnimations(string NameScene)
    {

        animator.SetBool("isOpen", !animator.GetBool("isOpen"));

        yield return new WaitForEndOfFrame();
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || animator.IsInTransition(0));

        SceneManager.LoadScene(NameScene);

        animator.SetBool("isOpen", !animator.GetBool("isOpen"));

    }

}

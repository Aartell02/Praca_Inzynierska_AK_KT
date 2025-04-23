using System.Collections;
using UnityEngine;

public class ObjectDeathStatus : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        Animator animator = GetComponent<Animator>();
        Debug.Log("died");
        if (animator != null)
        {
            animator.SetBool("IsDead",true);
            StartCoroutine(StopAnimationAtEnd(animator));
        }
    }
    private IEnumerator StopAnimationAtEnd(Animator animator)
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length);

        animator.speed = 0f;
    }

}

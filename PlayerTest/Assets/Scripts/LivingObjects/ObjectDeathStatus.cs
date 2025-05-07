using System.Collections;
using UnityEngine;

public class ObjectDeathStatus : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log("died");

    }

    private void StopAnimation(string stateName)
    {
        animator.speed = 0f;
    }

}

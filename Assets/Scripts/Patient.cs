using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Patient : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetAnimatorState(string state)
    {
        animator.Play(state);
    }
}

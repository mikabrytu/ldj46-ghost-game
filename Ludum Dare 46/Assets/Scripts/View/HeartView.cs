using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mikabrytu.LD46.View;

public class HeartView : MonoBehaviour, IHeart
{
    [SerializeField] private Animator animator;

    public void IncreaseBPM()
    {
        animator.SetTrigger("upbeat");
    }

    public void DecreaseBPM()
    {
        animator.SetTrigger("downbeat");
    }
}

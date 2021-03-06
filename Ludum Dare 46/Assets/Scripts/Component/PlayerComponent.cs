﻿using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;
using System.Collections;
using Mikabrytu.LD46;

public class PlayerComponent : MonoBehaviour, IPlayer
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private Transform initialPosition;
    [SerializeField] private float speed;
    [SerializeField] private float heartAttack = 200;
    [SerializeField] private float fearPulse = 25;
    [SerializeField] private float fearTimer = 3f;
    [SerializeField] private float rate = 2;

    private IMove moveSystem;
    private IHealth healthSystem;
    private IEnumerator fearRoutine;
    private bool canMove;

    private void Start()
    {
        moveSystem = new InputMovementSystem();
        healthSystem = new HealthSystem();

        moveSystem.Setup(speed, 0);
        healthSystem.Setup(heartAttack, fearPulse, rate);
    }

    private void Update()
    {
        healthSystem.Update();

        if (canMove)
        {
            bool isMoving = moveSystem.Move(transform, model);
            animator.SetBool("walking", isMoving);
            AudioManager.Instance.PlayFootstep(isMoving);
        }

        if (healthSystem.isDead())
            EventManager.Raise(new PlayerIsDeadEvent());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ghost")
        {
            animator.SetBool("blinking", true);
            AudioManager.Instance.PlayWhisper();

            if (fearRoutine != null)
                return;

            fearRoutine = IncreaseFear();
            StartCoroutine(fearRoutine);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Ghost")
        {
            animator.SetBool("blinking", false);

            if (fearRoutine == null)
                return;

            if (fearRoutine != null)
                StopCoroutine(fearRoutine);
            fearRoutine = null;

            healthSystem.DecreaseBPM();
            EventManager.Raise(new PlayerChangeBPMEvent(false));
        }
    }

    public void SetInitialPosition()
    {
        transform.position = initialPosition.position;
        canMove = true;
        healthSystem?.Reset();
    }

    public int GetHeartBPM()
    {
        return healthSystem.GetBPM();
    }

    public void StopMovement()
    {
        canMove = false;
        animator.SetBool("walking", false);
    }

    public void SetFixing(bool isFixing)
    {
        animator.SetBool("fixing", isFixing);
    }

    public IEnumerator IncreaseFear()
    {
        while (true)
        {
            healthSystem.IncreaseBPM();
            EventManager.Raise(new PlayerChangeBPMEvent(true));
            yield return new WaitForSeconds(fearTimer);
        }
    }
}

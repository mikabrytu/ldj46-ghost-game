using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;
using System.Collections;

public class PlayerComponent : MonoBehaviour, IPlayer
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] private Transform model;
    [SerializeField] private Transform initialPosition;
    [SerializeField] private float speed;
    [SerializeField] private float heartAttack = 200;
    [SerializeField] private float fearPulse = 25;
    [SerializeField] private float fearTimer = 3f;
    [SerializeField] private float rate = 2;
    [SerializeField] private float blinkRate = .5f;

    private IMove moveSystem;
    private IHealth healthSystem;
    private IEnumerator fearRoutine;
    private float blinkTimer;
    private bool canMove;
    private bool blink = false;

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
            moveSystem.Move(transform, model);

        if (healthSystem.isDead())
            EventManager.Raise(new PlayerIsDeadEvent());

        if (blink)
        {
            blinkTimer -= Time.deltaTime;

            if (blinkTimer <= 0.0f)
            {
                flashlight.SetActive(!flashlight.active);
            }
        }
        else
        {
            blinkTimer = blinkRate;
            flashlight.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ghost")
        {
            blink = true;

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
            blink = false;

            if (fearRoutine == null)
                return;

            if (fearRoutine != null)
                StopCoroutine(fearRoutine);
            fearRoutine = null;

            healthSystem.DecreaseBPM();
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
    }

    public IEnumerator IncreaseFear()
    {
        while (true)
        {
            healthSystem.IncreaseBPM();
            yield return new WaitForSeconds(fearTimer);
        }
    }
}

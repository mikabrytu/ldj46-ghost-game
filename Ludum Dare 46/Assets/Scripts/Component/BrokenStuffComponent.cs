using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;
using System.Collections;
using System;

public class BrokenStuffComponent : MonoBehaviour, IBrokenStuff
{
    [SerializeField] private float fixTime;

    private IFix fixSystem;

    private void Start()
    {
        EventManager.AddListener<BrokenStuffTriggerEvent>(OnBrokenStuffTriggerEnter);

        fixSystem = new FixSystem();
        fixSystem.Setup(fixTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            fixSystem.StartFix(OnFixFinish);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            fixSystem.CancelFix();
        }
    }

    private void OnFixFinish()
    {
        Debug.Log("Fix Finished");
        EventManager.Raise(new PlayerFixedStuffEvent());
    }

    private void OnBrokenStuffTriggerEnter(BrokenStuffTriggerEvent e)
    {
        if (e.isEnter)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            EventManager.Raise(new PlayerReachBrokenStuffEvent(screenPosition));
        }
        else
            EventManager.Raise(new PlayerLeavingBrokenStuffEvent());
    }
}

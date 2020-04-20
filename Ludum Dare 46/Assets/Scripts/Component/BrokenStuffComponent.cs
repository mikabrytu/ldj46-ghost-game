using UnityEngine;
using Mikabrytu.LD46;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;
using System.Collections;
using System;

public class BrokenStuffComponent : MonoBehaviour, IBrokenStuff
{
    [SerializeField] private BrokenStuffTypes type;
    [SerializeField] private ParticleSystem fixEffectPrefab;
    [SerializeField] private float fixTime;

    private IFix fixSystem;
    private ParticleSystem fixEffect;

    private void Start()
    {
        EventManager.AddListener<BrokenStuffTriggerEvent>(OnBrokenStuffTriggerEnter);

        fixSystem = new FixSystem();
        fixSystem.Setup(fixTime);

        fixEffect = Instantiate(fixEffectPrefab, transform.position, Quaternion.identity);
        fixEffect.Stop();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            fixEffect.Play();
            fixSystem.StartFix(OnFixFinish);
            AudioManager.Instance.PlayFix();
            EventManager.Raise(new PlayerFixingEvent(true));
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            fixEffect.Stop();
            fixSystem.CancelFix();
            EventManager.Raise(new PlayerFixingEvent(false));
        }
    }

    private void OnFixFinish()
    {
        fixEffect.Stop();
        Debug.Log("Fix Finished");
        EventManager.Raise(new PlayerFixedStuffEvent(type));
        Enable(false);
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

    public void Enable(bool enable)
    {
        gameObject.SetActive(enable);
    }
}

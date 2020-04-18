using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;
using System.Collections;

public class BrokenStuffComponent : MonoBehaviour, IBrokenStuff
{
    [SerializeField] private Light pointLight;
    [SerializeField] private float fixTime;

    private IBright brightSystem;
    private IFix fixSystem;
    private ISpawn spawnSystem;

    private void Start()
    {
        brightSystem = new BrightSystem();
        fixSystem = new FixSystem();
        spawnSystem = new SpawnSystem();

        brightSystem.TurnOff(pointLight);
        fixSystem.Setup(fixTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            brightSystem.TurnOn(pointLight);
            fixSystem.StartFix(OnFixFinish);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            brightSystem.TurnOff(pointLight);
            fixSystem.CancelFix();
        }
    }

    private void OnFixFinish()
    {
        Debug.Log("Fix Finished");
        EventManager.Raise(new PlayerFixedStuffEvent());
        Destroy(gameObject);
    }
}

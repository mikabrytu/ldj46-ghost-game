using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;

public class BrokenStuffComponent : MonoBehaviour, IBrokenStuff
{
    [SerializeField] private Light pointLight;

    private IBright brightSystem;
    private IFix fixSystem;
    private ISpawn spawnSystem;

    private void Start()
    {
        brightSystem = new BrightSystem();
        fixSystem = new FixSystem();
        spawnSystem = new SpawnSystem();

        brightSystem.TurnOff(pointLight);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            brightSystem.TurnOn(pointLight);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
            brightSystem.TurnOff(pointLight);
    }
}

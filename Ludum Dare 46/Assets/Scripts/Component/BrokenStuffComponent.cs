using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;

public class BrokenStuffComponent : MonoBehaviour, IBrokenStuff
{
    private IBright brightSystem;
    private IFix fixSystem;
    private ISpawn spawnSystem;

    private void Start()
    {
        brightSystem = new BrightSystem();
        fixSystem = new FixSystem();
        spawnSystem = new SpawnSystem();
    }
}

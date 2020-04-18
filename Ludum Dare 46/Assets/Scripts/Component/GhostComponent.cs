using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;

public class GhostComponent : MonoBehaviour, IGhost
{
    private IMove moveSystem;
    private IInvisibility invisibilitySystem;
    private ISpawn spawnSystem;

    private void Start()
    {
        moveSystem = new AIMovementSystem();
        invisibilitySystem = new InvisibilitySystem();
        spawnSystem = new SpawnSystem();
    }
}

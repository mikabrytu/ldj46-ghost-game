using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;
using System.Collections.Generic;

public class GhostComponent : MonoBehaviour, IGhost
{
    [SerializeField] private float speed;
    [SerializeField] private float raycastLimit = .5f;

    private IMove moveSystem;
    private IInvisibility invisibilitySystem;
    private ISpawn spawnSystem;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        moveSystem = new AIMovementSystem();
        invisibilitySystem = new InvisibilitySystem();
        spawnSystem = new SpawnSystem();

        moveSystem.Setup(speed, raycastLimit);
        moveSystem.Setup(Vector3.forward);

        invisibilitySystem.Hide(renderer);
    }

    private void Update()
    {
        moveSystem.Move(transform);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            moveSystem.TriggerMovement(transform);
            return;
        }

        if (collider.gameObject.tag == "Player")
        {
            invisibilitySystem.Show(renderer);
            EventManager.Raise(new PlayerIsDeadEvent());
            return;
        }
    }
}

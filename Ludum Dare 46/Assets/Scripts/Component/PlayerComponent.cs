using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;

public class PlayerComponent : MonoBehaviour, IPlayer
{
    [SerializeField] private float speed;

    private IMove moveSystem;
    private IHealth healthSystem;

    private void Start()
    {
        moveSystem = new InputMovementSystem();
        healthSystem = new HealthSystem();

        moveSystem.Setup(speed, 0);
    }

    private void Update()
    {
        moveSystem.Move(transform);
    }
}

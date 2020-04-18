using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;

public class PlayerComponent : MonoBehaviour, IPlayer
{
    [SerializeField] private float speed;
    [SerializeField] private int heartAttack = 200;
    [SerializeField] private int fearPulse = 25;

    private IMove moveSystem;
    private IHealth healthSystem;

    private void Start()
    {
        moveSystem = new InputMovementSystem();
        healthSystem = new HealthSystem();

        moveSystem.Setup(speed, 0);

        healthSystem.Setup(heartAttack, fearPulse);
    }

    private void Update()
    {
        moveSystem.Move(transform);

        Debug.Log($"Player heart BPM: {GetHeartBPM()}");

        if (healthSystem.isDead())
            EventManager.Raise(new PlayerIsDeadEvent());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Ghost")
            healthSystem.IncreaseBPM();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Ghost")
            healthSystem.DecreaseBPM();
    }

    public int GetHeartBPM()
    {
        return healthSystem.GetBPM();
    }
}

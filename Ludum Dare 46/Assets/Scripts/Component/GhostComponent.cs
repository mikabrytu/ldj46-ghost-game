using UnityEngine;
using Mikabrytu.LD46.Components;
using Mikabrytu.LD46.Systems;
using Mikabrytu.LD46.Events;
using System.Collections.Generic;
using UnityEngine.AI;
using Mikabrytu.LD46;

public class GhostComponent : MonoBehaviour, IGhost
{
    [SerializeField] private Transform model;
    [SerializeField] private List<Transform> targets;
    [SerializeField] private float speed;
    [SerializeField] private float raycastLimit = .5f;

    private IMove moveSystem;
    private IInvisibility invisibilitySystem;
    private ISpawn spawnSystem;

    private NavMeshAgent navMesh;
    private bool canMove;

    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        moveSystem = new AIMovementSystem();
        invisibilitySystem = new InvisibilitySystem();
        spawnSystem = new SpawnSystem();

        moveSystem.Setup(speed, raycastLimit);
        moveSystem.TriggerMovement(transform);

        invisibilitySystem.Show(false, model.gameObject);
    }

    private void Update()
    {
        if (canMove && navMesh.remainingDistance == 0)
        {
            SetDestination();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            invisibilitySystem.Show(true, model.gameObject);
            AudioManager.Instance.PlayCaught();
            EventManager.Raise(new PlayerIsDeadEvent());
            return;
        }
    }

    private void OnEnable()
    {
        invisibilitySystem?.Show(false, model.gameObject);
    }

    public void Enable(bool enable)
    {
        gameObject.SetActive(enable);
        canMove = enable;

        if (enable)
            SetDestination();
    }

    public void StopMovement()
    {
        canMove = false;
    }

    private void SetDestination()
    {
        Debug.Log("Setting destination");
        navMesh.SetDestination(targets[Random.Range(0, targets.Count)].position);
    }
}

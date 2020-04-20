using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public interface IMove
    {
        void Setup(float speed, float raycastLimit);
        void Setup(Vector3 direction);
        bool Move(Transform origin, Transform model);
        void TriggerMovement(Transform transform);
    }
}
using System.Collections.Generic;
using Mikabrytu.LD46.Systems;
using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public class AIMovementSystem : IMove
    {
        private Vector3 moveDirection;
        private float speed;
        private float raycastLimit;

        public void Setup(float speed, float raycastLimit)
        {
            this.speed = speed;
            this.raycastLimit = raycastLimit;
        }

        public void Setup(Vector3 direction)
        {
            moveDirection = direction;
        }

        public void Move(Transform origin, Transform model)
        {
            origin.Translate(moveDirection * speed * Time.deltaTime);
        }

        public void TriggerMovement(Transform transform)
        {
            List<Vector3> viablePaths = new List<Vector3>();
            if (!IsCloseToWallAt(transform, Vector3.forward)) viablePaths.Add(Vector3.forward);
            if (!IsCloseToWallAt(transform, Vector3.back)) viablePaths.Add(Vector3.back);
            if (!IsCloseToWallAt(transform, Vector3.left)) viablePaths.Add(Vector3.left);
            if (!IsCloseToWallAt(transform, Vector3.right)) viablePaths.Add(Vector3.right);

            moveDirection = viablePaths[Random.Range(0, viablePaths.Count)];
        }

        private bool IsCloseToWallAt(Transform transform, Vector3 direction)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, direction * raycastLimit, Color.green);
            if (Physics.Raycast(transform.position, direction, out hit, raycastLimit))
                return hit.collider.gameObject.tag == "Wall";
            else
                return false;
        }
    }
}

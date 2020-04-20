using Mikabrytu.LD46.Systems;
using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public class InputMovementSystem : IMove
    {
        private float speed = 0;

        public void Setup(float speed, float raycastLimit)
        {
            this.speed = speed;
        }

        public bool Move(Transform origin, Transform model)
        {
            bool walking = false;

            if (Input.GetKey(KeyCode.D))
            {
                origin.Translate(Vector3.left * speed * Time.deltaTime);
                model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(Vector3.left), speed * Time.deltaTime);
                walking = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                origin.Translate(Vector3.right * speed * Time.deltaTime);
                model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(Vector3.right), speed * Time.deltaTime);
                walking = true;
            }

            if (Input.GetKey(KeyCode.W))
            {
                origin.Translate(Vector3.back * speed * Time.deltaTime);
                model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(Vector3.back), speed * Time.deltaTime);
                walking = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                origin.Translate(Vector3.forward * speed * Time.deltaTime);
                model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(Vector3.forward), speed * Time.deltaTime);
                walking = true;
            }

            return walking;
        }

        public void Setup(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }

        public void TriggerMovement(Transform transform)
        {
            throw new System.NotImplementedException();
        }
    }
}

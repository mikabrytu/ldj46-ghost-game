using Mikabrytu.LD46.Systems;
using UnityEngine;

public class InputMovementSystem : IMove
{
    private float speed = 0;

    public void Setup(float speed)
    {
        this.speed = speed;
    }

    public void Move(Transform transform)
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}

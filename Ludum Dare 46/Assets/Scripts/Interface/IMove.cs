using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public interface IMove
    {
        void Setup(float speed);
        void Move(Transform transform);
    }
}
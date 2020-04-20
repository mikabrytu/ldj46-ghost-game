using UnityEngine;

namespace Mikabrytu.LD46.Events
{
    public class PlayerReachBrokenStuffEvent : BaseEvent
    {
        public Vector3 position;

        public PlayerReachBrokenStuffEvent(Vector3 position)
        {
            this.position = position;
        }
    }
}
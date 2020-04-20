namespace Mikabrytu.LD46.Events
{
    public class PlayerFixingEvent : BaseEvent
    {
        public bool isFixing;

        public PlayerFixingEvent(bool isFixing)
        {
            this.isFixing = isFixing;
        }
    }
}
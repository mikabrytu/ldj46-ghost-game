namespace Mikabrytu.LD46.Events
{
    public class PlayerChangeBPMEvent : BaseEvent
    {
        public bool isIncreasing;

        public PlayerChangeBPMEvent(bool isIncreasing)
        {
            this.isIncreasing = isIncreasing;
        }
    }
}
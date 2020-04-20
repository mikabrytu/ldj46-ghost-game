namespace Mikabrytu.LD46.Events
{
    public class BrokenStuffTriggerEvent : BaseEvent
    {
        public bool isEnter;

        public BrokenStuffTriggerEvent(bool isEnter)
        {
            this.isEnter = isEnter;
        }
    }
}
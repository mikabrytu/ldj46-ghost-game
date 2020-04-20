namespace Mikabrytu.LD46.Events
{
    public class PlayerFixedStuffEvent : BaseEvent {

        public BrokenStuffTypes type;

        public PlayerFixedStuffEvent(BrokenStuffTypes type)
        {
            this.type = type;
        }
    }
}
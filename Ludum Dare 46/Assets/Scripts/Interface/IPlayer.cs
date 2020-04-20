namespace Mikabrytu.LD46.Components
{
    public interface IPlayer
    {
        void SetInitialPosition();
        void StopMovement();
        void SetFixing(bool isFixing);
        int GetHeartBPM();
    }
}
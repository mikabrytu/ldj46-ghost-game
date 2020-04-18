namespace Mikabrytu.LD46.Systems
{
    public interface IHealth
    {
        void Setup(int heartAttackLimit, int pulse);
        void IncreaseBPM();
        void DecreaseBPM();
        int GetBPM();
        bool isDead();
    }
}
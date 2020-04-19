namespace Mikabrytu.LD46.Systems
{
    public interface IHealth
    {
        void Setup(int heartAttackLimit, int pulse);
        void Reset();
        void IncreaseBPM();
        void DecreaseBPM();
        int GetBPM();
        bool isDead();
    }
}
namespace Mikabrytu.LD46.Systems
{
    public interface IHealth
    {
        void Setup(float heartAttackLimit, float pulse, float rate);
        void Reset();
        void Update();
        void IncreaseBPM();
        void DecreaseBPM();
        int GetBPM();
        bool isDead();
    }
}
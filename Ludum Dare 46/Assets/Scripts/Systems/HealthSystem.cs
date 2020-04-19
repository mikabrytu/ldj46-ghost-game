using Mikabrytu.LD46.Systems;

namespace Mikabrytu.LD46.Systems
{
    public class HealthSystem : IHealth
    {
        private int currentBPM;
        private int heartAttackLimit;
        private int pulse;

        public void Setup(int heartAttackLimit, int pulse)
        {
            this.heartAttackLimit = heartAttackLimit;
            this.pulse = pulse;
            currentBPM = heartAttackLimit / 2;
        }

        public void Reset()
        {
            currentBPM = heartAttackLimit / 2;
        }

        public void IncreaseBPM()
        {
            currentBPM += pulse;
        }

        public void DecreaseBPM()
        {
            currentBPM -= pulse;
        }

        public int GetBPM()
        {
            return currentBPM;
        }

        public bool isDead()
        {
            return currentBPM >= heartAttackLimit;
        }
    }
}

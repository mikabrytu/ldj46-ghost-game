using Mikabrytu.LD46.Systems;
using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public class HealthSystem : IHealth
    {
        private float heartAttackLimit;
        private float currentBPM;
        private float nextBPMLevel;
        private float pulse;
        private float rate;

        public void Setup(float heartAttackLimit, float pulse, float rate)
        {
            this.heartAttackLimit = heartAttackLimit;
            this.pulse = pulse;
            this.rate = rate;
            currentBPM = heartAttackLimit / 2;
        }

        public void Reset()
        {
            currentBPM = heartAttackLimit / 2;
            nextBPMLevel = currentBPM;
        }

        public void Update()
        {
            if (currentBPM < nextBPMLevel)
                currentBPM += rate * Time.deltaTime;

            if (currentBPM > nextBPMLevel)
                currentBPM -= rate * Time.deltaTime;
        }

        public void IncreaseBPM()
        {
            nextBPMLevel = currentBPM + pulse;
        }

        public void DecreaseBPM()
        {
            nextBPMLevel = currentBPM - pulse;
        }

        public int GetBPM()
        {
            return (int) currentBPM;
        }

        public bool isDead()
        {
            return currentBPM >= heartAttackLimit;
        }
    }
}

using System;

namespace Mikabrytu.LD46.Systems
{
    public interface IFix
    {
        void Setup(float timeToFix);
        void StartFix(Action OnFinish);
        void CancelFix();
    }
}
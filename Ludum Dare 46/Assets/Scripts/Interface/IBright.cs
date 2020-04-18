using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public interface IBright
    {
        void TurnOn(Light light);
        void TurnOff(Light light);
    }
}
using Mikabrytu.LD46.Systems;
using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public class BrightSystem : IBright
    {
        public void TurnOn(Light light)
        {
            light.enabled = true;
        }

        public void TurnOff(Light light)
        {
            light.enabled = false;
        }
    }
}

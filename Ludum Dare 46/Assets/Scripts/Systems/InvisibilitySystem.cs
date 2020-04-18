using Mikabrytu.LD46.Systems;
using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public class InvisibilitySystem : IInvisibility
    {
        public void Show(Renderer renderer)
        {
            renderer.enabled = true;
        }

        public void Hide(Renderer renderer)
        {
            renderer.enabled = false;
        }

        public bool isVisible(Renderer renderer)
        {
            return renderer.enabled;
        }
    }
}

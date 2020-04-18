using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public interface IInvisibility
    {
        void Show(Renderer renderer);
        void Hide(Renderer renderer);
        bool isVisible(Renderer renderer);
    }
}
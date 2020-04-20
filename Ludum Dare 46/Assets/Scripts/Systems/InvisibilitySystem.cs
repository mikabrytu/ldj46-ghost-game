using Mikabrytu.LD46.Systems;
using UnityEngine;

namespace Mikabrytu.LD46.Systems
{
    public class InvisibilitySystem : IInvisibility
    {
        public void Show(bool show, GameObject gameObject)
        {
            gameObject.SetActive(show);
        }
    }
}

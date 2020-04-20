using Mikabrytu.LD46.Events;
using UnityEngine;

public class WarningComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            EventManager.Raise(new BrokenStuffTriggerEvent(true));
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
            EventManager.Raise(new BrokenStuffTriggerEvent(false));
    }
}

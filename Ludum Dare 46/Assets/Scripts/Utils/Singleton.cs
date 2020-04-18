using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T m_instance;

    public static T Instance { get { return m_instance; } }

    [SerializeField]
    protected bool _persistent = false;

    protected virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = gameObject.GetComponent<T>();
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (_persistent)
            DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (m_instance == this)
            m_instance = null;
    }
}
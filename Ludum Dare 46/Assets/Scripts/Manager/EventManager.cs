using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mikabrytu.LD46.Events
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager m_instance;

        public delegate void EventDelegate<T>(T e) where T : BaseEvent;
        private delegate void EventDelegate(BaseEvent e);

        private static Dictionary<System.Type, EventDelegate> _delegates;
        private static Dictionary<System.Delegate, EventDelegate> _delegateLookup;

        private static void Init()
        {
            if (m_instance != null)
                return;

            m_instance = FindObjectOfType<EventManager>();

            if (m_instance == null)
            {
                m_instance = new GameObject("Events Manager").AddComponent<EventManager>();
            }

            _delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
            _delegates = new Dictionary<System.Type, EventDelegate>();
            DontDestroyOnLoad(m_instance.gameObject);
        }

        /// <summary>
        /// Register a listener to an event
        /// </summary>
        /// <typeparam name="T">Event Type (Must Inherit from BaseEvent)</typeparam>
        /// <param name="del">Method to me registered</param>
        public static void AddListener<T>(EventDelegate<T> del) where T : BaseEvent
        {
            Init();

            if (_delegateLookup.ContainsKey(del))
                return;

            EventDelegate internalDelegate = (e) => del((T)e);
            _delegateLookup[del] = internalDelegate;

            EventDelegate tempDel;
            if (_delegates.TryGetValue(typeof(T), out tempDel))
            {
                _delegates[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                _delegates[typeof(T)] = internalDelegate;
            }
        }

        /// <summary>
        /// Remove a listener from an event
        /// </summary>
        /// <typeparam name="T">Event Type (Must Inherit from BaseEvent)</typeparam>
        /// <param name="del">Method to be removed</param>
        public static void RemoveListener<T>(EventDelegate<T> del) where T : BaseEvent
        {
            Init();

            EventDelegate internalDelegate;
            if (_delegateLookup.TryGetValue(del, out internalDelegate))
            {
                EventDelegate tempDel;
                if (_delegates.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        _delegates.Remove(typeof(T));
                    }
                    else
                    {
                        _delegates[typeof(T)] = tempDel;
                    }
                }

                _delegateLookup.Remove(del);
            }
        }

        /// <summary>
        /// Call a event
        /// </summary>
        /// <param name="e">Event Type</param>
        public static void Raise(BaseEvent e)
        {
            Init();

            EventDelegate del;

            _delegates.TryGetValue(e.GetType(), out del);
            del?.Invoke(e);
        }
    }
}
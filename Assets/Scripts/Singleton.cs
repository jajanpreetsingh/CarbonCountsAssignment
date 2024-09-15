using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            if (Instance.gameObject != gameObject)
            {
                Destroy(gameObject);
            }

            return;
        }

        Instance = GetComponent<T>();

        if (Instance == null)
        {
            throw new Exception("Object of type " + typeof(T).ToString() + " was not found");
        }
    }
}

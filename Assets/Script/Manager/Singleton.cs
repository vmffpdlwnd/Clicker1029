using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    private static bool isInitialized = false;
    public static void Initialize()
    {
        if (!isInitialized)
        {
            if (instance is MonoBehaviour monoBehaviourInstance)
            {
                monoBehaviourInstance.gameObject.name = typeof(T).Name;
                DontDestroyOnLoad(monoBehaviourInstance.gameObject);
            }
            isInitialized = true;
        }
    }


}

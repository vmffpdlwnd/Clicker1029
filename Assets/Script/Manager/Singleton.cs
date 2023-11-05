using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get // 인스턴스를 참조 하려고 할때 인스턴스가 없으면, 신규 생성하여 리턴.
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>(); // 씬내에 동일한 타입의 오브젝트가 있는지 찾음.
                if (instance == null) // 찾기 실패
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T)); // T 타입의 오브젝트를 신규 생성.
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (transform.parent != null & transform.root != null) // 해당 오브젝트가 부모를 가지고 있는 오브젝트라면
        {
            DontDestroyOnLoad(this.transform.root.gameObject); // 부모까지 DontDestroy 오브젝트로 설정.
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // 자기 자신만 DontDestroy 오브젝트로 설정.
        }
    }
}

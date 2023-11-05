using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get // �ν��Ͻ��� ���� �Ϸ��� �Ҷ� �ν��Ͻ��� ������, �ű� �����Ͽ� ����.
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>(); // ������ ������ Ÿ���� ������Ʈ�� �ִ��� ã��.
                if (instance == null) // ã�� ����
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T)); // T Ÿ���� ������Ʈ�� �ű� ����.
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (transform.parent != null & transform.root != null) // �ش� ������Ʈ�� �θ� ������ �ִ� ������Ʈ���
        {
            DontDestroyOnLoad(this.transform.root.gameObject); // �θ���� DontDestroy ������Ʈ�� ����.
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // �ڱ� �ڽŸ� DontDestroy ������Ʈ�� ����.
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager; // GameManager ������ ���� ����

    private void Start()
    {
        gameManager = GameManager.Instance; // GameManager�� �ν��Ͻ��� ������

        // 10�� �Ŀ� DestroyObject �Լ��� ȣ���մϴ�
        Invoke("DestroyObject", 10f);
 
    }

    private void OnMouseDown()
    {
        // ������ Ŭ���ϸ� �ı��մϴ�
        DestroyObject();
    }

    private void DestroyObject()
    {
        // GameManager�� ������ ����
        gameManager.AddGold(1);
        gameManager.AddExp(5);

        // ������ �ı��մϴ�
        Destroy(gameObject);
    }
}
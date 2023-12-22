using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager; // GameManager ������ ���� ����

    private Vector3 targetPosition; // �̵��� ��ǥ ��ǥ
    private float moveSpeed = 10f; // ������ �̵� �ӵ�

    private bool isMoving = false; // ������ �̵� ���θ� ��Ÿ���� ����

    private Rigidbody rb;

    private void Start()
    {
        gameManager = GameManager.Instance; // GameManager�� �ν��Ͻ��� ������

        targetPosition = new Vector3(0.4f, 2.8f, -6.5f); // �̵��� ��ǥ ��ǥ ����

        rb = GetComponent<Rigidbody>(); // Get the reference to the Rigidbody component once

        Invoke("SetIsMoving", 10f); // 10�� �Ŀ� SetIsMoving �Լ��� ȣ���մϴ�.
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if ((transform.position - targetPosition).sqrMagnitude < 0.01f * 0.01f)
            {
                Destroy(gameObject); // ��ǥ ��ġ�� �����ϸ� �ı��մϴ�.
            }
        }
    }

    private void OnMouseDown()
    {
        isMoving = true; // Ŭ���Ǿ��� �� �̵� ����
        rb.useGravity = true; // Rigidbody�� �߷� ����� Ȱ��ȭ�մϴ�.
    }

    public void OnDestroy()
    {
        if (isMoving)
        {
            gameManager.AddGold(1);
            Debug.Log("��尡 �߰��Ǿ����ϴ�.");
            gameManager.AddExp(5);
            Debug.Log("����ġ�� �߰��Ǿ����ϴ�.");
        }
    }

    public void SetIsMoving() // This method will be invoked after 10 seconds.
    {
        isMoving = true;
    }
}

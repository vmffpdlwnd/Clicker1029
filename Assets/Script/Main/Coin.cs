using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager; // GameManager 참조를 위한 변수

    private Vector3 targetPosition; // 이동할 목표 좌표
    private float moveSpeed = 10f; // 코인의 이동 속도

    private bool isMoving = false; // 코인의 이동 여부를 나타내는 변수

    private Rigidbody rb;

    private void Start()
    {
        gameManager = GameManager.Instance; // GameManager의 인스턴스를 가져옴

        targetPosition = new Vector3(0.4f, 2.8f, -6.5f); // 이동할 목표 좌표 설정

        rb = GetComponent<Rigidbody>(); // Get the reference to the Rigidbody component once

        Invoke("SetIsMoving", 10f); // 10초 후에 SetIsMoving 함수를 호출합니다.
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if ((transform.position - targetPosition).sqrMagnitude < 0.01f * 0.01f)
            {
                Destroy(gameObject); // 목표 위치에 도달하면 파괴합니다.
            }
        }
    }

    private void OnMouseDown()
    {
        isMoving = true; // 클릭되었을 때 이동 시작
        rb.useGravity = true; // Rigidbody의 중력 사용을 활성화합니다.
    }

    public void OnDestroy()
    {
        if (isMoving)
        {
            gameManager.AddGold(1);
            Debug.Log("골드가 추가되었습니다.");
            gameManager.AddExp(5);
            Debug.Log("경험치가 추가되었습니다.");
        }
    }

    public void SetIsMoving() // This method will be invoked after 10 seconds.
    {
        isMoving = true;
    }
}

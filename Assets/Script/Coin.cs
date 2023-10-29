using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager; // GameManager 참조를 위한 변수

    private void Start()
    {
        gameManager = GameManager.Instance; // GameManager의 인스턴스를 가져옴

        // 10초 후에 DestroyObject 함수를 호출합니다
        Invoke("DestroyObject", 10f);
 
    }

    private void OnMouseDown()
    {
        // 코인을 클릭하면 파괴합니다
        DestroyObject();
    }

    private void DestroyObject()
    {
        // GameManager로 데이터 전달
        gameManager.AddGold(1);
        gameManager.AddExp(5);

        // 코인을 파괴합니다
        Destroy(gameObject);
    }
}
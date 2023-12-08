using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 객체지향 프로그래밍 - 다양성(순수가상함수 혹은 인터페이스)을 구현 코드
public interface IBaseTownPopup
{
    public void PopupOpen();
    public void PopupClose();
}


public class Popup_Base : MonoBehaviour
{


    private void Awake()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.01f); // 게임 사작하면 기본적으로 팝업은 닫힌상태로 시작
    }

}

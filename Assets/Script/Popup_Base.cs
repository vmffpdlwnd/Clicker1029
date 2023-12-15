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
    public void PopupOpen()
    {
        transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutElastic);
    }
    public void PopupClose()
    {
        transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.OutElastic);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;  // UnityEditor 네임스페이스 추가
#endif

public class SettingPopup : MonoBehaviour
{
    [SerializeField]
    private Button saveBtn;
    [SerializeField]
    private Button exitBtn;

    private void Start()
    {
        // 버튼의 클릭 이벤트에 핸들러를 추가
        saveBtn.onClick.AddListener(OnButtonSave);
        exitBtn.onClick.AddListener(OnButtonExit);
    }

    // 저장 버튼 클릭 이벤트 핸들러
    private void OnButtonSave()
    {
        GameManager.Instance.SaveData(); // 게임 데이터 저장
        Debug.Log("저장되었습니다.");
    }
    // 종료 버튼 클릭 이벤트 핸들러
    private void OnButtonExit()
    {   
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;  // 에디터에서는 이 명령어를 사용
        #else
        Application.Quit();  // 빌드된 게임에서는 이 명령어를 사용
        #endif
    }
}
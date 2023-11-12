using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;  // UnityEditor ���ӽ����̽� �߰�
#endif

public class SettingPopup : MonoBehaviour
{
    [SerializeField]
    private Button saveBtn;
    [SerializeField]
    private Button exitBtn;

    private void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ�� �ڵ鷯�� �߰�
        saveBtn.onClick.AddListener(OnButtonSave);
        exitBtn.onClick.AddListener(OnButtonExit);
    }

    // ���� ��ư Ŭ�� �̺�Ʈ �ڵ鷯
    private void OnButtonSave()
    {
        GameManager.Instance.SaveData(); // ���� ������ ����
        Debug.Log("����Ǿ����ϴ�.");
    }
    // ���� ��ư Ŭ�� �̺�Ʈ �ڵ鷯
    private void OnButtonExit()
    {   
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;  // �����Ϳ����� �� ��ɾ ���
        #else
        Application.Quit();  // ����� ���ӿ����� �� ��ɾ ���
        #endif
    }
}
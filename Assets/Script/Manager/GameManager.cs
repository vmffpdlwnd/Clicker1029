using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public enum SceneName
{
    MainScene,
    GameScene,
}


public class PlayerData
{
    public string userNickName; 
    public int level;
    public int curExp;
    public int gold;
    public int CPU_level;
}

public class GameManager : Singleton<GameManager>
{
    private PlayerData pData = new PlayerData();
    public PlayerData PlayerInfo
    {
        get => pData;
    }

    private SceneName nextScene;
    public SceneName NextScene
    {
        get => nextScene;
    }

    private new void Awake()
    {
        base.Awake(); // �θ��� Awake ȣ��
        dataPath = Application.persistentDataPath + "/save";
        CheckData();
        SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ�� �޼��� ���
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ���� �޼��� ����
    }

    public void AsyncLoadNextScene(SceneName newScene)
    {
        SaveData();
        nextScene = newScene;
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ���ο� ���� �ε��� �Ϸᰡ �Ǹ� ȣ��Ǵ� �̺�Ʈ
    {
        if (scene.buildIndex > 2)
        {
            Debug.Log("ĳ���� ���̺� ������ �ε�");
            LoadData();
        }
    }

    #region _Save&Load_

    private string dataPath; //= Application.persistentDataPath + "/save"; // ����̽� ������ ������ save���� �߰�

    public void SaveData()
    {
        string data = JsonUtility.ToJson(pData); // ��ü������ stringŸ������ ���� ( Json ������ Ȱ�� )
        File.WriteAllText(dataPath, data);
    }

    public bool LoadData()
    {
        if (File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath);
            pData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }
        return false;
    }

    public void DeleteData()
    {
        File.Delete(dataPath);
    }
    public bool CheckData()
    {
        if (File.Exists(dataPath))
        {
            return LoadData();
        }
        return false;
    }

    #endregion

    #region _ UserData_

    public void CreateUserData(string newNickName) // ó�� ���� �����Ҷ� �ʱ� ������ ����
    {
        pData.userNickName = newNickName;
        pData.curExp = 0;
        pData.level = 1;
        pData.CPU_level = 1;

    }

    public string PlayerName
    {
        get => pData.userNickName;
    }
    public int PlayerLevel
    {
        get => pData.level;
    }
    public int PlayerCurrentExp
    {
        get => pData.curExp;
    }
    public int PlayerCurrentCPU_level
    {
        get => pData.CPU_level;
    }

    public void AddExp(int addEXP)
    {
        pData.curExp += addEXP;
        // todo : ������ ó��.
    }
    public int PlayerGold
    {
        get => pData.gold;
        set => pData.gold = value;
    }

    public void AddGold(int addGold)
    {
        pData.gold += addGold;
        // todo : �κ��丮, UI ����ó�� �߰�.
    }

    #endregion
}
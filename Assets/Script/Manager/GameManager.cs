using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public enum SceneName
{
    TitleScene,
    MainScene,
    GameScene1
}

public enum Save_Date
{
    save_NickName,
    save_SceneName,
    save_Coin_BGM, // ������ ���� ũ�⸦ ����.(�ɼ�)
    save_BGM, // ������� ���� ũ�⸦ ����.
    save_Level, // �÷��̾� ����
    save_CPU_level,
    save_Exp, // ����ġ
    save_Gold
}

[System.Serializable]
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
    private ClickerGame table;

    private PlayerData pData = new PlayerData();

    public bool isPaused = false;
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

        #region _TableData_
        table = Resources.Load<ClickerGame>("ClickerGame"); // ��Ÿ���߿� ��������(Resources)�� �����ؼ� ������ �������� �ε�.
        // ������ ���̺� ��ųʸ��� ����
        for (int i=0; i<)
      
        #endregion
        CheckData();
        SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ�� �޼��� ���
    }

    private void Update()
    {
        if (Time.time - lastSaveTime >= autoSaveInterval)
        {
            Debug.Log("Save Data!");
            SaveData();
            lastSaveTime = Time.time;
        }
        if (GameManager.Instance.isPaused)
        {
            return; // ������ �Ͻ����� ���¶�� Update�� ��������
        }

        // ������ �Ͻ����� ���°� �ƴ϶�� ���������� Update ����
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ���� �޼��� ����
    }

    public void AsyncLoadNextScene(SceneName newScene)
    {
        SaveData();
        nextScene = newScene;
        isPaused = true; // ���ο� ���� �ε��ϴ� ���� ������ �Ͻ� ���� ���·� ����
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ���ο� ���� �ε��� �Ϸᰡ �Ǹ� ȣ��Ǵ� �̺�Ʈ
    {
        if (scene.buildIndex > 2)
        {
            Debug.Log("ĳ���� ���̺� ������ �ε�");
            LoadData();
        }
        isPaused = false; // �� �ε��� �Ϸ�Ǹ� ������ �Ͻ� ���� ���¿��� ����
    }

    #region _Save&Load_

    private string dataPath; //= Application.persistentDataPath + "/save"; // ����̽� ������ ������ save���� �߰�
    private float autoSaveInterval = 60f; // �ڵ� ���� ���� (�� ����)
    private float lastSaveTime = 0f; // ������ ���� �ð�

  

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
        pData.gold = 0; // ���� ������ �ʱ�ȭ
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
        set => pData.level = value;
    }
    public int PlayerCurrentExp
    {
        get => pData.curExp;
        set => pData.curExp = value;
    }
    public int PlayerCurrentCPU_level
    {
        get => pData.CPU_level;
        set => pData.CPU_level = value;
    }

    public Text expText;

    public void AddExp(int addEXP)
    {
        pData.curExp += addEXP;
        UIManager.Instance.UpdateUI();
    }
    public int PlayerGold
    {
        get => pData.gold;
        set => pData.gold = value;
    }

    public Text goldText;

    public void AddGold(int addGold)
    {
        pData.gold += addGold * pData.level;
        UIManager.Instance.UpdateUI();
    }

    #endregion
}

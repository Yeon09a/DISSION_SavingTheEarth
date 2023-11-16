using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    public static DataManager instance = null; // 싱글턴 선언

    private string path;
    private string fileName = "PlayerSaveData";

    public PlayerData nowPlayerData;
    public PlayerData savePlayerData;


    private void Awake()
    {
        // 싱글턴
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/09. Data/";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData(PlayerData playerData) // 저장하기
    {
        string data = PlayerJsonUtility.ToJson(playerData);
        File.WriteAllText(path + fileName, data);
    }

    public void LoadData(int slotNum) // 불러오기
    {
        string loadData = File.ReadAllText(path + fileName + slotNum);
        savePlayerData = PlayerJsonUtility.FromJson(loadData);
    }

    public void PlayNewData() // nowPlayerData 세팅
    {
        nowPlayerData = new PlayerData(0, "잠수함", 2, 540, "오전  ", new Dictionary<int, HaveItemInfo>(), 0);
    }
}

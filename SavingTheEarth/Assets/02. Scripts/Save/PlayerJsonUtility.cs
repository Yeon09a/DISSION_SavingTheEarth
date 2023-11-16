using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class HaveItemDictionary<TKey, TValue>
{
    public TKey key;
    public TValue value;
}

[Serializable]
public class JsonData
{
    public List<HaveItemDictionary<int, HaveItemInfo>> haveItemInfoData;

    public List<PlayerData> playerData = new List<PlayerData>();

    public JsonData(List<HaveItemDictionary<int, HaveItemInfo>> haveItemInfoData, PlayerData playerData) // 플레이어 관련 변수 추가시 생성자도 추가됨
    {
        this.haveItemInfoData = haveItemInfoData;
        this.playerData.Add(playerData);
    }
}

public static class PlayerJsonUtility 
{
    public static string ToJson(PlayerData playerData, bool pretty = false)
    {
        List<HaveItemDictionary<int, HaveItemInfo>> dataList = new List<HaveItemDictionary<int, HaveItemInfo>>();
        HaveItemDictionary<int, HaveItemInfo> hIIDictonary; // haveIteonInfoDic
        foreach (int key in playerData.haveItems.Keys)
        {
            hIIDictonary = new HaveItemDictionary<int, HaveItemInfo>();
            hIIDictonary.key = key;
            hIIDictonary.value = playerData.haveItems[key];
            dataList.Add(hIIDictonary);
        }
        JsonData jsonData = new JsonData(dataList, playerData);

        return JsonUtility.ToJson(jsonData, pretty);
    }

    public static PlayerData FromJson(string jsonData)
    {
        JsonData loadJsonData = JsonUtility.FromJson<JsonData>(jsonData);
        Dictionary<int, HaveItemInfo> hIIDictonary = new Dictionary<int, HaveItemInfo>();

        foreach (HaveItemDictionary<int, HaveItemInfo> item in loadJsonData.haveItemInfoData)
        {
            hIIDictonary.Add(item.key, item.value);
        }

        PlayerData playerData = loadJsonData.playerData[0];
        playerData.haveItems = hIIDictonary;

        return playerData;
    }
}

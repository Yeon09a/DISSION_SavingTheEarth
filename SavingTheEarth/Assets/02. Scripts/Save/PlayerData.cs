using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerData // 플레이어 데이터 클래스
{
    // 플레이어 관련 변수
    public string playerName; // 플레이어 이름
    public int playTime; // 플레이 타임(초)
    public string placeName; // 장소 이름
    public int placeNum; // 장소 번호
    public Vector3 playerPos; // 플레이어 좌표
    public int gameTime; // 게임 시간
    public string ampm; // 오전, 오후
    public int money; // 돈
    public int[] fieldSpIndex; // 0 : 잔디, 1 : 간 땅, 2 : 젖은 땅
    public int[] fieldCrops; // 0 : 작물 없음, 1 : 감자, 2 : 토마토

    // 아이템 관련 변수
    [NonSerialized]
    public Dictionary<int, HaveItemInfo> haveItems; // 현재 가지고 있는 아이템 딕셔너리<아이템 id, 아이템 정보>

    public PlayerData(int playTime, string placeName, int placeNum, int gameTime, string ampm , Dictionary<int, HaveItemInfo> haveItems, int money) // 후에 플레이어 변수 추가시 매개변수 추가할 예정
    {
        this.playTime = playTime;
        this.placeName = placeName;
        this.placeNum = placeNum;
        this.gameTime = gameTime;
        this.ampm = ampm;
        this.haveItems = haveItems;
        this.money = money;
        fieldSpIndex = Enumerable.Repeat(0, 18).ToArray();
        fieldCrops = Enumerable.Repeat(0, 18).ToArray();

    }
}

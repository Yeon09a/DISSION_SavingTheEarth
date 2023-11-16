using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public string itName; // 아이템 이름
    public int id; // 아이템 id(배열 인덱스일 예정)
    [TextArea]
    public string info; // 아이템 정보
    public Sprite image; // 아이템 이미지
    public int type; // 아이템 종류(1 : 소지품, 2 : 중요물품)
    public int price; // 아이템 가격
}

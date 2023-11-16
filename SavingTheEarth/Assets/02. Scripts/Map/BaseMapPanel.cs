using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMapPanel : MonoBehaviour
{
    public delegate void BaseMapDel(); // 기본맵 관련 델리게이트
    public event BaseMapDel activateFace; // BaseMapFace 활성화 이벤트
    public event BaseMapDel deactivateFace; // BaseMapFace 비활성화 이벤트

    // 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerGroundCollider"))
        {
            activateFace();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerGroundCollider"))
        {
            deactivateFace();
        }
    }
}

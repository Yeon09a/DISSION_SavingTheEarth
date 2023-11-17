using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerDir
{ // 캐릭터 방향 열거
    Up,
    Down,
    Right,
    Left
};

public enum MapName
{ // 맵 열거형
    SaveTitle,
    Title,
    BaseMap
}

public class Player : Character
{

    public PlayerDir playerDir = PlayerDir.Down; // 플레이어 현재 방향

    private RaycastHit2D hit; // 레이캐스트 결괏값을 저장하기 위한 구조체 선언
    private Vector3 interPos; // 상호작용할 방향(레이 발사할 방향)
    private float rayLength; // 레이 길이

    public Action OpenShop; // 상점을 여는 액션

    GameObject scanObject; // 레이와 충돌한 오브젝트 저장

    public DialogManager dialogManager;


    protected override void Start()
    {
        base.Start();
        if (GameManager.instance.preMap == MapName.Title)
        {
            transform.position = new Vector3(12.63f, 3.3f, 0);
        } else if (GameManager.instance.preMap == MapName.SaveTitle)
        {
            transform.position = DataManager.instance.nowPlayerData.playerPos;
        }
    }

    // override는 상속받은 클래스의 메소드 중에서 virtual로 선언된 부분을 재정의
    protected override void Update()
    {
        GetInput();
        // base는 상속받은 클래스의 기능을 가리킴
        base.Move();

        // 키보드 상호작용 
        if (Input.GetKeyDown(KeyCode.E)) // 상호작용 키
        {
            if (playerDir == PlayerDir.Down) // 방향이 아래인 경우
            {
                interPos = Vector3.down;
                rayLength = 1.1f;
            }
            else if (playerDir == PlayerDir.Up) // 방향이 위인 경우
            {
                interPos = Vector3.up;
                rayLength = 1.1f;
            }
            else if (playerDir == PlayerDir.Right) // 방향이 오른쪽일 경우
            {
                interPos = Vector3.right;
                rayLength = 1.0f;
            }
            else if (playerDir == PlayerDir.Left) // 방향이 왼쪽일 경우
            {
                interPos = Vector3.left;
                rayLength = 1.0f;
            }

            hit = Physics2D.Raycast(transform.position, interPos, rayLength, 1 << 6); // 레이 발사
            //Debug.DrawRay(transform.position, interPos * rayLength, Color.red);
            if (hit.collider != null) // 충돌한 오브젝트가 있을 경우
            {
                // 여기에서 상호작용
                // hit.collider가 레이와 충돌한 오브젝트

                scanObject = hit.collider.gameObject;


                dialogManager.Talk(scanObject);
            }
            else
                scanObject = null;
        }
        HandleLayers();
    }

    // 키보드 입력값을 받음 (방향 및 멈춤 제어)
    public void GetInput()
    {
        Vector2 moveVector;

        // Input.GetAxisRaw(): 수평, 수직 버튼 입력시에 -1f, 1f 반환, 멈춰있을 때는 0f 반환
        moveVector.x = Input.GetAxisRaw("Horizontal"); // A키 or 왼쪽 화살표 -1f, D키 or 오른쪽 화살표 1f 값 반환
        moveVector.y = Input.GetAxisRaw("Vertical"); // S키 or 아래쪽 화살표 -1f, W키 or 위쪽 화살표 1f 값 반환

        direction = moveVector;

        // 플레이어의 애니메이션에 따른 현재 방향 설정
       if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Up")) 
        {
            playerDir = PlayerDir.Up;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Down"))
        {
            playerDir = PlayerDir.Down;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Right"))
        {
            playerDir = PlayerDir.Right;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(1).IsName("walk_Left"))
        {
            playerDir = PlayerDir.Left;
        }
    }

    // 마우스 상호작용 추가하기


    
}
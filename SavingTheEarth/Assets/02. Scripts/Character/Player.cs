using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // override는 상속받은 클래스의 메소드 중에서 virtual로 선언된 부분을 재정의
    protected override void Update()
    {
        GetInput();
        // base는 상속받은 클래스의 기능을 가리킴
        base.Move();
    }

    // 키보드 입력값을 받음 (방향 및 멈춤 제어)
    public void GetInput()
    {
        Vector2 moveVector;

        // Input.GetAxisRaw(): 수평, 수직 버튼 입력시에 -1f, 1f 반환, 멈춰있을 때는 0f 반환
        moveVector.x = Input.GetAxisRaw("Horizontal"); // A키 or 왼쪽 화살표 -1f, D키 or 오른쪽 화살표 1f 값 반환
        moveVector.y = Input.GetAxisRaw("Vertical"); // S키 or 아래쪽 화살표 -1f, W키 or 위쪽 화살표 1f 값 반환

        direction = moveVector;
<<<<<<< Updated upstream
=======

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
>>>>>>> Stashed changes
    }

    // 마우스 상호작용 추가하기

}
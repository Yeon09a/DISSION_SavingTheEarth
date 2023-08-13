using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // override�� ��ӹ��� Ŭ������ �޼ҵ� �߿��� virtual�� ����� �κ��� ������
    protected override void Update()
    {
        GetInput();
        // base�� ��ӹ��� Ŭ������ ����� ����Ŵ
        base.Move();
    }

    // Ű���� �Է°��� ���� (���� �� ���� ����)
    public void GetInput()
    {
        Vector2 moveVector;

        // Input.GetAxisRaw(): ����, ���� ��ư �Է½ÿ� -1f, 1f ��ȯ, �������� ���� 0f ��ȯ
        moveVector.x = Input.GetAxisRaw("Horizontal"); // AŰ or ���� ȭ��ǥ -1f, DŰ or ������ ȭ��ǥ 1f �� ��ȯ
        moveVector.y = Input.GetAxisRaw("Vertical"); // SŰ or �Ʒ��� ȭ��ǥ -1f, WŰ or ���� ȭ��ǥ 1f �� ��ȯ

        direction = moveVector;
    }

    // ���콺 ��ȣ�ۿ� �߰��ϱ�

}
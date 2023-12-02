using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// abstract를 통한 추상 클래스 사용
public abstract class Character : MonoBehaviour
{
    // 인스펙터창에 보여짐
    [SerializeField]
    private float speed;
    public float hp;
    protected Vector2 direction;
    protected Animator myAnimator;
    private Rigidbody2D myRigidbody;

    public LayerMask groundMask;

    public float distance;

    public Transform chkPos;
    public bool isGround;
    public float checkRadius;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // protected는 상속받은 클래스에서만 접근 가능
    // virtual을 통해서 상속 가능
    protected virtual void Update()
    {
        
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // 캐릭터 이동
    public void Move()
    {
        // direction 값 0f일 시에 멈춤
        myRigidbody.velocity = direction.normalized * speed;

        // 특정 씬에서는 y축 이동 금지
        if (GameManager.instance.curMap == MapName.SeaMap)
        {

            if (direction.x == 0)
                myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePosition;
            else
                myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            //오르막길 추가함수
            //RaycastHit2D hit = Physics2D.Raycast(chkPos.position, Vector2.down, distance, groundMask);
            //angle = Vector2.Angle(hit.normal, Vector2.up);

            //Debug.DrawLine(hit.point, hit.point + hit.normal, Color.yellow);

            direction.y = 0f;

            Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.gravityScale = 10f;
            }

            // MoveCollider 오브젝트의 박스 콜라이더를 찾아 비활성화
            GameObject moveCollider = transform.Find("MoveCollider").gameObject;
            if (moveCollider != null)
            {
                BoxCollider2D boxCollider = moveCollider.GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;
                }
            }
        }
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("Walk Layer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
        }
        else
        {
            ActivateLayer("Idle Layer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }


    // 스피드 setter
    public void SetSpeed(float sp)
    {
        speed = sp;
    }

    // 스피드 getter
    public float GetSpeed()
    {
        return speed;
    }

    // 애니메이터 setter
    public void SetAnimator(bool b)
    {
        myAnimator.enabled = b;
        myAnimator.Play("idle_Up", 0);
    }
}
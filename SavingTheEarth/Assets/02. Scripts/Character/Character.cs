using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract를 통한 추상 클래스 사용
public abstract class Character : MonoBehaviour
{
    // 인스펙터창에 보여짐
    [SerializeField]
<<<<<<< Updated upstream
    private float speed;
    protected Vector2 direction;
    private Animator animator;
=======
    protected float speed;
    protected float hp; // 오류 방지를 위해 추가
    protected Vector2 direction;
    protected Animator myAnimator;
    private Rigidbody2D myRigidbody;
>>>>>>> Stashed changes

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // protected는 상속받은 클래스에서만 접근 가능
    // virtual을 통해서 상속 가능
    protected virtual void Update()
    {
        Move();
    }

    // 캐릭터 이동
    public void Move()
    {
        // direction 값 0f일 시에 멈춤
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    //파라미터 값에 따른 애니메이션 변환
    public void AnimateMovement(Vector2 direction)
    {
        myAnimator.SetLayerWeight(1, 1);

        myAnimator.SetFloat("x", direction.x);
        myAnimator.SetFloat("y", direction.y);
    }
<<<<<<< Updated upstream
=======



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
>>>>>>> Stashed changes
}
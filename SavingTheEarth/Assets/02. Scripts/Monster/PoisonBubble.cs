using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBubble : MonoBehaviour
{
    public float bubbleRot;
    private Vector3 moveDir;

    private float speed = 1.5f;

    private bool isMove = false;

    public MiddleOctMonster mOctMon;

    // Start is called before the first frame update
    void Start()
    {
        mOctMon.startBubbleMove += startBubbleMove;
        moveDir = (Quaternion.Euler(0, 0, bubbleRot) * transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.Translate(moveDir * speed * Time.deltaTime);
        }
    }

    IEnumerator BubbleMove() {

        isMove = true;

        yield return new WaitForSeconds(5.0f);

        this.gameObject.SetActive(false);
    }

    private void startBubbleMove()
    {
        StartCoroutine(BubbleMove());
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
}

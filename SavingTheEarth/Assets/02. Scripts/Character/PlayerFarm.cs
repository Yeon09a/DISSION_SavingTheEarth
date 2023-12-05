using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarm : MonoBehaviour
{
    private RaycastHit2D hit; // 레이캐스트 결괏값을 저장하기 위한 구조체 선언

    GameObject scanObject; // 레이와 충돌한 오브젝트 저장

    [SerializeField]
    private int fieldNum; // 선택된 fieldNum
    [SerializeField]
    private int curState; // 0 : 잔디, 1 : 간 땅, 2 : 젖은 땅
    [SerializeField]
    private int cropState; // 0 : 없음, 1 : 감자 씨앗, 2 : 감자 싹, 3 : 감자 작물, 4 : 토마토 씨앗, 5 : 토마토 싹, 6 : 토마토 작물

    public int curTool = 0; // 현재 플레이어가 선택한 도구 0 : 없음, 1 : 호미, 2 : 물뿌리개, 3 : 씨앗 바구니
    public int seedCount = 0;

    public Action<int, int, int> SetField;
    public Action<int> SetSeedCount;
    public Action<string> OnFarmInfo;

    public InventoryManager invenMng;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 상호작용 키
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.4f, 0), Vector3.forward * -1, 1.0f, 1 << 9); // 레이 발사
            if (hit.collider != null) // 충돌한 오브젝트가 있을 경우
            {
                // 여기에서 상호작용
                // hit.collider가 레이와 충돌한 오브젝트
                scanObject = hit.collider.gameObject;

                Debug.Log(scanObject.name);

                if (scanObject.CompareTag("FarmField"))
                {
                    Vector3 fieldInfo = scanObject.GetComponent<Field>().fieldInfo;
                    fieldNum = (int)fieldInfo.x;
                    curState = (int)fieldInfo.y;
                    cropState = (int)fieldInfo.z;

                    if (cropState != 3 && cropState != 6)
                    {
                        CheckField();
                    }
                    else
                    {
                        bool isNotFull = invenMng.PutItem(cropState == 3 ? 6 : 7, 1);

                        if (!isNotFull)
                        {
                            // 상자에 들어감.
                            OnFarmInfo("가방에 빈 자리가 없어 수확한\n감자를 상자에 넣었습니다.");

                            // 상자
                        }
                    }
                }
            }
            else
                scanObject = null;
        }
    }

    private void CheckField()
    {
        switch (curTool)
        {
            case 1: // 호미
                if (curState == 0 && cropState == 0) // 잔디
                {
                    scanObject.GetComponent<Field>().fieldInfo = new Vector3(fieldNum, 1, 0);
                    SetField(fieldNum, 1, 0);
                }
                break;
            case 2: // 물뿌리개
                if (curState == 1) // 간 밭
                {
                    scanObject.GetComponent<Field>().fieldInfo = new Vector3(fieldNum, 2, cropState);
                    SetField(fieldNum, 2, cropState);
                }
                break;
            case 3: // 씨앗 바구니
                if (curState != 0 && cropState == 0)
                {
                    int count = DataManager.instance.nowPlayerData.haveItems.ContainsKey(6) ? DataManager.instance.nowPlayerData.haveItems[6].count : 0;

                    if (count > 0)
                    {
                        count--;
                        SetSeedCount(count);
                        invenMng.UseItem(6);
                        scanObject.GetComponent<Field>().fieldInfo = new Vector3(fieldNum, curState, 1);
                        SetField(fieldNum, curState, 1);
                    }
                    else
                    {
                        OnFarmInfo("심을 감자 씨앗이 없습니다.");
                    }

                }
                break;
        }
    }
}

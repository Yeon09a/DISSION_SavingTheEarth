using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeaMap : MonoBehaviour
{
    public GameObject SeaMapFace;
    public SeaMapPanel SeaMapPanel;

    // Start is called before the first frame update
    void Start()
    {
        // 현재 씬에서 Player를 찾아봄
        Player player = GameObject.FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("현재 씬에서 Player를 찾을 수 없습니다.");
        }
        else
        {
            Debug.Log("현재 씬에서 Player를 찾았습니다.");
        }

        // SeaMapPanel의 이벤트 핸들러들을 연결
        SeaMapPanel.activateFace += ActivateFace;
        SeaMapPanel.deactivateFace += DeactivateFace;
    }

    // Update is called once per frame
    void Update()
    {
        // 입력 또는 기타 로직을 확인할 수 있음
    }

    private void ActivateFace()
    {
        SeaMapFace.SetActive(true);
    }

    private void DeactivateFace()
    {
        SeaMapFace.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSeaMap : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        // 플레이어가 "OutDoor"와 충돌하고 "E" 키를 누르면 "SeaMap"으로 이동
        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.curMap = MapName.SeaMap;
            GameManager.instance.preMap = MapName.BaseMap;

            if (player.ScanObject != null && player.ScanObject.CompareTag("Door"))
            {
                SceneManager.LoadScene("SeaMap");
                SceneManager.LoadScene("Player", LoadSceneMode.Additive);
            }
        }
    }
}
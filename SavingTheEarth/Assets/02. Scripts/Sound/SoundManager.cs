using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource1;
    public AudioSource musicsource2;
    //해당방법으로 사운드 불러오는게 비효율적이라 추후 연결시엔 리스트형태로 관리하여 호출
    //public AudioSource musicsource3;
    public AudioSource btnsource;

    void Awake()
    {

        // 만일 현재 씬이 "BaseMap"이면 musicsource1을 활성화하고, "SeaMap"이면 musicsource2를 활성화합니다.
        if (GameManager.instance.curMap == MapName.BaseMap)
        {
            musicsource1.gameObject.SetActive(true);
            musicsource2.gameObject.SetActive(false);
            //musicsource3.gameObject.SetActive(false);
        }
        else if (GameManager.instance.curMap == MapName.SeaMap)
        {
            musicsource1.gameObject.SetActive(false);
            musicsource2.gameObject.SetActive(true);
            //musicsource3.gameObject.SetActive(false);

            /*if(보스전시작)
            {
                musicsource1.gameObject.SetActive(false);
                musicsource2.gameObject.SetActive(false);
                musicsource3.gameObject.SetActive(true);

            }*/
        }

    }
}

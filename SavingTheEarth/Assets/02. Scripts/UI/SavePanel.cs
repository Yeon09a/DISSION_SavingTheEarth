using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    private string path;

    public bool isFile;
    public int slotNum;

    public GameObject saveInfoPanel;
    
    void Start()
    {
        path = Application.dataPath + "/09. Data/";
        GetComponent<Button>().onClick.AddListener(ClickSavePanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ClickSavePanel()
    {
        if (isFile)
        {
            DataManager.instance.LoadData(slotNum);
            EnterGame();


        }
        else
        {
            saveInfoPanel.SetActive(true);
        }
    }

    public void EnterGame()
    {
        MapName nowMap = (MapName)DataManager.instance.nowPlayerData.placeNum;
        GameManager.instance.curMap = nowMap;
        GameManager.instance.preMap = MapName.SaveTitle;
        SceneManager.LoadScene(nowMap.ToString());
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }
}

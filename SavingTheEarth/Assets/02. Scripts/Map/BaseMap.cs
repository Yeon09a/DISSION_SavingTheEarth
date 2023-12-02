using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMap : MonoBehaviour
{
    public GameObject baseMapFace;
    public BaseMapPanel baseMapPanel;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        baseMapPanel.activateFace += ActivateFace;
        baseMapPanel.deactivateFace += DeactivateFace;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivateFace()
    {
        baseMapFace.SetActive(true);
    }

    private void DeactivateFace()
    {
        baseMapFace.SetActive(false);
    }

    private void OnDisable()
    {
        baseMapPanel.activateFace -= ActivateFace;
        baseMapPanel.deactivateFace -= DeactivateFace;
    }
}

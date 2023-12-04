using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmInfoPanel : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(OffFarmInfo());
    }

    IEnumerator OffFarmInfo()
    {
        Debug.Log("시작");
        yield return new WaitForSecondsRealtime(5f);
        Debug.Log("종료");
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(OffFarmInfo());
    }
}

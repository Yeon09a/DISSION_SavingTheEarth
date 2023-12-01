using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public Image progressBar; // 로딩 바
    private static string nextScene; // 이동할 다음 씬
    private float fullSize = 718; // 로딩 바 반절 사이즈
    private RectTransform progressBarRt; // 로딩 바 RectTransform

    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        progressBarRt = progressBar.GetComponent<RectTransform>();
        
        // 코루틴 로딩 시작
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName) // 씬 이동 함수
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading"); // 로딩씬 이동
    }


    IEnumerator LoadScene() // 씬 이동 코루틴
    {
        yield return null;

        // 비동기 씬 이동
        AsyncOperation op1 = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        AsyncOperation op2 = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

        // 씬 비활성화
        op1.allowSceneActivation = false;
        op2.allowSceneActivation = false;

        while (!op1.isDone && !op2.isDone)
        {
            yield return null;

            // 로딩 바 업데이트
            float op1Value = Mathf.Lerp(0, fullSize, op1.progress);
            float op2Value = Mathf.Lerp(0, fullSize, op2.progress);

            progressBarRt.sizeDelta = new Vector2(op1Value + op2Value , 40);

            if (op1.progress >= 0.9f && op2.progress >= 0.9f) // 로딩 완료 시
            {
                progressBarRt.sizeDelta = new Vector2(fullSize * 2, 40);

                yield return new WaitForSeconds(0.5f);

                // 씬 활성화
                op1.allowSceneActivation = true;
                op2.allowSceneActivation = true;
            }
        }

        SceneManager.UnloadSceneAsync("Loading"); // 로딩 씬 종료
    }
}

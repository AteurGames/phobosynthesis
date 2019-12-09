using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour {
    public GameObject LoadingPanel;
    public GameObject LoadingText;
    private CanvasGroup cg;
    private void Start() {
        cg = LoadingPanel.GetComponent<CanvasGroup>();
        LoadingPanel.SetActive(false);
    }
    private IEnumerator FadeAsync() {
        float lerpTime = 0.5f;
        float start = 0f;
        float end = 1f;

        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true) {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            Debug.Log(cg.alpha);

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        print("done");
    }
    public void Begin() {
        LoadingPanel.SetActive(true);
        StartCoroutine(this.FadeAsync());
        SceneManager.LoadSceneAsync("Level1");
    }
    public void Quit() {
        Application.Quit();
    }
}

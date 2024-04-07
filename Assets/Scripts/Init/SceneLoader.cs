using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace Init
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Image loadingBar;
        [SerializeField] private GameObject view;
        [SerializeField] private TextMeshProUGUI percText;
        [SerializeField] private TextMeshProUGUI message;
        private float _tt => Time.realtimeSinceStartup;

        private void Awake()
        {
            loadingBar.fillAmount = 0;
            EventsModel.LOAD_SCENE += LoadScene;

            view.SetActive(false);
        }

        private void OnDestroy()
        {
            Debug.Log($"{_tt} :: SceneLoader :: destroyed");
            EventsModel.LOAD_SCENE -= LoadScene;
        }

        private void LoadScene(string sceneName, bool isAdditive, string msg)
        {
            message.SetText(string.IsNullOrEmpty(msg) ? "" : msg);
            StartCoroutine(LoadSceneAsync(sceneName, isAdditive));
        }

        private IEnumerator LoadSceneAsync(string sceneName, bool isAdditive)
        {
            string prvScene = SceneManager.GetActiveScene().name;

            view.SetActive(true);
            yield return new WaitForEndOfFrame();

            Debug.Log($"{_tt} :: SceneLoader :: BEGIN :: {prvScene} -> {sceneName} :: {(isAdditive ? "Additive" : "Single")}");
            EventsModel.PRE_SCENE_LOAD_BEGIN?.Invoke(sceneName);

            FillLoader(0f);
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            float startTime = Time.time;

            while (!op.isDone)
            {
                // [op.progress] goes from 0.0 to 0.9 (max 90%)
                FillLoader(op.progress / 0.9f);
                yield return null;
            }

            // we need to show the loader from a minimum time. So, we make a dummy progress.
            float timeTakenToLoad = Time.time - startTime;

            FillLoader(1f);
            bool shouldHide = true;
            if (shouldHide)
            {
                view.SetActive(false);
                yield return new WaitForEndOfFrame();
            }

            Debug.Log($"{_tt} :: SceneLoader :: END :: {prvScene} -> {sceneName}");
            EventsModel.SCENE_LOAD_COMPLETED?.Invoke(sceneName);
        }

        // perc in range [0, 1]
        private void FillLoader(float perc)
        {
            loadingBar.fillAmount = Mathf.Clamp(perc, 0f, 1f);
            percText.text = (loadingBar.fillAmount * 100).ToString("0") + "%";
        }
    }
}
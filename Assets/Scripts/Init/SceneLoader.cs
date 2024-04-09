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
            EventsModel.FORCE_HIDE_LOADER += ForceHideView;

            view.SetActive(false);
        }

        private void OnDestroy()
        {
            Debug.Log($"{_tt} :: SceneLoader :: destroyed");
            EventsModel.LOAD_SCENE -= LoadScene;
            EventsModel.FORCE_HIDE_LOADER -= ForceHideView;
        }

        private void LoadScene(string sceneName, bool isAdditive, string msg)
        {
            message.SetText(string.IsNullOrEmpty(msg) ? "" : msg);
            StartCoroutine(LoadSceneAsync(sceneName, isAdditive));
        }

        private void ForceHideView() => view.SetActive(false);

        private IEnumerator LoadSceneAsync(string sceneName, bool isAdditive)
        {
            string prvScene = SceneManager.GetActiveScene().name;

            view.SetActive(true);
            yield return new WaitForEndOfFrame();

            Debug.Log($"{_tt} :: SceneLoader :: BEGIN :: {prvScene} -> {sceneName} :: {(isAdditive ? "Additive" : "Single")}");
            FillLoader(0f);
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            float startTime = Time.time;

            while (!op.isDone)
            {
                FillLoader(op.progress / 0.9f);
                yield return null;
            }

            float timeTakenToLoad = Time.time - startTime;

            FillLoader(1f);

            // continue to show loader on PROGRESS_RESTORE and GAMEPLAY scenes
            bool shouldHide = sceneName != GameConstants.Scenes.PROGRESS_RESTORE && sceneName != GameConstants.Scenes.GAMEPLAY;
            if (shouldHide)
            {
                view.SetActive(false);
                yield return new WaitForEndOfFrame();
            }

            Debug.Log($"{_tt} :: SceneLoader :: END :: {prvScene} -> {sceneName}");
        }

        // perc in range [0, 1]
        private void FillLoader(float perc)
        {
            loadingBar.fillAmount = Mathf.Clamp(perc, 0f, 1f);
            percText.text = (loadingBar.fillAmount * 100).ToString("0") + "%";
        }
    }
}
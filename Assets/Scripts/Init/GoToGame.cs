using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Temp
{
    public class GoToGame : MonoBehaviour
    {
        [SerializeField] private float waitTime = 0.5f;
        private void Start()
        {
            Debug.Log($"GoToGame :: start");
            StartCoroutine(WaitAndGoToScene());
        }

        private IEnumerator WaitAndGoToScene()
        {
            yield return new WaitForSeconds(waitTime);
            GoToScene();
        }

        public void GoToScene() => Events.EventsModel.LOAD_SCENE?.Invoke(GameConstants.Scenes.GAMEPLAY, false, "Loading Game...");
    }
}
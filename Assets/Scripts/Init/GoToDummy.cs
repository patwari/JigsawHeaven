using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Temp
{
    public class GoToDummy : MonoBehaviour
    {
        [SerializeField] private float waitTime = 0.5f;
        private void Start()
        {
            Debug.Log($"GoToDummy :: start");
            StartCoroutine(WaitAndGoToScene());
        }

        private IEnumerator WaitAndGoToScene()
        {
            yield return new WaitForSeconds(waitTime);
            GoToScene();
        }

        public void GoToScene() => Events.EventsModel.LOAD_SCENE?.Invoke(GameConstants.Scenes.DUMMY, false, "Loading Dummy...");
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Temp
{
    public class GoToProgressRestore : MonoBehaviour
    {
        [SerializeField] private float waitTime = 0.5f;
                private void Start()
        {
            Debug.Log($"GoToProgressRestore :: start");
            StartCoroutine(WaitAndGoToScene());
        }

        private IEnumerator WaitAndGoToScene()
        {
            yield return new WaitForSeconds(waitTime);
            GoToScene();
        }

                public void GoToScene()
        {
            Events.EventsModel.LOAD_SCENE?.Invoke(GameConstants.Scenes.PROGRESS_RESTORE, false, "Loading Game...");
        }
    }
}
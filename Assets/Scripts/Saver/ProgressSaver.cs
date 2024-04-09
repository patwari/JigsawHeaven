using UnityEngine;
using Utils;

namespace Saver
{
    public class ProgressSaver : MonoBehaviour
    {
        private readonly string filename = "entry.json";

        private float timeSinceLastTimerSave = 0f;
        private float timeGap = 10f;

        public SModel_TileManager model;

        private void Awake()
        {
            DI.di.SetProgressSaver(this);
        }

        public void Start()
        {
            string str = FileUtils.Read(filename);
            if (string.IsNullOrEmpty(str))
                model = null;

            model = JsonUtility.FromJson<SModel_TileManager>(str);

            // TODO: patwari: to remove
            // model = null;
            // PlayerPrefs.DeleteKey("level_to_play");
        }

        public void DeleteFile() => FileUtils.DeleteFile(filename);

        public void Save()
        {
            if (model == null) return;

            string str = JsonUtility.ToJson(model, true);
            FileUtils.Write(filename, str);
        }
    }
}
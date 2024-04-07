using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

namespace Sound
{
    public partial class SoundManager : MonoBehaviour
    {
        public bool canPlaySfx { get; private set; } = true;

        [SerializeField] private AudioSource _defaultButtonSound;

        private void Awake()
        {
            if (DI.di.soundManager != null)
            {
                Destroy(gameObject);
                return;
            }
            DI.di.SetSoundManager(this);
        }

        public void PlaySfx(string name)
        {
            if (!canPlaySfx) return;
            var audioSource = transform.Find(name)?.GetComponent<AudioSource>();
            if (audioSource != null) audioSource.Play();
        }

        public void StopSfx(string name)
        {
            var audioSource = transform.Find(name)?.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
        }

        public void PlayDefaultButtonClick()
        {
            if (!canPlaySfx) return;
            _defaultButtonSound.Play();
        }
    }
}
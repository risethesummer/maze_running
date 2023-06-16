using MazeRunning.Utils.Collections;
using UnityEngine;

namespace MazeRunning.Sound
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private SerializableDictionary<SoundName, AudioClip> sounds;
        // [SerializeField] private SerializableDictionary<SfxSoundName, AudioClip> vfxSounds;
        private void Awake()
        {
            sounds.Init();
        }

        public void PlaySound(SoundName soundName, bool isLoop = true)
        {
            if (sounds.Dict.TryGetValue(soundName, out var clip))
            {
                audioSource.clip = clip;
                audioSource.Stop();
                audioSource.loop = isLoop;
                audioSource.Play();
            }
        }

        public void ContinuePlaying(bool con)
        {
            if (con)
                audioSource.Play();
            // else
            // {
            //     Debug.Log("Pause");
            //     audioSource.Pause();
            // }
        }

        public void PlayVfx(SoundName sfxName)
        {
            //Debug.Log(sfxName);
            if (sounds.Dict.TryGetValue(sfxName, out var clip))
                sfxSource.PlayOneShot(clip);
        }
    }
}
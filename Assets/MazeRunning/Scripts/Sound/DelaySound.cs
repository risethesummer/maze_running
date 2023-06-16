using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MazeRunning.Sound
{
    public class DelaySound
    {
        private readonly AudioManager _audio;
        private readonly float _delay;
        private bool _isDelaying = false;
        public DelaySound(AudioManager audioManager, float delay)
        {
            _audio = audioManager;
            _delay = delay;
        }
        public void PlaySound(SoundName soundName)
        {
            if (_isDelaying)
                return;
            _isDelaying = true;
            WaitPlaySound().Forget();
            _audio.PlayVfx(soundName);
        }
        private async UniTaskVoid WaitPlaySound()
        {
            float current = 0;
            while (current <= _delay)
            {
                current += Time.deltaTime;
                await UniTask.NextFrame();
            }
            _isDelaying = false;
        }
    }
}
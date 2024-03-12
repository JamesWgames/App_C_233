using UI.Panels;
using UnityEngine;

using AudioSettings = Prototype.AudioCore.AudioSettings;

namespace UI
{
    public class Settings : Panel
    {
        [SerializeField] private CustomToggle _soundToggle = null;
        [SerializeField] private CustomToggle _musicToggle = null;

        private void Start()
        {
            _soundToggle.IsOn = AudioSettings.IsSoundsEnabled();
            _musicToggle.IsOn = AudioSettings.IsMusicEnabled();

            _soundToggle.OnChangedValue += SetSoundEnabled;
            _musicToggle.OnChangedValue += SetMusicEnabled;
        }

        private void SetSoundEnabled(bool enable) =>
            AudioSettings.EnableSounds(enable);

        private void SetMusicEnabled(bool enable) =>
            AudioSettings.EnableMusic(enable);
    }
}

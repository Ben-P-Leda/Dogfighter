using UnityEngine;
using System.Collections.Generic;

namespace Shared.Scripts
{
    public class SoundEffectPlayer : MonoBehaviour
    {
        private static SoundEffectPlayer _instance;

        public static void PlaySound(string name) { _instance.PlaySoundEffect(name, 1.0f); }
        public static void PlaySound(string name, float pitch) { _instance.PlaySoundEffect(name, pitch); }

        private List<AudioSource> _voices;
        private Dictionary<string, AudioClip> _effects;

        public int VoiceCount;
        public List<AudioClip> Effects;

        private void Awake()
        {
            _effects = new Dictionary<string, AudioClip>();
            foreach (AudioClip clip in Effects) { _effects.Add(clip.name, clip); }

            _voices = new List<AudioSource>();
            for (int i = 0; i < VoiceCount; i++) { _voices.Add(gameObject.AddComponent<AudioSource>()); }

            _instance = this;
        }

        private void PlaySoundEffect(string name, float pitch)
        {
            if (_effects.ContainsKey(name))
            {
                AudioSource voice = GetFirstAvailableVoice();

                if (voice != null)
                {
                    voice.pitch = pitch;
                    voice.clip = _effects[name];
                    voice.Play();
                }
            }
        }

        private AudioSource GetFirstAvailableVoice()
        {
            AudioSource voice = null;
            for (int i = 0; ((i < _voices.Count) && (voice == null)); i++)
            {
                if (!_voices[i].isPlaying) { voice = _voices[i]; }
            }
            return voice;
        }
    }
}
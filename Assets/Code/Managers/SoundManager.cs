using Assets.Code.Abstractions;
using UnityEngine;

namespace Assets.Code.Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource MusicSource;

        private void Start()
        {
            MusicSource = GetComponent<AudioSource>();
        }
    }
}

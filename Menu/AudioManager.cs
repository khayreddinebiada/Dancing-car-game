using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager audioManager;
        public AudioSource[] sources;
        public int numberOfStage = 0;
        public bool musicIsPlay = true;

        private bool stopMusicOnPlay = true;

        // Use this for initialization
        void Awake()
        {

            sources = GetComponents<AudioSource> ();
            audioManager = this;
            numberOfStage = Data.getStageNumberWithPrefabs();
        }

        void Start()
        {

            musicIsPlay = Data.GetPlayAudio();

            int volume = (musicIsPlay) ? 1 : 0;
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].volume = volume;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                stopMusicOnPlay = false;

                if (!sources[numberOfStage].isPlaying)
                    sources[numberOfStage].Play();

                StartCoroutine(RecursiveTiming());
            }
            else
            {
                if (!stopMusicOnPlay)
                {
                    sources[numberOfStage].Stop();
                    stopMusicOnPlay = true;
                }
            }
            
        }

        IEnumerator RecursiveTiming()
        {
            yield return new WaitForSeconds(0.5f);

            if (musicIsPlay != Data.GetPlayAudio())
            {
                int volume = (musicIsPlay) ? 0 : 1;
                for (int i = 0; i < sources.Length; i++)
                {
                    sources[i].volume = volume;
                }

                musicIsPlay = Data.GetPlayAudio();
            }

            int stage = Data.getStageNumberWithPrefabs();
            if (stage != numberOfStage)
            {
                sources[numberOfStage].Stop();
                sources[stage].Play();
                numberOfStage = stage;
            }

            StartCoroutine(RecursiveTiming());
        }
    }
    
}

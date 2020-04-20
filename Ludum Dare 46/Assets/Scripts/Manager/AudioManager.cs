using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mikabrytu.LD46
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource footstepSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource whisperSource;
        [SerializeField] private AudioClip soundtrack;
        [SerializeField] private AudioClip blink, fix, broken, caught;

        private IEnumerator footstepRoutine;

        public void PlayFootstep(bool play)
        {
            Debug.Log(play);

            if (play)
            {
                if (footstepRoutine != null)
                    return;

                footstepRoutine = FootstepInterval();
                StartCoroutine(footstepRoutine);
            } else
            {
                if (footstepRoutine != null)
                {
                    StopCoroutine(footstepRoutine);
                    footstepRoutine = null;
                }
            }
        }

        public void PlayCaught()
        {
            if (sfxSource.clip != caught)
                sfxSource.clip = caught;
            sfxSource.Play();
        }

        public void PlayFix()
        {
            if (sfxSource.clip != fix)
                sfxSource.clip = fix;
            sfxSource.Play();
        }

        public void PlayBroke()
        {
            if (sfxSource.clip != broken)
                sfxSource.clip = broken;
            sfxSource.Play();
        }

        public void PlayWhisper()
        {
            whisperSource.Play();
        }

        public IEnumerator FootstepInterval()
        {
            while (true)
            {
                footstepSource.Play();
                yield return new WaitForSeconds(.3f);
            }
        }
    }
}

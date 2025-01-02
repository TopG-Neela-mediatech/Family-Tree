using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.AddTest
{
    public class AddTestSoundManager : MonoBehaviour
    {
        private AddTestSoundSO AddTestSoundSO;
        [SerializeField] private AddTestSoundSO USENGAudio;
        [SerializeField] private AddTestSoundSO HINDIAudio;
        [SerializeField] private AddTestSoundSO TAMILAudio;
        [SerializeField] private AudioSource levelAudio;
        //[SerializeField] private AudioSource numberAudio;
        [SerializeField] private AudioSource sfxAudio;
        [SerializeField] private string audioLocalization;
        [SerializeField] private SoundType[] audioClips;
        private bool playedIntro;


       /* private void Awake()
        {
            SetLanguage();
        }
        private void Start()
        {
            AddTestGameManager.Instance.OnLevelStart += () => playedIntro = false;
            AddTestGameManager.Instance.OnLevelStart += PlayLevelStartAudio;
        }
        private void SetLanguage()
        {
            audioLocalization = PlayerPrefs.GetString("PlaySchoolLanguage", audioLocalization);
            switch (audioLocalization)
            {
                case "English":
                    AddTestSoundSO = USENGAudio;
                    break;
                case "EnglishUS":
                    AddTestSoundSO = USENGAudio;
                    break;
                case "Hindi":
                    AddTestSoundSO = HINDIAudio;
                    break;
                case "Tamil":
                    AddTestSoundSO = TAMILAudio;
                    break;
                default:
                    AddTestSoundSO = USENGAudio;
                    break;
            }
        }
        private void PlayLevelAudio(AudioClip clip)
        {
            if (clip != null)
            {
                levelAudio.PlayOneShot(clip);
            }
            else
            {
                Debug.Log("Clip Not Found");
            }
        }
        public void PlayLevelStartAudio()//commonsound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int currentLevelNumber = AddTestGameManager.Instance.LevelManager.GetCurrentQuestionIndex();
            if (currentLevelNumber == 0 && !playedIntro)
            {
                playedIntro = true;
                PlayLevelAudio(AddTestSoundSO.gameIntro);
            }
            else
            {
                PlayLevelAudio(AddTestSoundSO.levelStartAudio[currentLevelNumber]);
            }
        }
        public void PlaySFX(Sounds sound)
        {
            AudioClip clip = GetSoundClip(sound);
            if (clip != null)
            {
                if (sfxAudio.isPlaying) { sfxAudio.Stop(); }
                sfxAudio.PlayOneShot(clip);
            }
            else
            {
                Debug.Log("Audio Not Assigned");
            }
        }
        private AudioClip GetSoundClip(Sounds sound)
        {
            SoundType item = Array.Find(audioClips, i => i.soundtype == sound);
            if (item != null)
            {
                return item.soundclip;
            }
            else
            {
                return null;
            }
        }
        /* private void PlayNumberAudio(AudioClip clip)
         {
             if (clip != null)
             {
                 numberAudio.PlayOneShot(clip);
             }
             else
             {
                 Debug.Log("Clip Not Found");
             }
         }*/
       /* public void PlayCorrectAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, AddTestSoundSO.correctAudio.Length);
            PlayLevelAudio(AddTestSoundSO.correctAudio[random]);
        }
        public void PlayTimeUpAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, AddTestSoundSO.timeUpAudio.Length);
            PlayLevelAudio(AddTestSoundSO.timeUpAudio[random]);
        }
        public void PlayIncorrectAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, AddTestSoundSO.incorrectAudio.Length);
            PlayLevelAudio(AddTestSoundSO.incorrectAudio[random]);
        }

        public void PlayFinalAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayLevelAudio(AddTestSoundSO.gameOutro);
        }
        private void OnDestroy()
        {
            AddTestGameManager.Instance.OnLevelStart -= PlayLevelStartAudio;
            AddTestGameManager.Instance.OnLevelStart -= () => playedIntro = false;
        }*/
    }
    [Serializable]
    public class SoundType
    {
        public Sounds soundtype;
        public AudioClip soundclip;
    }
    public enum Sounds
    {
        correctSound,
        incorrectSound
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Test
{
    public class TestSoundManager : MonoBehaviour
    {
        private TestSoundSO TestSoundSO;
        [SerializeField] private TestSoundSO USENGAudio;
        [SerializeField] private TestSoundSO HINDIAudio;
        [SerializeField] private TestSoundSO TAMILAudio;
        [SerializeField] private AudioSource levelAudio;
        //[SerializeField] private AudioSource numberAudio;
        [SerializeField] private AudioSource sfxAudio;
        [SerializeField] private string audioLocalization;
        [SerializeField] private SoundType[] audioClips;
        private bool playedIntro;


        private void Awake()
        {
            SetLanguage();
        }
        private void Start()
        {
           TestGameManager.Instance.OnLevelStart += () => playedIntro = false;
           TestGameManager.Instance.OnLevelStart += PlayLevelStartAudio;
        }
        private void SetLanguage()
        {
            audioLocalization = PlayerPrefs.GetString("PlaySchoolLanguage", audioLocalization);
            switch (audioLocalization)
            {
                case "English":
                    TestSoundSO = USENGAudio;
                    break;
                case "EnglishUS":
                    TestSoundSO = USENGAudio;
                    break;
                case "Hindi":
                    TestSoundSO = HINDIAudio;
                    break;
                case "Tamil":
                    TestSoundSO = TAMILAudio;
                    break;
                default:
                    TestSoundSO = USENGAudio;
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
            int currentLevelNumber =TestGameManager.Instance.LevelManager.GetCurrentQuestionIndex();
            if (currentLevelNumber == 0 && !playedIntro)
            {
                playedIntro = true;
                PlayLevelAudio(TestSoundSO.gameIntro);
            }
            else
            {               
                PlayLevelAudio(TestSoundSO.levelStartAudio[currentLevelNumber]);
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
        public void PlayCorrectAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, TestSoundSO.correctAudio.Length);
            PlayLevelAudio(TestSoundSO.correctAudio[random]);
        }
        public void PlayTimeUpAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, TestSoundSO.timeUpAudio.Length);
            PlayLevelAudio(TestSoundSO.timeUpAudio[random]);
        }
        public void PlayIncorrectAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, TestSoundSO.incorrectAudio.Length);
            PlayLevelAudio(TestSoundSO.incorrectAudio[random]);
        }
       
        public void PlayFinalAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayLevelAudio(TestSoundSO.gameOutro);
        }
        private void OnDestroy()
        {
           TestGameManager.Instance.OnLevelStart -= PlayLevelStartAudio;
           TestGameManager.Instance.OnLevelStart -= () => playedIntro = false;
        }
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


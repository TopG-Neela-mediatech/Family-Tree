using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Matching
{
    public class MatchingSoundManager : MonoBehaviour
    {
        private MatchingSoundSO MatchingSoundSO;
        [SerializeField] private MatchingSoundSO USENGAudio;
        [SerializeField] private MatchingSoundSO HINDIAudio;
        [SerializeField] private MatchingSoundSO TAMILAudio;
        [SerializeField] private AudioSource levelAudio;
        [SerializeField] private AudioSource numberAudio;
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
            GameManagerMatching.Instance.OnLevelStart += () => playedIntro = false;
            GameManagerMatching.Instance.OnLevelStart += PlayLevelStartAudio;
            GameManagerMatching.Instance.OnLevelWin += PlayLevelCompleteAudio;
        }
        private void SetLanguage()
        {
            audioLocalization = PlayerPrefs.GetString("PlaySchoolLanguage", audioLocalization);
            switch (audioLocalization)
            {
                case "English":
                    MatchingSoundSO = USENGAudio;
                    break;
                case "EnglishUS":
                    MatchingSoundSO = USENGAudio;
                    break;
                case "Hindi":
                    MatchingSoundSO = HINDIAudio;
                    break;
                case "Tamil":
                    MatchingSoundSO = TAMILAudio;
                    break;
                default:
                    MatchingSoundSO = USENGAudio;
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
            int currentLevelNumber = GameManagerMatching.Instance.LevelManager.GetCurrentLevelNumber();
            if (currentLevelNumber == 1 && !playedIntro)
            {
                playedIntro = true;
                PlayLevelAudio(MatchingSoundSO.gameIntro);
            }
            else
            {
                int random = UnityEngine.Random.Range(0, MatchingSoundSO.levelStartAudio.Length);
                PlayLevelAudio(MatchingSoundSO.levelStartAudio[random]);
            }
        }
        public void PlayCurrentNumberSound(int number)
        {
            if (numberAudio.isPlaying) { numberAudio.Stop(); }
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayNumberAudio(MatchingSoundSO.common1to10[number - 1]);
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
        private void PlayNumberAudio(AudioClip clip)
        {
            if (clip != null)
            {
                numberAudio.PlayOneShot(clip);
            }
            else
            {
                Debug.Log("Clip Not Found");
            }
        }
        private void PlayLevelCompleteAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, MatchingSoundSO.levelEndAudio.Length);
            PlayLevelAudio(MatchingSoundSO.levelEndAudio[random]);
        }
        public void PlayTimeUpAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, MatchingSoundSO.timeUpAudio.Length);
            PlayLevelAudio(MatchingSoundSO.timeUpAudio[random]);
        }
        public void PlayIncorrectAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, MatchingSoundSO.incorrectAudio.Length);
            PlayLevelAudio(MatchingSoundSO.incorrectAudio[random]);
        }
        public void PlayLevelLoseAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, MatchingSoundSO.levelLoseAudio.Length);
            PlayLevelAudio(MatchingSoundSO.levelLoseAudio[random]);
        }
        public void PlayFinalAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayLevelAudio(MatchingSoundSO.gameOutro);
        }
        private void OnDestroy()
        {
            GameManagerMatching.Instance.OnLevelStart -= PlayLevelStartAudio;
            GameManagerMatching.Instance.OnLevelStart -= () => playedIntro = false;
            GameManagerMatching.Instance.OnLevelWin -= PlayLevelCompleteAudio;
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
       swipeSound,
        levelComplete
    }
}


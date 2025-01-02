using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Sequence
{
    public class SoundManagerSequence : MonoBehaviour
    {
        private SequenceSoundSO TestSoundSO;
        [SerializeField] private SequenceSoundSO USENGAudio;
        [SerializeField] private SequenceSoundSO HINDIAudio;
        [SerializeField] private SequenceSoundSO TAMILAudio;
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
            GameManagerSequence.Instance.OnLevelStart += () => playedIntro = false;
            GameManagerSequence.Instance.OnLevelStart += PlayLevelStartAudio;
            GameManagerSequence.Instance.OnLevelWin += PlayLevelCompleteAudio;
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
            int currentLevelNumber = GameManagerSequence.Instance.LevelManager.GetCurrentLevelNumber();
            if (currentLevelNumber == 1 && !playedIntro)
            {
                playedIntro = true;
                PlayLevelAudio(TestSoundSO.gameIntro);
            }
            else
            {
                int random = UnityEngine.Random.Range(0, TestSoundSO.levelStartAudio.Length);
                PlayLevelAudio(TestSoundSO.levelStartAudio[random]);
            }
        }
        public void PlayCurrentNumberSound(int number)
        {
            if (numberAudio.isPlaying) { numberAudio.Stop(); }
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayNumberAudio(TestSoundSO.common1to10[number - 1]);
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
            int random = UnityEngine.Random.Range(0, TestSoundSO.levelEndAudio.Length);
            PlayLevelAudio(TestSoundSO.levelEndAudio[random]);
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
        public void PlayLevelLoseAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, TestSoundSO.levelLoseAudio.Length);
            PlayLevelAudio(TestSoundSO.levelLoseAudio[random]);
        }
        public void PlayFinalAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayLevelAudio(TestSoundSO.gameOutro);
        }
        private void OnDestroy()
        {
            GameManagerSequence.Instance.OnLevelStart -= PlayLevelStartAudio;
            GameManagerSequence.Instance.OnLevelStart -= () => playedIntro = false;
            GameManagerSequence.Instance.OnLevelWin -= PlayLevelCompleteAudio;
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
        numberPlacedSound,
        levelComplete
    }
}


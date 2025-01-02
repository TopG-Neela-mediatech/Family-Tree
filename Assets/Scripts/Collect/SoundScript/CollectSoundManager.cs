using System;
using UnityEngine;

namespace TMKOC.CountWithMe.Collect
{
    public class CollectSoundManager : MonoBehaviour
    {
        private CollectSoundSO CollectSoundSO;
        [SerializeField] private CollectSoundSO USENGAudio;
        [SerializeField] private CollectSoundSO HINDIAudio;
        [SerializeField] private CollectSoundSO TAMILAudio;
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
            GameManagerCollect.Instance.OnLevelStart += () => playedIntro = false;
            GameManagerCollect.Instance.OnLevelStart += PlayLevelStartAudio;
            GameManagerCollect.Instance.OnLevelWin += PlayLevelCompleteAudio;
        }
        private void SetLanguage()
        {
            audioLocalization = PlayerPrefs.GetString("PlaySchoolLanguage", audioLocalization);
            switch (audioLocalization)
            {
                case "English":
                    CollectSoundSO = USENGAudio;
                    break;
                case "EnglishUS":
                    CollectSoundSO = USENGAudio;
                    break;
                case "Hindi":
                    CollectSoundSO = HINDIAudio;
                    break;
                case "Tamil":
                    CollectSoundSO = TAMILAudio;
                    break;
                default:
                    CollectSoundSO = USENGAudio;
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
            int currentLevelNumber = GameManagerCollect.Instance.LevelManager.GetCurrentLevelNumber();
            if (currentLevelNumber == 1 && !playedIntro)
            {
                playedIntro = true;
                PlayLevelAudio(CollectSoundSO.gameIntro);
            }
            else
            {
                int random = UnityEngine.Random.Range(0, CollectSoundSO.levelStartAudio.Length);
                PlayLevelAudio(CollectSoundSO.levelStartAudio[random]);
            }
        }
        public void PlayCurrentNumberSound(int number)
        {
            if (numberAudio.isPlaying) { numberAudio.Stop(); }
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayNumberAudio(CollectSoundSO.common1to10[number - 1]);
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
            int random = UnityEngine.Random.Range(0, CollectSoundSO.levelEndAudio.Length);
            PlayLevelAudio(CollectSoundSO.levelEndAudio[random]);
        }
        public void PlayTimeUpAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, CollectSoundSO.timeUpAudio.Length);
            PlayLevelAudio(CollectSoundSO.timeUpAudio[random]);
        }
        public void PlayIncorrectAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, CollectSoundSO.incorrectAudio.Length);
            PlayLevelAudio(CollectSoundSO.incorrectAudio[random]);
        }
        public void PlayLevelLoseAudio()//common sound
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int random = UnityEngine.Random.Range(0, CollectSoundSO.levelLoseAudio.Length);
            PlayLevelAudio(CollectSoundSO.levelLoseAudio[random]);
        }
        public void PlayFinalAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayLevelAudio(CollectSoundSO.gameOutro);
        }
        private void OnDestroy()
        {
            GameManagerCollect.Instance.OnLevelStart -= PlayLevelStartAudio;
            GameManagerCollect.Instance.OnLevelStart -= () => playedIntro = false;
            GameManagerCollect.Instance.OnLevelWin -= PlayLevelCompleteAudio;
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
        numberPickSound,
        levelComplete
    }
}


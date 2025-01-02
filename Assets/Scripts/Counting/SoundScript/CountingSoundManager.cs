using UnityEngine;

namespace TMKOC.CountWithMe
{
    public class CountingSoundManager : MonoBehaviour
    {
        private CountingSoundSO countingSoundSO;
        [SerializeField] private CountingSoundSO USENGAudio;
        [SerializeField] private CountingSoundSO HINDIAudio;
        [SerializeField] private CountingSoundSO TAMILAudio;
        [SerializeField] private AudioSource levelAudio;
        [SerializeField] private AudioSource numberAudio;
        [SerializeField] private AudioSource soundMusic;
        [SerializeField] private string audioLocalization;
        private bool playedIntro;


        private void Awake()
        {
            SetLanguage();
        }
        private void Start()
        {
            GameManagerCounting.Instance.OnLevelStart += () => playedIntro = false;
            GameManagerCounting.Instance.OnLevelStart += PlayLevelStartAudio;
        }
        private void SetLanguage()
        {
            audioLocalization = PlayerPrefs.GetString("PlaySchoolLanguageAudio", audioLocalization);
            switch (audioLocalization)
            {
                case "English":
                    countingSoundSO = USENGAudio;
                    break;
                case "EnglishUS":
                    countingSoundSO = USENGAudio;
                    break;
                case "Hindi":
                    countingSoundSO = HINDIAudio;
                    break;
                case "Tamil":
                    countingSoundSO = TAMILAudio;
                    break;
                default:
                    countingSoundSO = USENGAudio;
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
        /*private void PlayNumberAudio(AudioClip clip)
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
        public void PlayLevelStartAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int currentLevelNumber = GameManagerCounting.Instance.CountingLevelManager.currentLevelNumber;
            if (currentLevelNumber == 0 && !playedIntro)
            {
                playedIntro = true;
                PlayLevelAudio(countingSoundSO.gameIntro);
            }
            else
            {
                PlayLevelAudio(countingSoundSO.levelStartAudio[currentLevelNumber]);
            }
        }

        public void PlayLevelCompleteAudio()//for level 10 play all level complete audio//transition audio
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int currentLevelNumber = GameManagerCounting.Instance.CountingLevelManager.currentLevelNumber;
            PlayLevelAudio(countingSoundSO.levelEndAudio[currentLevelNumber]);
        }
        public void PlayFinalAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayLevelAudio(countingSoundSO.gameOutro);
        }
        public void PlayCurrentNumberSound(int number)
        {
            if (numberAudio.isPlaying) { numberAudio.Stop(); }
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayNumberAudio(countingSoundSO.common1to10[number - 1]);
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
        private void OnDestroy()
        {
            GameManagerCounting.Instance.OnLevelStart -= PlayLevelStartAudio;
            GameManagerCounting.Instance.OnLevelStart -= () => playedIntro = false;
        }
    }
}

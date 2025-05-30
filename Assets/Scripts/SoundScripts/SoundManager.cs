using System;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource levelAudio;
        [SerializeField] private AudioSource quizAudioSource;
        [SerializeField] private SoundSO ENGUS;
        [SerializeField] private MemberSoundSO memberSO_ENGUS;
        [SerializeField] private SoundSO Hindi;
        [SerializeField] private MemberSoundSO memberSO_Hindi;
        [SerializeField] private SoundSO French;
        [SerializeField] private MemberSoundSO memberSO_French;
        [SerializeField] private SoundSO Tamil;
        [SerializeField] private MemberSoundSO memberSO_Tamil;
        [SerializeField] private SoundSO Marathi;
        [SerializeField] private MemberSoundSO memberSO_Marathi;
        [SerializeField] private SoundSO Bengali;
        [SerializeField] private MemberSoundSO memberSO_Bengali;
        [SerializeField] private string audioLocalization;
        private SoundSO levelSounds;
        private MemberSoundSO memberSound;
        private int questionIndex;


        private void Awake()
        {
            SetLanguage();
        }
        private void Start()
        {
            GameManager.Instance.OnLevelWin += PlayLevelCompleteAudio;
            GameManager.Instance.OnTreeComplete += PlayTreeCompleteAudio;
            GameManager.Instance.OnLevelStart += () => questionIndex = -1;
            GameManager.Instance.UIManager.OnMenuPressed += DisableAudioSources;
        }
        private void SetLanguage()
        {
            audioLocalization = PlayerPrefs.GetString("PlayschoolLanguageAudio", audioLocalization);
            switch (audioLocalization)
            {
                case "English":
                    levelSounds = ENGUS;
                    memberSound = memberSO_ENGUS;
                    break;
                case "EnglishUS":
                    levelSounds = ENGUS;
                    memberSound = memberSO_ENGUS;
                    break;
                case "Hindi":
                    levelSounds = Hindi;
                    memberSound = memberSO_Hindi;
                    break;
                case "Tamil":
                    levelSounds = Tamil;
                    memberSound = memberSO_Tamil;
                    break;
                case "French":
                    levelSounds = French;
                    memberSound = memberSO_French;
                    break;
                case "Marathi":
                    levelSounds = Marathi;
                    memberSound = memberSO_Marathi;
                    break;
                case "Bengali":
                    levelSounds = Bengali;
                    memberSound = memberSO_Bengali;
                    break;
                default:
                    levelSounds = ENGUS;
                    memberSound = memberSO_ENGUS;
                    break;
            }
        }
        private void DisableAudioSources()
        {
            levelAudio.Stop();
            quizAudioSource.Stop();
        }
        private void PlayLevelAudio(AudioClip clip)
        {
            if (clip != null)
            {
                levelAudio.PlayOneShot(clip);
            }
        }
        private void PlayQuizAudio(AudioClip clip)
        {
            if (clip != null)
            {
                quizAudioSource.PlayOneShot(clip);
            }
        }
        public void PlayQuestion()
        {
            questionIndex++;
            int currentlevel = GameManager.Instance.LevelManager.GetLevelIndex();
            AudioClip questionClip = levelSounds.levelsAudio[currentlevel].quizAudio[questionIndex].Question;
            if (quizAudioSource.isPlaying) { quizAudioSource.Stop(); }
            PlayQuizAudio(questionClip);
        }
        public void PlayCorrectAnswer()
        {
            int currentlevel = GameManager.Instance.LevelManager.GetLevelIndex();
            AudioClip correctAnswerClip = levelSounds.levelsAudio[currentlevel].quizAudio[questionIndex].correctAnswer;
            if (quizAudioSource.isPlaying) { quizAudioSource.Stop(); }
            PlayQuizAudio(correctAnswerClip);
        }
        public void PlayInCorrectAnswer()
        {
            int currentlevel = GameManager.Instance.LevelManager.GetLevelIndex();
            AudioClip incorrectAnswerClip = levelSounds.levelsAudio[currentlevel].quizAudio[questionIndex].incorrectAnswer;
            if (quizAudioSource.isPlaying) { quizAudioSource.Stop(); }
            PlayQuizAudio(incorrectAnswerClip);
        }
        public float PlayLevelStartAudio(int currentLevelNumber)
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            PlayLevelAudio(levelSounds.levelsAudio[currentLevelNumber].intro);
            float length = levelSounds.levelsAudio[currentLevelNumber].intro.length;
            return length;
        }
        private void PlayLevelCompleteAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int currentLevelNumber = GameManager.Instance.LevelManager.GetLevelIndex();
            Debug.Log(currentLevelNumber);
            PlayLevelAudio(levelSounds.levelsAudio[currentLevelNumber].outro);
        }
        private void PlayTreeCompleteAudio()
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            int currentLevelNumber = GameManager.Instance.LevelManager.GetLevelIndex();
            PlayLevelAudio(levelSounds.levelsAudio[currentLevelNumber].treeCompleteAudio);
        }
        public void PlayCurrentMemberSound(FamilyMember member)
        {
            if (levelAudio.isPlaying) { levelAudio.Stop(); }
            IndividualMemberAudio memberAudio = Array.Find(memberSound.memberAudio, i => i.memberIdentity == member);
            if (memberAudio != null)
            {
                PlayLevelAudio(memberAudio.memberClip);
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelWin += PlayLevelCompleteAudio;
            GameManager.Instance.OnTreeComplete += PlayTreeCompleteAudio;
            GameManager.Instance.OnLevelStart += () => questionIndex = -1;
            GameManager.Instance.UIManager.OnMenuPressed += DisableAudioSources;
        }
    }
}


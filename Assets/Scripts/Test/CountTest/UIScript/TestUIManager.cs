using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Test
{
    public class TestUIManager : MonoBehaviour
    {

        [SerializeField] private GameObject ScoreBar;
        [SerializeField] private TextMeshProUGUI ScoreNumberText;
        [SerializeField] private GameObject timerObject;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Button playSchoolBackButton;
        [SerializeField] private Button playSchoolBackButton2;
        [SerializeField] private Button restartTestButton;
        private int levelTime;
        private Coroutine timerCoroutine;


        private void Start()
        {
            TestGameManager.Instance.LevelManager.OnOptionSelected += ResetUIObjects;
            TestGameManager.Instance.OnLevelStart += StartLevelTimer;
            TestGameManager.Instance.LevelManager.OnOptionSelected += DisableTimer;
            TestGameManager.Instance.LevelManager.OnOptionSelected += SetScore;
            TestGameManager.Instance.OnGameEnd += EnableFinalWinPanel;
            TestGameManager.Instance.OnGameEnd += DisableTimer;
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            playSchoolBackButton2.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            restartTestButton.onClick.AddListener(TestGameManager.Instance.LevelManager.StartTest);
            ScoreNumberText.text = "0";
        }


        private void ResetTimerObject() => timerObject.transform.DOScale(0f, 0.5f);
        private void SetScore() => ScoreNumberText.text = "" + TestGameManager.Instance.LevelManager.score;
        private void UpdateTimerText(int remainingTIme) => timerText.text = "" + remainingTIme;


        private void EnableFinalWinPanel()
        {
#if PLAYSCHOOL_MAIN
                      EffectParticleControll.Instance.SpawnGameEndPanel();
                        GameOverEndPanel.Instance.AddTheListnerRetryGame(TestGameManager.Instance.LevelManager.StartTest);
#else
            AllEndPanel.Instance.PopUpEndPanel();
#endif
        }
        private void ResetUIObjects()
        {
            ResetTimerObject();
        }

        private void StartLevelTimer()
        {
            timerObject.transform.DOScale(1f, 1f).OnComplete(() =>
            {
                levelTime = 30;
                UpdateTimerText(levelTime);
                if (timerCoroutine != null)
                {
                    StopCoroutine(timerCoroutine);
                }
                timerCoroutine = StartCoroutine(UpdateLevelTimer());
            }
            );
        }
        private IEnumerator UpdateLevelTimer()
        {
            while (levelTime > 0)
            {
                yield return new WaitForSeconds(1f);
                levelTime -= 1;
                UpdateTimerText(levelTime);
            }
            if (levelTime <= 0)
            {
                TestGameManager.Instance.LevelManager.DeductFive();
                TestGameManager.Instance.LevelManager.EnableIncorrect();
                TestGameManager.Instance.LevelManager.InvokeOptionSelection();
                StartCoroutine(TestGameManager.Instance.LevelManager.InvokeLevelEndAfterDelay());
                TestGameManager.Instance.SoundManager.PlaySFX(Sounds.incorrectSound);
                TestGameManager.Instance.SoundManager.PlayTimeUpAudio();
            }
        }
        private void DisableTimer()
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
                timerText.text = null;
            }
            transform.DOScale(0f, 0.3f);
        }
        private void OnDestroy()
        {
            TestGameManager.Instance.LevelManager.OnOptionSelected -= ResetUIObjects;
            TestGameManager.Instance.OnLevelStart -= StartLevelTimer;
            TestGameManager.Instance.LevelManager.OnOptionSelected -= DisableTimer;
            TestGameManager.Instance.LevelManager.OnOptionSelected -= SetScore;
            TestGameManager.Instance.OnGameEnd -= EnableFinalWinPanel;
            TestGameManager.Instance.OnGameEnd -= DisableTimer;
        }
    }
}


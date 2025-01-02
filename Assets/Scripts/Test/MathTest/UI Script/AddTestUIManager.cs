using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.AddTest
{
    public class AddTestUIManager : MonoBehaviour
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
            AddTestGameManager.Instance.LevelManager.OnOptionSelected += ResetUIObjects;
            AddTestGameManager.Instance.OnLevelStart += StartLevelTimer;
            AddTestGameManager.Instance.LevelManager.OnOptionSelected += DisableTimer;
            AddTestGameManager.Instance.LevelManager.OnOptionSelected += SetScore;
            AddTestGameManager.Instance.OnGameEnd += EnableFinalWinPanel;
            AddTestGameManager.Instance.OnGameEnd += DisableTimer;
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            playSchoolBackButton2.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            restartTestButton.onClick.AddListener(AddTestGameManager.Instance.LevelManager.StartTest);
            ScoreNumberText.text = "0";
        }


        private void ResetTimerObject() => timerObject.transform.DOScale(0f, 0.5f);
        private void SetScore() => ScoreNumberText.text = "" + AddTestGameManager.Instance.LevelManager.score;
        private void UpdateTimerText(int remainingTIme) => timerText.text = "" + remainingTIme;


        private void EnableFinalWinPanel()
        {
#if PLAYSCHOOL_MAIN
                      EffectParticleControll.Instance.SpawnGameEndPanel();
                        GameOverEndPanel.Instance.AddTheListnerRetryGame(AddTestGameManager.Instance.LevelManager.StartTest);
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
                AddTestGameManager.Instance.LevelManager.DeductFive();
                AddTestGameManager.Instance.LevelManager.EnableIncorrect();
                AddTestGameManager.Instance.LevelManager.InvokeOptionSelection();
                StartCoroutine(AddTestGameManager.Instance.LevelManager.InvokeLevelEndAfterDelay());
                //AddTestGameManager.Instance.SoundManager.PlaySFX(Sounds.incorrectSound);
                //AddTestGameManager.Instance.SoundManager.PlayTimeUpAudio();
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
            AddTestGameManager.Instance.LevelManager.OnOptionSelected -= ResetUIObjects;
            AddTestGameManager.Instance.OnLevelStart -= StartLevelTimer;
            AddTestGameManager.Instance.LevelManager.OnOptionSelected -= DisableTimer;
            AddTestGameManager.Instance.LevelManager.OnOptionSelected -= SetScore;
            AddTestGameManager.Instance.OnGameEnd -= EnableFinalWinPanel;
            AddTestGameManager.Instance.OnGameEnd -= DisableTimer;
        }
    }
}

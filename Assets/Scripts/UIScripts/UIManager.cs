using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TMKOC.FamilyTree
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private GameObject infoBar;
        [SerializeField] private GameObject levelBar;
        [SerializeField] private TextMeshProUGUI levelNumberText;
        [SerializeField] private GameObject timerObject;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Button playSchoolBackButton;
        private int levelTime;
        private Coroutine timerCoroutine;



        private void Start()
        {
            GameManager.Instance.OnLevelWin += EnableWinPanel;
            GameManager.Instance.OnLevelLose += EnableLosePanel;
            GameManager.Instance.OnLevelStart += DisablePanels;
            GameManager.Instance.OnLevelStart += PlayUIStartAnimation;
            GameManager.Instance.OnLevelWin += ResetUIObjects;
            GameManager.Instance.OnLevelLose += ResetUIObjects;
            GameManager.Instance.OnLevelStart += StartLevelTimer;
            GameManager.Instance.OnLevelLose += DisableTimer;
            GameManager.Instance.OnLevelWin += DisableTimer;
            GameManager.Instance.OnGameEnd += EnableFinalWinPanel;
           // nextButton.onClick.AddListener(GameManager.Instance.LevelManager.LoadNextLevel);
            //restartButton.onClick.AddListener(GameManager.Instance.LevelManager.LoadLevel);
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
        }


        private void EnableWinPanel() => StartCoroutine(EnableWinPanelAfterDelay());
        private void EnableLosePanel() => StartCoroutine(EnableLosePanelAfterDelay());
        private void ResetLevelBar() => levelBar.transform.DOScale(0f, 0.5f);
        private void ResetTimerObject() => timerObject.transform.DOScale(0f, 0.5f);
      //  private void SetLevelNumber() => levelNumberText.text = "Level " + GameManager.Instance.LevelManager.GetCurrentLevelNumber();
        private void UpdateTimerText(int remainingTIme) => timerText.text = "" + remainingTIme;


        private void EnableFinalWinPanel()
        {
            winPanel.SetActive(false);
#if PLAYSCHOOL_MAIN
                    EffectParticleControll.Instance.SpawnGameEndPanel();
                    GameOverEndPanel.Instance.AddTheListnerRetryGame(GameManager.Instance.LevelManager.LoadNextLevel);
#else
            //Your testing End panel
            AllEndPanel.Instance.PopUpEndPanel();
#endif
        }
        private IEnumerator EnableLosePanelAfterDelay()
        {
            yield return new WaitForSeconds(0f);
            losePanel.SetActive(true);
        }
        private IEnumerator EnableWinPanelAfterDelay()
        {
            yield return new WaitForSeconds(2f);
            winPanel.SetActive(true);
        }
        private void PlayUIStartAnimation()
        {
            StartLevelBarAnimation();           
        }
        private void ResetUIObjects()
        {
            ResetLevelBar();
            ResetTimerObject();
        }
        private void StartLevelBarAnimation()
        {
           // SetLevelNumber();
            levelBar.gameObject.transform.DOScale(1f, 1f);
        }
        private void DisablePanels()
        {
            losePanel.SetActive(false);
            winPanel.SetActive(false);
        }
        private void StartLevelTimer()
        {
            timerObject.transform.DOScale(1f, 1f).OnComplete(() =>
            {
                levelTime = 60;
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
                GameManager.Instance.InvokeLevelLose();
                //GameManager.Instance.SoundManager.PlayTimeUpAudio();
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
            GameManager.Instance.OnLevelWin -= EnableWinPanel;
            GameManager.Instance.OnLevelLose -= EnableLosePanel;
            GameManager.Instance.OnLevelStart -= DisablePanels;
            GameManager.Instance.OnLevelStart -= PlayUIStartAnimation;
            GameManager.Instance.OnLevelWin -= ResetUIObjects;
            GameManager.Instance.OnLevelLose -= ResetUIObjects;
            GameManager.Instance.OnLevelStart -= StartLevelTimer;
            GameManager.Instance.OnLevelLose -= DisableTimer;
            GameManager.Instance.OnLevelWin -= DisableTimer;
            GameManager.Instance.OnGameEnd -= EnableFinalWinPanel;
        }
    }
}

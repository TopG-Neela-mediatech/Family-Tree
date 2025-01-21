using DG.Tweening;
using System;
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
        [SerializeField] private Button nextButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button playSchoolBackButton;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private Image fullTreeImage;
        [SerializeField] private GameObject SelectionScreenObject;
        public event Action OnFullTreeShown;


        private void Start()
        {
            GameManager.Instance.OnLevelWin += EnableWinPanel;
            GameManager.Instance.OnLevelStart += DisableUIPanels;
            GameManager.Instance.OnGameEnd += EnableFinalWinPanel;
            GameManager.Instance.OnTreeComplete += ShowFullTree;
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            nextButton.onClick.AddListener(GameManager.Instance.LevelManager.LoadNextLevel);
            restartButton.onClick.AddListener(GameManager.Instance.LevelManager.LoadNextLevel);
            fullTreeImage.enabled = false;
        }


        private void EnableWinPanel() => StartCoroutine(EnableWinPanelAfterDelay());
        private void ShowFullTree() => StartCoroutine(ShowFullTreeOnTreeComplete());


        private IEnumerator ShowFullTreeOnTreeComplete()
        {
            fullTreeImage.transform.DOLocalMoveY(Screen.height, 0f);
            fullTreeImage.enabled = true;
            fullTreeImage.transform.DOLocalMoveY(0f, 1f);
            yield return new WaitForSeconds(4f);
            fullTreeImage.transform.DOLocalMoveY(Screen.height, 1f).OnComplete(() =>
            {
                fullTreeImage.enabled = false;
                OnFullTreeShown?.Invoke();
            });
        }


        public void DisableSelectionScreen()=>SelectionScreenObject.SetActive(false);


        private IEnumerator ShowFullTreeBeforeLevelStart()
        {
            fullTreeImage.transform.DOLocalMoveY(Screen.height, 0f);
            fullTreeImage.enabled = true;
            fullTreeImage.transform.DOLocalMoveY(0f, 1f);
            yield return new WaitForSeconds(4f);
            fullTreeImage.transform.DOLocalMoveY(Screen.height, 1f).OnComplete(() =>
            {
                fullTreeImage.enabled = false;
                OnFullTreeShown?.Invoke();
            });
        }
        private void DisableUIPanels()
        {
            winPanel.SetActive(false);
            endPanel.SetActive(false);
        }
        private void EnableFinalWinPanel()
        {
            winPanel.SetActive(false);
#if PLAYSCHOOL_MAIN
                    EffectParticleControll.Instance.SpawnGameEndPanel();
                    GameOverEndPanel.Instance.AddTheListnerRetryGame(GameManager.Instance.LevelManager.LoadNextLevel);
#else
            //Your testing End panel
            endPanel.SetActive(true);
#endif
        }
        private IEnumerator EnableWinPanelAfterDelay()
        {
            yield return new WaitForSeconds(0f);
            winPanel.SetActive(true);
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelWin -= EnableWinPanel;
            GameManager.Instance.OnGameEnd -= EnableFinalWinPanel;
            GameManager.Instance.OnLevelStart -= DisableUIPanels;
            GameManager.Instance.OnTreeComplete -= ShowFullTree;
        }
    }
}




#region DogWater
//private int levelTime;
//private Coroutine timerCoroutine;
//[SerializeField] private GameObject timerObject;
//[SerializeField] private TextMeshProUGUI timerText; 
//[SerializeField] private GameObject infoBar;
//[SerializeField] private GameObject losePanel;
//[SerializeField] private Button restartButton;
//[SerializeField] private TextMeshProUGUI levelNumberText;
//[SerializeField] private GameObject levelBar;


//private void EnableLosePanel() => StartCoroutine(EnableLosePanelAfterDelay());
//private void ResetLevelBar() => levelBar.transform.DOScale(0f, 0.5f);
//private void ResetTimerObject() => timerObject.transform.DOScale(0f, 0.5f);
//private void UpdateTimerText(int remainingTIme) => timerText.text = "" + remainingTIme;
//private void SetLevelNumber() => levelNumberText.text = "Level " + GameManager.Instance.LevelManager.GetCurrentLevelNumber();


/*          
           StartMethod
            GameManager.Instance.OnLevelStart += DisablePanels;
            GameManager.Instance.OnLevelStart += PlayUIStartAnimation;
            GameManager.Instance.OnLevelWin += ResetUIObjects;
            nextButton.onClick.AddListener(GameManager.Instance.LevelManager.LoadNextLevel);
            restartButton.onClick.AddListener(GameManager.Instance.LevelManager.LoadLevel); 
            GameManager.Instance.OnLevelStart += StartLevelTimer;         
            GameManager.Instance.OnLevelWin += DisableTimer;
           */


/* OnDestroy
 * //GameManager.Instance.OnLevelStart -= PlayUIStartAnimation;
//GameManager.Instance.OnLevelWin -= ResetUIObjects;
//GameManager.Instance.OnLevelStart -= StartLevelTimer; 
//GameManager.Instance.OnLevelStart -= DisablePanels; */


/*private IEnumerator EnableLosePanelAfterDelay()
       {
           yield return new WaitForSeconds(0f);
           losePanel.SetActive(true);
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
        }*/
/* private void StartLevelTimer()
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
        }*/
#endregion
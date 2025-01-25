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
        [SerializeField] private Button menuButton1;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private Image fullTreeImage;
        [SerializeField] private Image highLightedTreeImage;
        [SerializeField] private Sprite[] treeSpritesIndividual;
        [SerializeField] private GameObject SelectionScreenObject;
        [SerializeField] private TextMeshProUGUI winScreenText;
        [SerializeField] private Button enableMenuFullTreeButton;
        [SerializeField] private Button disableMenuFullTreeButton;
        [SerializeField] private GameObject menuFullTree;
        public event Action OnFullTreeShown;
        public event Action OnMenuPressed;


        private void Start()
        {
            GameManager.Instance.OnLevelWin += EnableWinPanel;
            GameManager.Instance.OnLevelStart += DisableUIPanels;
            GameManager.Instance.OnLevelStart += DisableMenuTree;
            GameManager.Instance.OnGameEnd += EnableFinalWinPanel;
            GameManager.Instance.OnTreeComplete += ShowFullTree;
            LevelButtonManager.OnLevelButtonPressed += () => enableMenuFullTreeButton.enabled = false;
            OnMenuPressed += () => enableMenuFullTreeButton.enabled = true;
            playSchoolBackButton.onClick.AddListener(() => SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            nextButton.onClick.AddListener(() => LoadNextLevel(nextButton));
            restartButton.onClick.AddListener(() => LoadNextLevel(restartButton));
            fullTreeImage.enabled = false;
            menuButton1.onClick.AddListener(EnableSelectionScreen);
            OnMenuPressed += () => fullTreeImage.enabled = false;
            enableMenuFullTreeButton.onClick.AddListener(EnableMenuTree);
            disableMenuFullTreeButton.onClick.AddListener(DisableMenuTree);
            DisableMenuTree();
        }


        private void EnableWinPanel() => StartCoroutine(EnableWinPanelAfterDelay());
        private void ShowFullTree() => StartCoroutine(ShowFullTreeOnTreeComplete());
        public void DisableSelectionScreen() => StartCoroutine(DisableSelectionScreenAfterDelay());
        private void EnableMenuTree() => menuFullTree.SetActive(true);
        private void DisableMenuTree() => menuFullTree.SetActive(false);


        private IEnumerator ShowFullTreeOnTreeComplete()
        {
            fullTreeImage.transform.DOLocalMoveY(Screen.height, 0f).OnComplete(() =>
            {
                fullTreeImage.enabled = true;
                highLightedTreeImage.enabled = true;
                SetHighLightedImage();
                fullTreeImage.transform.DOLocalMoveY(0f, 1f);
            });
            yield return new WaitForSeconds(4f);
            fullTreeImage.transform.DOLocalMoveY(Screen.height, 1f).OnComplete(() =>
            {
                if (fullTreeImage.isActiveAndEnabled)
                {
                    OnFullTreeShown?.Invoke();
                }
                fullTreeImage.enabled = false;
            });
        }
        private void LoadNextLevel(Button bt)
        {
            bt.enabled = false;
            GameManager.Instance.LevelManager.LoadNextLevel();
        }
        private void SetHighLightedImage()
        {
            int levelIndex = GameManager.Instance.LevelManager.GetLevelIndex();
            SetHighLightedSprite(levelIndex);
        }
        private void SetHighLightedSprite(int levelIndex)
        {
            switch (levelIndex)
            {
                case 0:
                    highLightedTreeImage.sprite = treeSpritesIndividual[0];
                    break;
                case 1:
                    highLightedTreeImage.sprite = treeSpritesIndividual[1];
                    break;
                case 2:
                    highLightedTreeImage.sprite = treeSpritesIndividual[2];
                    break;
                case 3:
                    highLightedTreeImage.sprite = treeSpritesIndividual[3];
                    break;
                case 4:
                    highLightedTreeImage.sprite = treeSpritesIndividual[4];
                    break;
                default:
                    Debug.Log("Sprite Not Found");
                    break;
            }
        }
        private IEnumerator DisableSelectionScreenAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            SelectionScreenObject.SetActive(false);
            playSchoolBackButton.gameObject.SetActive(false);
            menuButton1.gameObject.SetActive(true);
        }
        public void EnableSelectionScreen()
        {
            OnMenuPressed?.Invoke();
            SelectionScreenObject.SetActive(true);
            playSchoolBackButton.gameObject.SetActive(true);
            menuButton1.gameObject.SetActive(false);
            GameManager.Instance.LevelManager.DisableLevel();
            DisableUIPanels();
        }
        private void SetWinPanelText()
        {
            int levelNumber = GameManager.Instance.LevelManager.GetLevelIndex();
            levelNumber--;
            switch (levelNumber)
            {
                case 0:
                    winScreenText.text = "Parent Tree Completed";
                    break;
                case 1:
                    winScreenText.text = "Father's Extended Tree Completed";
                    break;
                case 2:
                    winScreenText.text = "Mother's Extended Tree Completed";
                    break;
                case 3:
                    winScreenText.text = "Father's Parents Tree Completed";
                    break;
                case 4:
                    winScreenText.text = "Mother's Parents Tree Completed";
                    break;
                default:
                    winScreenText.text = "Tree Completed";
                    break;
            }
        }
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
            restartButton.enabled = true;
#endif
        }
        private IEnumerator EnableWinPanelAfterDelay()
        {
            yield return new WaitForSeconds(0.25f);
            SetWinPanelText();
            winPanel.SetActive(true);
            nextButton.enabled = true;
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelWin -= EnableWinPanel;
            GameManager.Instance.OnGameEnd -= EnableFinalWinPanel;
            GameManager.Instance.OnLevelStart -= DisableUIPanels;
            GameManager.Instance.OnTreeComplete -= ShowFullTree;
            GameManager.Instance.OnLevelStart -= DisableMenuTree;
            OnMenuPressed -= () => fullTreeImage.enabled = false;
            LevelButtonManager.OnLevelButtonPressed -= () => enableMenuFullTreeButton.enabled = false;
            OnMenuPressed -= () => enableMenuFullTreeButton.enabled = true;
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
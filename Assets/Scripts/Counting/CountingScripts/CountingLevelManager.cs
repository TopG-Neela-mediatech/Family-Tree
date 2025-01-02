using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe
{
    public class CountingLevelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectText;
        [SerializeField] private Sprite[] numberSprites;
        [SerializeField] private Image numberImage;
        [SerializeField] private CountingLevelSO countingLevelSO;
        [SerializeField] private GameObject[] countingPrefab;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private GameObject puffPrefab;
        [SerializeField] private GameObject puffPrefab2;
        [SerializeField] private GameObject conffetiPrefab;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManager updateCategoryApiManager;
        public int gameID;
        bool calledOnce;


        public int currentLevelNumber { get; private set; }
        public event Action OnSpawningFinished;

        private void Awake()
        {
#if PLAYSCHOOL_MAIN
             // assign varaible in this to get the  game ID from main app
             gameID  = PlayerPrefs.GetInt("currentGameId");
#endif
            gameCategoryDataManager = new GameCategoryDataManager(gameID, "countwithme");
            updateCategoryApiManager = new UpdateCategoryApiManager(gameID);
        }
        private void Start()
        {
            GameManagerCounting.Instance.OnLevelEnd += (() => StartCoroutine(DeScaleTextAndNumberAfterDelay()));
            GameManagerCounting.Instance.OnGameEnd += (() => StartCoroutine(DeScaleTextAndNumberAfterDelay()));
            GameManagerCounting.Instance.OnLevelEnd += (() => StartCoroutine(LevelEndEffectAfterDelay()));
            GameManagerCounting.Instance.OnGameEnd += (() => StartCoroutine(LevelEndEffectAfterDelay()));
            GameManagerCounting.Instance.OnLevelStart += (() => EnablePuffEffect(false));
            GameManagerCounting.Instance.OnLevelStart += (() => calledOnce = false);
            GameManagerCounting.Instance.OnLevelStart += (() => EnableConfetti(false));
            currentLevelNumber = gameCategoryDataManager.GetCompletedLevel;
            LoadLevel();
            AssignButtons();
        }
        public void EnableConfetti(bool enabled) => conffetiPrefab.SetActive(enabled);
        private void CheckAndResetToLevelOne()
        {
            if (currentLevelNumber == countingLevelSO.countingData.Length - 1)
            {
                currentLevelNumber = 0;
                gameCategoryDataManager.SaveLevel(currentLevelNumber, 10);
            }
        }
        private void AssignButtons()
        {
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        }
        private IEnumerator LevelEndEffectAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            EnablePuffEffect(true);
        }
        private void EnablePuffEffect(bool status)
        {
            puffPrefab.SetActive(status);
            puffPrefab2.SetActive(status);
        }
        private void LoadLevel()
        {
            gameCategoryDataManager.SaveLevel(currentLevelNumber, 10);
            SendStars();
            GameManagerCounting.Instance.InvokeLevelStart();
            SetTextToDisplay(currentLevelNumber);
            SetGridSize();
            StartCoroutine(SpawnObjectWithDelay(0.75f, currentLevelNumber));
            SetNumberToDisplay(currentLevelNumber);
        }
        private void SendStars()
        {
            int star = gameCategoryDataManager.GetLoadedstar;
            if (star >= 5)
            {
                updateCategoryApiManager.SetGameDataMore(10, 10, 0, 5);
            }
            else
            {
                updateCategoryApiManager.SetGameDataMore(currentLevelNumber, 10, 0, star);
            }
        }
        private IEnumerator SpawnObjectWithDelay(float delay, int levelNumber)
        {
            yield return new WaitForSeconds(1f);//delay spawing here
            SetDataInObject(countingPrefab[0], levelNumber);
             GameManagerCounting.Instance.CountingSoundManager.PlayCurrentNumberSound(1);
            for (int i = 1; i <= currentLevelNumber; i++)
            {
                yield return new WaitForSeconds(delay);
                GameManagerCounting.Instance.CountingSoundManager.PlayCurrentNumberSound(i+1);
                SetDataInObject(countingPrefab[i], levelNumber);
            }
            yield return new WaitForSeconds(delay);
            OnSpawningFinished?.Invoke();
        }
        private void SetDataInObject(GameObject go, int num)
        {
            if (go != null)
            {
                Image image = go.GetComponent<Image>();
                image.sprite = countingLevelSO.countingData[num].objectSprite;
                go.SetActive(true);
                go.transform.localScale = Vector3.zero;
                go.transform.DOScale(1f, 0.3f).OnComplete(() =>
                {
                    go.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);
                });
            }
        }
        private void SetGridSize()
        {
            float cellSize = countingLevelSO.countingData[currentLevelNumber].cellSize;
            gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        }
        private void SetNumberToDisplay(int num)
        {
            numberImage.sprite = numberSprites[num];
            numberImage.transform.localScale = Vector3.zero;
            numberImage.gameObject.SetActive(true);
            numberImage.transform.DOScale(1f, 0.5f);
        }
        private void SetTextToDisplay(int num)
        {
            string objectName = countingLevelSO.countingData[num].objectName;
            objectText.text = objectName;
            objectText.transform.localScale = Vector3.zero;
            objectText.gameObject.SetActive(true);
            objectText.transform.DOScale(1f, 0.5f);
        }
        private void ScaleUpTextAndNumber()
        {
            objectText.transform.DOScale(1.3f, 0.7f).OnComplete(() => objectText.transform.DOScale(1f, 0.3f));
            numberImage.transform.DOScale(1.3f, 0.7f).OnComplete(() => numberImage.transform.DOScale(1f, 0.3f));
        }
        private IEnumerator DeScaleTextAndNumberAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            objectText.transform.DOScale(0f, 1.5f);
            numberImage.transform.DOScale(0f, 1.5f);
        }
        private void DisableAllObject()
        {
            foreach (var go in countingPrefab)
            {
                go.SetActive(false);
            }
        }
        private void LoadNextLevel()
        {
            if (!calledOnce)
            {
                DisableNextLevelButton();
                calledOnce = true;
                StartCoroutine(LoadNextLevelDelay());
            }
        }
        private IEnumerator LoadNextLevelDelay()
        {
            GameManagerCounting.Instance.InvokeLevelEnd();
            GameManagerCounting.Instance.CountingSoundManager.PlayLevelCompleteAudio();
            yield return new WaitForSeconds(3f);
            currentLevelNumber++;
            LoadLevel();
        }
        private void DisableNextLevelButton()
        {
            nextLevelButton.enabled = false;
            nextLevelButton.gameObject.SetActive(false);
        }
        public void EnableNextLevelButton()
        {
            if (currentLevelNumber != countingLevelSO.countingData.Length - 1)
            {
                StartCoroutine(EnableNextLevelButtonAfterDelay());
            }
            else
            {
                StartWinPanel();
            }
        }
        private IEnumerator EnableNextLevelButtonAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            PlayLevelEndEffectandSound();
            yield return new WaitForSeconds(2f);
            nextLevelButton.gameObject.SetActive(true);
            nextLevelButton.enabled = true;
        }
        private void PlayLevelEndEffectandSound()//add 0.5 delay
        {
            ScaleUpTextAndNumber();
            EnableConfetti(true);
            GameManagerCounting.Instance.CountingSoundManager.PlayLevelStartAudio();
        }
        private void StartWinPanel()
        {
            StartCoroutine(EnableEndPanelAfterDelay());
        }
        private IEnumerator EnableEndPanelAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            PlayLevelEndEffectandSound();
            yield return new WaitForSeconds(1f);
            GameManagerCounting.Instance.InvokeGameEnd();
            yield return new WaitForSeconds(3f);
            CheckAndResetToLevelOne();
            GameManagerCounting.Instance.CountingSoundManager.PlayFinalAudio();
#if PLAYSCHOOL_MAIN
                    EffectParticleControll.Instance.SpawnGameEndPanel();
                    GameOverEndPanel.Instance.AddTheListnerRetryGame(CheckAndResetToLevelOne());
#else
            //Your testing End panel
            AllEndPanel.Instance.PopUpEndPanel();
#endif              
        }
        public void LoadLevelOne()
        {
            AllEndPanel.Instance.EndPanelOff();
            LoadLevel();
        }
        private void OnDestroy()
        {
            GameManagerCounting.Instance.OnLevelEnd -= (() => StartCoroutine(DeScaleTextAndNumberAfterDelay()));
            GameManagerCounting.Instance.OnGameEnd -= (() => StartCoroutine(DeScaleTextAndNumberAfterDelay()));
            GameManagerCounting.Instance.OnLevelEnd -= (() => StartCoroutine(LevelEndEffectAfterDelay()));
            GameManagerCounting.Instance.OnGameEnd -= (() => StartCoroutine(LevelEndEffectAfterDelay()));
            GameManagerCounting.Instance.OnLevelStart -= (() => EnablePuffEffect(false));
            GameManagerCounting.Instance.OnLevelStart -= (() => calledOnce = false);
            GameManagerCounting.Instance.OnLevelStart -= (() => EnableConfetti(false));
        }
    }
}


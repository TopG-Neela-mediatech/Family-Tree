using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.AddTest
{
    public class AddTestLevelManager : MonoBehaviour
    {
        [SerializeField] private Image[] questionImages;
        [SerializeField] private Image symbolImage;
        [SerializeField] private Image equalImage;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private Button[] optionButtons;
        [SerializeField] private Transform questionParentTransform;
        [SerializeField] private Transform ButtonparentTransform;
        [SerializeField] private AddTestSO testLevels;
        [SerializeField] private Sprite emptyBoxSprite;
        [SerializeField] private List<GameObject> optionButtonList = new List<GameObject>();
        [SerializeField] private GameObject boardCleaningEffect;
        [SerializeField] private string gameNameString;
        [SerializeField] private AttemptCounterAdd[] attemptCounters;
        [SerializeField] private Transform attemptParentTransform;
        private int correctOption;
        private int currentQuestionIndex;
        private GameCategoryDataManager gameCategoryDataManager;
        private UpdateCategoryApiManagerTest _updateCategoryApiManagerTest;
        public int gameID;
        private int correctCount;
        public event Action OnOptionSelected;


        public int score { get; private set; }
        public int GetCurrentQuestionIndex() => currentQuestionIndex;
        public void MoveObjectDown() => attemptParentTransform.DOLocalMoveY(-25f, 0.5f);
        public void InvokeOptionSelection() => OnOptionSelected?.Invoke();


        private void Awake()
        {
            #region GameID
#if PLAYSCHOOL_MAIN
             // assign varaible in this to get the  game ID from main app
             gameID  = PlayerPrefs.GetInt("currentGameId");
#endif
            #endregion
            gameCategoryDataManager = new GameCategoryDataManager(gameID, PlayerPrefs.GetString("currentGameName", gameNameString));
            _updateCategoryApiManagerTest = new UpdateCategoryApiManagerTest(PlayerPrefs.GetInt("currentTestId"));
            questionParentTransform.DOLocalMoveX(Screen.width, 0f);
            ButtonparentTransform.DOLocalMoveX(-Screen.width, 0f);
        }
        private void Start()
        {
            AddTestGameManager.Instance.OnLevelEnd += LoadNextQuestionAfterDelay;
            AddTestGameManager.Instance.OnLevelStart += () => boardCleaningEffect.SetActive(false);
            OnOptionSelected += AnimateOnButtonClick;
            AddTestGameManager.Instance.OnLevelStart += AnimateOnLevelStart;
            AddTestGameManager.Instance.OnGameEnd += SendTestData;
            AddTestGameManager.Instance.OnGameEnd += () => boardCleaningEffect.SetActive(true);
            //AddTestGameManager.Instance.OnLevelStart += MoveObjectDown;
            // AddTestGameManager.Instance.OnLevelEnd += MoveObjectUp;
            StartTest();
        }
        public void StartTest()
        {
            AllEndPanel.Instance.EndPanelOff();
            MoveObjectDown();
            score = 0;
            currentQuestionIndex = 0;
            correctCount = 0;
            LoadLevel();
        }
        private void AnimateOnButtonClick()
        {
            questionParentTransform.DOLocalMoveX(Screen.width, 1f);
            ButtonparentTransform.DOLocalMoveX(-Screen.width, 1f);
            questionText.transform.DOScale(0f, 0.75f);
            boardCleaningEffect.SetActive(true);
        }
        private void AnimateOnLevelStart()
        {
            questionParentTransform.DOLocalMoveX(0f, 1f);
            ButtonparentTransform.DOLocalMoveX(0f, 1f);
            questionText.transform.DOScale(1f, 1f);
        }
        private void LoadNextQuestionAfterDelay()
        {
            boardCleaningEffect.SetActive(true);
            StartCoroutine(LoadNextQuestion());
        }
        private IEnumerator LoadNextQuestion()
        {
            yield return new WaitForSeconds(0f);
            currentQuestionIndex++;
            LoadLevel();
        }
        private void LoadLevel()
        {
            DisableAllQuestionImages();
            int random = UnityEngine.Random.Range(0, testLevels.outerLevels[currentQuestionIndex].innerLevels.Length);
            questionImages[0].sprite = testLevels.outerLevels[currentQuestionIndex].innerLevels[random].numbersToBeAdded[0].numberSprite;
            questionImages[1].sprite = testLevels.outerLevels[currentQuestionIndex].innerLevels[random].numbersToBeAdded[1].numberSprite;
            questionImages[0].gameObject.SetActive(true);
            questionImages[1].gameObject.SetActive(true);
            correctOption = testLevels.outerLevels[currentQuestionIndex].innerLevels[random].correctAnswer;
            for (int i = 0; i < optionButtons.Length; i++)//loop setting options
            {
                optionButtons[i].GetComponent<ButtonValueAdd>().buttonValue = testLevels.outerLevels[currentQuestionIndex].innerLevels[random].optionData[i].value;
                Button currentButton = optionButtons[i];
                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(() => CheckIfCorrect(currentButton));
                currentButton.image.sprite = testLevels.outerLevels[currentQuestionIndex].innerLevels[random].optionData[i].numberSprite;
            }
            questionText.text = "Tap The Correct Answer";
            ShuffleOptions();
            AddTestGameManager.Instance.InvokeLevelStart();
        }
        private void DisableAllQuestionImages()
        {
            foreach (Image image in questionImages)
            {
                image.gameObject.SetActive(false);
            }
        }
        public void DeductFive()
        {
            score -= 5;
            if (score < 0)
            {
                score = 0;
            }
        }
        private void CheckIfCorrect(Button button)
        {
            int optionSelected = button.GetComponent<ButtonValueAdd>().buttonValue;

            if (optionSelected == correctOption)
            {
                score += 20;
                attemptCounters[currentQuestionIndex].EnableCorrect();
                correctCount++;
                //AddTestGameManager.Instance.SoundManager.PlaySFX(Sounds.correctSound);
                //AddTestGameManager.Instance.SoundManager.PlayCorrectAudio();
            }
            else
            {
                DeductFive();
                EnableIncorrect();
                //AddTestGameManager.Instance.SoundManager.PlaySFX(Sounds.incorrectSound);
                //AddTestGameManager.Instance.SoundManager.PlayIncorrectAudio();
            }
            OnOptionSelected?.Invoke();
            StartCoroutine(InvokeLevelEndAfterDelay());
        }
        public IEnumerator InvokeLevelEndAfterDelay()
        {
            yield return new WaitForSeconds(1.8f);//change this for level transition time
            if (currentQuestionIndex < 4)
            {
                AddTestGameManager.Instance.InvokeLevelEnd();
                yield break;
            }
            else
            {
                AddTestGameManager.Instance.InvokeGameEnd();
                //AddTestGameManager.Instance.SoundManager.PlayFinalAudio();
                yield break;
            }
        }
        public void EnableIncorrect()
        {
            attemptCounters[currentQuestionIndex].EnableIncorrect();
        }
        private void SendTestData()
        {
            gameCategoryDataManager.SaveTestStar(correctCount, 5); ;
            int testStar = gameCategoryDataManager.GetTeststar;
            _updateCategoryApiManagerTest.SetGameDataMore(score, 5, testStar);
        }
        private void ShuffleOptions()
        {
            Transform parent = optionButtonList[0].transform.parent;
            ShuffleList(optionButtonList);
            for (int i = 0; i < optionButtonList.Count; i++)
            {
                optionButtonList[i].transform.SetParent(parent);
                optionButtonList[i].transform.SetSiblingIndex(i);
            }
        }
        private void ShuffleList(List<GameObject> list)
        {
            int n = list.Count;
            System.Random rng = new System.Random();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                GameObject value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void OnDestroy()
        {
            AddTestGameManager.Instance.OnLevelEnd -= LoadNextQuestionAfterDelay;
            AddTestGameManager.Instance.OnLevelStart -= () => boardCleaningEffect.SetActive(false);
            OnOptionSelected -= AnimateOnButtonClick;
            AddTestGameManager.Instance.OnLevelStart -= AnimateOnLevelStart;
            AddTestGameManager.Instance.OnGameEnd -= SendTestData;
            AddTestGameManager.Instance.OnGameEnd -= () => boardCleaningEffect.SetActive(true);
            //AddTestGameManager.Instance.OnLevelStart -= MoveObjectDown;
            //AddTestGameManager.Instance.OnLevelEnd -= MoveObjectUp;
        }
    }
}

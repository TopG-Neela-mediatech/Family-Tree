using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.CountWithMe.Test
{
    public class TestLevelManager : MonoBehaviour
    {
        [SerializeField] private Image[] questionImages;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private Button[] optionButtons;
        [SerializeField] private Transform questionParentTransform;
        [SerializeField] private Transform ButtonparentTransform;
        [SerializeField] private TestLevelSO testLevels;
        [SerializeField] private Sprite emptyBoxSprite;
        [SerializeField] private List<GameObject> optionButtonList = new List<GameObject>();
        [SerializeField] private GameObject boardCleaningEffect;
        [SerializeField] private string gameNameString;
        [SerializeField] private AttemptCounter[] attemptCounters;
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
        private void MoveObjectUp() => attemptParentTransform.DOLocalMoveY(150f, 0f);
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
            TestGameManager.Instance.OnLevelEnd += LoadNextQuestionAfterDelay;
            TestGameManager.Instance.OnLevelStart += () => boardCleaningEffect.SetActive(false);
            OnOptionSelected += AnimateOnButtonClick;
            TestGameManager.Instance.OnLevelStart += AnimateOnLevelStart;
            TestGameManager.Instance.OnGameEnd += SendTestData;
            TestGameManager.Instance.OnGameEnd += () => boardCleaningEffect.SetActive(true);
            //TestGameManager.Instance.OnLevelStart += MoveObjectDown;
            // TestGameManager.Instance.OnLevelEnd += MoveObjectUp;
            StartTest();
        }
        public void StartTest()
        {
            AllEndPanel.Instance.EndPanelOff();
            MoveObjectDown();
            score = 0;
            currentQuestionIndex = 0;
            correctCount = 0;
            SetFirstQuestion();
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
        private void SetFirstQuestion()
        {
            DisableAllQuestionImages();
            for (int i = 0; i < optionButtons.Length; i++)//loop setting the options
            {
                Button currentButton = optionButtons[i];
                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(() => CheckIfCorrect(currentButton));
                currentButton.image.sprite = testLevels.level1Data.optionData[i].optionsSprite;
                ButtonValue buttonValue = currentButton.GetComponent<ButtonValue>();
                buttonValue.buttonValue = testLevels.level1Data.optionData[i].optionNumber;
            }
            int random = UnityEngine.Random.Range(0, testLevels.level1Data.correctNumber.Length);
            correctOption = testLevels.level1Data.correctNumber[random];
            int random2 = UnityEngine.Random.Range(0, testLevels.level1Data.correctNumber.Length);
            for (int i = 0; i < correctOption; i++)//setting the question images
            {
                questionImages[i].gameObject.SetActive(true);
                questionImages[i].sprite = testLevels.level1Data.questionData[random2].objectImage;
            }
            questionText.text = "How Many " + testLevels.level1Data.questionData[random2].objectName + "s Are There?";
            ShuffleOptions();
            TestGameManager.Instance.InvokeLevelStart();
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
            switch (currentQuestionIndex)
            {
                case 1:
                    SetSecondQuestion();
                    TestGameManager.Instance.InvokeLevelStart();
                    break;
                case 2:
                    SetThirdQuestion();
                    TestGameManager.Instance.InvokeLevelStart();
                    break;
                case 3:
                    SetFourthQuestion();
                    TestGameManager.Instance.InvokeLevelStart();
                    break;
                case 4:
                    SetFifthQuestion();
                    TestGameManager.Instance.InvokeLevelStart();
                    break;
                case 5:
                    TestGameManager.Instance.InvokeGameEnd();
                    break;
                default:
                    SetFirstQuestion();
                    break;
            }
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
            int optionSelected = button.GetComponent<ButtonValue>().buttonValue;

            if (optionSelected == correctOption)
            {
                score += 20;
                attemptCounters[currentQuestionIndex].EnableCorrect();
                correctCount++;
                TestGameManager.Instance.SoundManager.PlaySFX(Sounds.correctSound);
                TestGameManager.Instance.SoundManager.PlayCorrectAudio();
            }
            else
            {
                DeductFive();
                EnableIncorrect();
                TestGameManager.Instance.SoundManager.PlaySFX(Sounds.incorrectSound);
                TestGameManager.Instance.SoundManager.PlayIncorrectAudio();
            }
            OnOptionSelected?.Invoke();
            StartCoroutine(InvokeLevelEndAfterDelay());
        }
        public IEnumerator InvokeLevelEndAfterDelay()
        {
            yield return new WaitForSeconds(3f);//change this for level transition time
            if (currentQuestionIndex < 4)
            {
                TestGameManager.Instance.InvokeLevelEnd();
                yield break;
            }
            else
            {
                TestGameManager.Instance.InvokeGameEnd();
                TestGameManager.Instance.SoundManager.PlayFinalAudio();
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
        private void SetSecondQuestion()
        {
            DisableAllQuestionImages();
            for (int i = 0; i < testLevels.level2Data.questionSprite.Length; i++)
            {
                questionImages[i].sprite = testLevels.level2Data.questionSprite[i];
                questionImages[i].gameObject.SetActive(true);
            }

            int random = UnityEngine.Random.Range(0, 3);
            correctOption = testLevels.level2Data.optionData[random].optionNumber;
            questionImages[correctOption - 1].sprite = emptyBoxSprite;

            for (int i = 0; i < optionButtons.Length; i++)//loop setting the options
            {
                Button currentButton = optionButtons[i];
                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(() => CheckIfCorrect(currentButton));
                currentButton.image.sprite = testLevels.level2Data.optionData[i].optionsSprite;
                ButtonValue buttonValue = currentButton.GetComponent<ButtonValue>();
                buttonValue.buttonValue = testLevels.level2Data.optionData[i].optionNumber;
            }
            questionText.text = "Tap The Correct Number To Complete The Sequence";
            ShuffleOptions();
        }
        private void SetThirdQuestion()
        {
            DisableAllQuestionImages();
            for (int i = 0; i < testLevels.level3Data.questionSprite.Length; i++)
            {
                questionImages[i].sprite = testLevels.level3Data.questionSprite[i];
                questionImages[i].gameObject.SetActive(true);
            }

            int random = UnityEngine.Random.Range(0, 3);
            correctOption = testLevels.level3Data.optionData[random].optionNumber;
            questionImages[correctOption - 5].sprite = emptyBoxSprite;//-5 because 0 based

            for (int i = 0; i < optionButtons.Length; i++)//loop setting the options
            {
                Button currentButton = optionButtons[i];
                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(() => CheckIfCorrect(currentButton));
                currentButton.image.sprite = testLevels.level3Data.optionData[i].optionsSprite;
                ButtonValue buttonValue = currentButton.GetComponent<ButtonValue>();
                buttonValue.buttonValue = testLevels.level3Data.optionData[i].optionNumber;
            }
            questionText.text = "Tap The Correct Number To Complete The Sequence";
            ShuffleOptions();
        }
        private void SetFourthQuestion()
        {
            DisableAllQuestionImages();
            for (int i = 0; i < testLevels.level4Data.questionSprite.Length; i++)
            {
                questionImages[i].sprite = testLevels.level4Data.questionSprite[i];
                questionImages[i].gameObject.SetActive(true);
            }

            correctOption = 1;//hard coding correct value to be 1            

            for (int i = 0; i < optionButtons.Length; i++)//loop setting the options
            {
                Button currentButton = optionButtons[i];
                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(() => CheckIfCorrect(currentButton));
                currentButton.image.sprite = testLevels.level4Data.optionData[i].optionsSprite;
                ButtonValue buttonValue = currentButton.GetComponent<ButtonValue>();
                buttonValue.buttonValue = testLevels.level4Data.optionData[i].optionNumber;
            }
            questionText.text = "Tap The Correct Pair";
            ShuffleOptions();
        }
        private void SetFifthQuestion()
        {
            DisableAllQuestionImages();
            for (int i = 0; i < testLevels.level5Data.questionSprite.Length; i++)
            {
                questionImages[i].sprite = testLevels.level5Data.questionSprite[i];
                questionImages[i].gameObject.SetActive(true);
            }

            correctOption = 1;//hard coding correct value to be 1            

            for (int i = 0; i < optionButtons.Length; i++)//loop setting the options
            {
                Button currentButton = optionButtons[i];
                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(() => CheckIfCorrect(currentButton));
                currentButton.image.sprite = testLevels.level5Data.optionData[i].optionsSprite;
                ButtonValue buttonValue = currentButton.GetComponent<ButtonValue>();
                buttonValue.buttonValue = testLevels.level5Data.optionData[i].optionNumber;
            }
            questionText.text = "Tap The Correct Pair";
            ShuffleOptions();
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
            TestGameManager.Instance.OnLevelEnd -= LoadNextQuestionAfterDelay;
            TestGameManager.Instance.OnLevelStart -= () => boardCleaningEffect.SetActive(false);
            OnOptionSelected -= AnimateOnButtonClick;
            TestGameManager.Instance.OnLevelStart -= AnimateOnLevelStart;
            TestGameManager.Instance.OnGameEnd -= SendTestData;
            TestGameManager.Instance.OnGameEnd -= () => boardCleaningEffect.SetActive(true);
            //TestGameManager.Instance.OnLevelStart -= MoveObjectDown;
            //TestGameManager.Instance.OnLevelEnd -= MoveObjectUp;
        }
    }
}

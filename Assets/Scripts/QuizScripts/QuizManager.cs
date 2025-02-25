using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.FamilyTree
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private Transform questionParent;
        [SerializeField] private Transform optionParent;
        [SerializeField] private Image treeImage;
        [SerializeField] private GameObject quizParent;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private ButtonData[] quizButtons;
        [SerializeField] private QuizSO englishQuiz;
        [SerializeField] private QuizSO hindiQuiz;
        [SerializeField] private QuizSO tamilQuiz;
        private QuizSO quizSO;
        private int levelIndex;
        private int questionNumber;
        private string languageString;
        //public event Action OnOptionSelected;

        private void Awake()
        {
            SetQuizSO();
        }
        private void Start()
        {
            quizParent.SetActive(false);
            GameManager.Instance.OnLevelWin += ResetQuiz;
            GameManager.Instance.UIManager.OnFullTreeShown += StartCurrentQuiz;
            GameManager.Instance.UIManager.OnMenuPressed += ResetQuiz;
            questionNumber = 0;
        }
        private void SetQuizSO()
        {
            languageString = PlayerPrefs.GetString("PlaySchoolLanguageAudio", languageString);
            switch (languageString)
            {
                case "English":
                    quizSO = englishQuiz;
                    break;
                case "EnglishUS":
                    quizSO = englishQuiz;
                    break;
                case "Hindi":
                    quizSO = hindiQuiz;
                    break;
                case "Tamil":
                    quizSO = tamilQuiz;
                    break;
                default:
                    quizSO = englishQuiz;
                    break;
            }
        }
        private void StartCurrentQuiz()
        {
            quizParent.SetActive(true);
            StartQuiz();
        }
        private void ResetQuiz()
        {
            questionNumber = 0;
            quizParent.SetActive(false);
        }
        private QuizLevels GetQuizLevels()
        {
            levelIndex = GameManager.Instance.LevelManager.GetLevelIndex();
            return quizSO.quizLevels[levelIndex];
        }
        private void LoadNextQuestion()
        {
            StartCoroutine(LoadNextQuestionAfterDelay());
        }
        private void QuizStartAnimation()
        {
            questionParent.DOLocalMoveY(Screen.height, 0f).OnComplete(() =>
            {
                GameManager.Instance.SoundManager.PlayQuestion();
                questionParent.DOLocalMoveY(440f, 0.5f);//hardcoded
            });
            optionParent.DOLocalMoveX(Screen.width, 0f).OnComplete(() =>
            {
                optionParent.DOLocalMoveX(0f, 0.5f);
            });
            treeImage.transform.DOLocalMoveY(Screen.height, 0f).OnComplete(() =>
            {
                treeImage.transform.DOLocalMoveY(0f, 0.75f).OnComplete(() =>
                {
                    EnableButtons();
                });
            });
        }
        private IEnumerator LoadNextQuestionAfterDelay()
        {
            yield return new WaitForSeconds(4f);
            questionNumber++;
            if (questionNumber > 2)
            {
                GameManager.Instance.InvokeLevelWin();
                questionNumber = 0;
                yield break;
            }
            StartQuiz();
        }
        private void DisableButtons()
        {
            foreach (var button in quizButtons)
            {
                button.QuizButton.enabled = false;
            }
        }
        private void EnableButtons()
        {
            foreach (var button in quizButtons)
            {
                button.QuizButton.enabled = true;
            }
        }
        private void StartQuiz()
        {
            QuizLevels currentLevel = GetQuizLevels();
            if (currentLevel != null)
            {
                QuizData currentQuizData = currentLevel.quizSet[questionNumber];
                SetQuestion(currentQuizData);
                SetOptions(currentQuizData);
                QuizStartAnimation();
                SetHintSprite(currentLevel.hintSprite);
            }
            else
            {
                Debug.Log("Quiz not Found");
            }
        }
        private void SetHintSprite(Sprite sprite)
        {
            treeImage.sprite = sprite;
        }
        private void CheckIfCorrect(QuizButtonManager qbManager)
        {
            DisableButtons();
            Options optionSelected = qbManager.value;
            if (optionSelected == Options.Correct)
            {
                qbManager.EnableCorrectImage();
                GameManager.Instance.SoundManager.PlayCorrectAnswer();
            }
            else
            {
                GameManager.Instance.SoundManager.PlayInCorrectAnswer();
                QuizButtonManager correctQBManager = FindCorrectButtonManager(Options.Correct);
                correctQBManager.EnableCorrectImage();
                qbManager.EnableIncorrectImage();
            }
            LoadNextQuestion();
            //OnOptionSelected?.Invoke();
        }
        private QuizButtonManager FindCorrectButtonManager(Options correct)
        {
            ButtonData bd = Array.Find(quizButtons, i => i.buttonManager.value == correct);
            return bd.buttonManager;
        }
        private void SetQuestion(QuizData currentQuizdata)
        {
            questionText.text = currentQuizdata.question;
        }
        private void SetOptions(QuizData currentQuizdata)
        {
            for (int i = 0; i < quizButtons.Length; i++)
            {
                Options value = currentQuizdata.options[i].value;
                string name = currentQuizdata.options[i].name;
                QuizButtonManager currentQBManager = quizButtons[i].buttonManager;
                currentQBManager.SetData(value, name);
                quizButtons[i].QuizButton.onClick.RemoveAllListeners();
                quizButtons[i].QuizButton.onClick.AddListener(() => CheckIfCorrect(currentQBManager));
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelWin -= ResetQuiz;
            GameManager.Instance.UIManager.OnFullTreeShown -= StartCurrentQuiz;
            GameManager.Instance.UIManager.OnMenuPressed -= ResetQuiz;
        }
    }
    [System.Serializable]
    public class ButtonData
    {
        public QuizButtonManager buttonManager;
        public Button QuizButton;
    }
}

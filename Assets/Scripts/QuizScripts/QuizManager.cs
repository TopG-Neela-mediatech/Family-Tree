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
        [SerializeField] private QuizSO[] quizzes;
        [SerializeField] private GameObject quizParent;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private ButtonData[] quizButtons;
        private int levelIndex;
        private int questionNumber;
        private const int correctOption = 1;//correct answer always one
        //public event Action OnOptionSelected;


        private void Start()
        {
            quizParent.SetActive(false);
            questionNumber = 0;
            GameManager.Instance.OnTreeComplete += StartCurrentQuiz;
            GameManager.Instance.OnLevelWin += () => quizParent.SetActive(false);
        }
        private void StartCurrentQuiz()
        {
            quizParent.SetActive(true);
            StartQuiz();
        }
        private QuizSO GetQuizData()
        {
            switch (levelIndex)
            {
                case 0:
                    return quizzes[0];
                case 1:
                    return quizzes[1];
                case 2:
                    return quizzes[2];
                case 3:
                    return quizzes[3];
                case 4:
                    return quizzes[4];
            }
            return null;
        }
        private void LoadNextQuestion()
        {
            StartCoroutine(LoadNextQuestionAfterDelay());
        }
        private void QuizStartAnimation()
        {
            questionParent.DOLocalMoveX(Screen.width, 0f).OnComplete(() =>
            {
                questionParent.DOLocalMoveX(0f, 0.5f);
            });
            optionParent.DOLocalMoveX(-Screen.width, 0f).OnComplete(() =>
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
            yield return new WaitForSeconds(2f);
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
            QuizSO currentQuizSO = GetQuizData();
            if (currentQuizSO != null)
            {
                QuizData currentQuizData = currentQuizSO.quizSet[questionNumber];
                SetQuestion(currentQuizData);
                SetOptions(currentQuizData);
                QuizStartAnimation();
            }
            else
            {
                Debug.Log("Quiz not Found");
            }
        }
        private void CheckIfCorrect(QuizButtonManager qbManager)
        {
            DisableButtons();
            int optionSelected = qbManager.value;
            if (optionSelected == correctOption)
            {               
                qbManager.EnableCorrectImage();
            }
            else
            {
                QuizButtonManager correctQBManager = FindCorrectButtonManager(correctOption);
                correctQBManager.EnableCorrectImage();
                qbManager.EnableIncorrectImage();
            }
            LoadNextQuestion();
            //OnOptionSelected?.Invoke();
        }
        private QuizButtonManager FindCorrectButtonManager(int correct)
        {
            ButtonData bd = Array.Find(quizButtons, i => i.buttonManager.value==correct);
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
                int value = currentQuizdata.options[i].value;
                string name = currentQuizdata.options[i].name;
                QuizButtonManager currentQBManager = quizButtons[i].buttonManager;
                currentQBManager.SetData(value, name);
                quizButtons[i].QuizButton.onClick.RemoveAllListeners();
                quizButtons[i].QuizButton.onClick.AddListener(() => CheckIfCorrect(currentQBManager));
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnTreeComplete -= StartCurrentQuiz;
            GameManager.Instance.OnLevelWin += () => quizParent.SetActive(false);
        }
    }
    [System.Serializable]
    public class ButtonData
    {
        public QuizButtonManager buttonManager;
        public Button QuizButton;
    }
}

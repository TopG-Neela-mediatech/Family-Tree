using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.FamilyTree
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private QuizSO[] quizzes;
        [SerializeField] private GameObject quizParent;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private ButtonData[] quizButtons;
        private int levelIndex;
        private int questionNumber;
        private const int correctOption = 1;//correct answer always one
        public event Action OnOptionSelected;


        private void Start()
        {
            quizParent.SetActive(false);
            questionNumber = 0;
            GameManager.Instance.OnTreeComplete += StartCurrentQuiz;
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
        private void StartQuiz()
        {
            QuizSO currentQuizSO = GetQuizData();
            if (currentQuizSO != null)
            {
                QuizData currentQuizData = currentQuizSO.quizSet[questionNumber];
                SetQuestion(currentQuizData);
                SetOptions(currentQuizData);
            }
            else
            {
                Debug.Log("Quiz not Found");
            }
        }
        private void CheckIfCorrect(int value)
        {
            int optionSelected = value;
            if (optionSelected == correctOption)
            {
                Debug.Log("Correct");
            }
            else
            {
                Debug.Log("Incorrect");
            }
            OnOptionSelected?.Invoke();
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
                quizButtons[i].buttonManager.SetData(value, name);
                quizButtons[i].QuizButton.onClick.RemoveAllListeners();
                quizButtons[i].QuizButton.onClick.AddListener(() => CheckIfCorrect(value));
            }
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnTreeComplete -= StartCurrentQuiz;
        }
    }
    [System.Serializable]
    public class ButtonData
    {
        public QuizButtonManager buttonManager;
        public Button QuizButton;
    }
}

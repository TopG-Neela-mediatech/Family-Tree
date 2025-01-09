using TMPro;
using UnityEngine;


namespace TMKOC.FamilyTree
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private QuizSO[] quizzes;
        [SerializeField] private GameObject quizParent;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private QuizButtonManager[] quizButtons;
        private int levelIndex;
        private int questionNumber;


        private void Start()
        {
            //quizParent.SetActive(false);
            questionNumber = 0;
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
                quizButtons[i].SetData(value, name);
            }
        }
    }
}

using UnityEngine;

namespace TMKOC.FamilyTree
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "QuizSO")]
    public class QuizSO : ScriptableObject
    {
        public QuizData[] quizSet;
    }
    [System.Serializable]
    public class QuizData
    {
        public string question;
        public OptionData[] options;
        public const int correctAnswer = 1;//correct answer always 1
    }
    [System.Serializable]
    public class OptionData
    {
        public string name;
        public int value;
    }
}

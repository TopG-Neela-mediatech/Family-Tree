using UnityEngine;

namespace TMKOC.FamilyTree
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "QuizSO")]
    public class QuizSO : ScriptableObject
    {
        public QuizLevels[] quizLevels;
    }
    [System.Serializable]
    public class QuizLevels
    {
        public QuizData[] quizSet;
        public Sprite hintSprite;
    }
    [System.Serializable]
    public class QuizData
    {
        public string question;
        public OptionData[] options;
    }
    [System.Serializable]
    public class OptionData
    {
        public string name;
        public Options value;
    }
    public enum Options
    {
        Incorrect,
        Correct
    }
}

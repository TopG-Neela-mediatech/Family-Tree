using UnityEngine;

namespace TMKOC.CountWithMe.AddTest
{

    [CreateAssetMenu(fileName = "AddTestSO", menuName = "AddTestSO")]
    public class AddTestSO : ScriptableObject
    {
        public AddTestLevelsData[] outerLevels;
    }
    [System.Serializable]
    public class AddTestLevelsData
    {
        public AddTestLevelData[] innerLevels;         
    }
    [System.Serializable]
    public class AddTestLevelData
    {
        public OptionData[] numbersToBeAdded;
        public int correctAnswer;
        public OptionData[] optionData;       
    }
    [System.Serializable]
    public class OptionData
    {
        public Sprite numberSprite;
        public int value;
    }
}

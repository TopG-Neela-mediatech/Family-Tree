using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Matching
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "MatchingLevelSO")]
    public class MatchingLevelSO : ScriptableObject
    {
        public List<MatchingImage> MatchingData = new List<MatchingImage>();
        public List<SlidingNumber> slidingNumbers = new List<SlidingNumber>();
    }
    [System.Serializable]
    public class MatchingImage
    {
        public Sprite[] objectImage;
        public int correspondingNumber;
    }
    [System.Serializable]
    public class SlidingNumber
    {
        public Sprite numberImage;
        public int number;
    }
}

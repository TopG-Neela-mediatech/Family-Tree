using TMPro;
using UnityEngine;


namespace TMKOC.FamilyTree
{
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "LevelSO")]
    public class LevelSO : ScriptableObject
    {
        public TreeController treeObject;
        public int memberCount;
        //public MemberData[] revealedMembers;
        public MemberData[] memberData;
        public FontData[] fontDatas;
    }   
    [System.Serializable]
    public class MemberData
    {
        public Sprite faceSprite;
        public string Name;
        public LocalDescription[] Description;
        public int Key;
        public FamilyMember member;
    }
    [System.Serializable]
    public class FontData
    {
        public DescriptionLanguage language;
        public TMP_FontAsset fontAsset;
    }
    [System.Serializable]
    public class LocalDescription
    {
        public DescriptionLanguage language;
        public string description;       
    }
    public enum DescriptionLanguage
    {
        English,
        Hindi,
        Tamil
    }
}

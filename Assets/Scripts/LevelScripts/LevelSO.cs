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
    }   
    [System.Serializable]
    public class MemberData
    {
        public Sprite faceSprite;
        public string Name;
        public string Description;
        public int Key;
        public FamilyMember member;
    }
}

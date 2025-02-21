using TMPro;
using UnityEngine;


namespace TMKOC.FamilyTree
{
    [CreateAssetMenu(fileName = "LocalizedName", menuName = "LocalizedNameSO")]
    public class MemberLocalNameSO : ScriptableObject
    {
        public LocalizeNames[] localNames;
        public TMP_FontAsset respectiveFontAsset;
    }

    [System.Serializable]
    public class LocalizeNames
    {
        public MemberRelationShip respectiveMember;
        public string localizedName;
    }
    public enum MemberRelationShip
    {
        None,
        Father,
        Mother,
        Daughter,
        Son,
        FatherBrother,
        FatherBrotherWife,
        FatherBrotherDaughter,
        MotherSister,
        MotherSisHusband,
        MotherSisterSon,
        FatherFather,
        FatherMother,
        MotherFather,
        MotherMother
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    [CreateAssetMenu(fileName = "LocalizedName", menuName = "ScritpableObejct")]
    public class MemberLocalNameSO : ScriptableObject
    {
        public LocalizeNames[] localNames;
    }

    [System.Serializable]
    public class LocalizeNames
    {
        public TextMeshProUGUI text;
        public FamilyMember respectiveMember;
        public string localizedName;
    }
}

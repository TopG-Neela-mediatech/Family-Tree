using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    [CreateAssetMenu(fileName = "FamilyMemberSoundSO", menuName = "MemberSoundSO")]
    public class MemberSoundSO : ScriptableObject
    {
        public IndividualMemberAudio[] memberAudio;
    }
}

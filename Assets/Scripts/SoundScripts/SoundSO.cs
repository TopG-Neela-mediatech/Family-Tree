using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    [CreateAssetMenu(fileName = "FamilyTreeSoundSO", menuName = "SoundSO")]
    public class SoundSO : ScriptableObject
    {
        public IndividualLevelSound[] levelsAudio;
    }
    [CreateAssetMenu(fileName = "FamilyMemberSoundSO", menuName = "MemberSoundSO")]
    public class MemberSoundSO : ScriptableObject
    { 
        public IndividualMemberAudio[] memberAudio;        
    }
    [System.Serializable]
    public class IndividualLevelSound
    {
        public AudioClip intro;
        public AudioClip outro;
        public AudioClip treeCompleteAudio;       
        public IndividualQuizSound[] quizAudio;
    }
    [System.Serializable]
    public class IndividualQuizSound
    {
        public AudioClip Question;
        public AudioClip correctAnswer;
        public AudioClip incorrectAnswer;
    }
    [System.Serializable]
    public class IndividualMemberAudio
    {
        public FamilyMember memberIdentity;
        public AudioClip memberClip;
    }
    public enum FamilyMember
    {
        None,
        Son,
        Daughter,
        Cousin1,
        Cousin2,
        Mother,
        Father,
        PaternalUnc,
        PaternalUncWife,
        MaternalAunt,
        MaternalAuntHus,
        PaternalGrandFather,
        PaternalGrandMother,
        MaternalGrandFather,
        MaternalGrandMother
    }
}

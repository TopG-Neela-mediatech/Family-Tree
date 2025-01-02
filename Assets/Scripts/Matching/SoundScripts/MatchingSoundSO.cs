using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Matching
{
    [CreateAssetMenu(fileName = "MatchingSoundData", menuName = "ScriptableObject/MatchingSoundSO")]
    public class MatchingSoundSO : ScriptableObject
    {
        public AudioClip[] common1to10;
        public AudioClip[] levelStartAudio;
        public AudioClip[] levelLoseAudio;
        public AudioClip[] incorrectAudio;
        public AudioClip[] levelEndAudio;
        public AudioClip[] timeUpAudio;
        public AudioClip gameIntro;
        public AudioClip gameOutro;
    }
}

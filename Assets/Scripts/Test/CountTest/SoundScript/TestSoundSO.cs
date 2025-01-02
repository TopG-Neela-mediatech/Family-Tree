using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Test
{
    [CreateAssetMenu(fileName = "TestSoundData", menuName = "ScriptableObject/TestSoundSO")]
    public class TestSoundSO : ScriptableObject
    {
        //public AudioClip[] common1to10;
        public AudioClip[] levelStartAudio;
        public AudioClip[] incorrectAudio;
        public AudioClip[] correctAudio;
        public AudioClip[] timeUpAudio;
        public AudioClip gameIntro;
        public AudioClip gameOutro;
    }
}

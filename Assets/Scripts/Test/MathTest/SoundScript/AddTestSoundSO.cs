using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.AddTest
{
    [CreateAssetMenu(fileName = "AddTestSoundData", menuName = "ScriptableObject/AddTestSoundSO")]
    public class AddTestSoundSO : ScriptableObject
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Add
{
    [CreateAssetMenu(fileName = "AddSoundData", menuName = "ScriptableObject/AddSoundSO")]
    public class AddSoundSO : ScriptableObject
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

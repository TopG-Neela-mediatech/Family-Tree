using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.AddTest
{
    public class AttemptCounterAdd : MonoBehaviour
    {
        
        [SerializeField] private Image correctImage;
        [SerializeField] private Image incorrectImage;


        private void Start()
        {
            DisableCorrectIncorrect();
            AddTestGameManager.Instance.OnGameEnd += DisableCorrectIncorrect;
        }
        private void DisableCorrectIncorrect()
        {
            correctImage.enabled = false;
            incorrectImage.enabled = false;
        }
        

        public void EnableCorrect() => correctImage.enabled = true;
        public void EnableIncorrect()=>incorrectImage.enabled = true;

        private void OnDestroy()
        {
            AddTestGameManager.Instance.OnGameEnd -= DisableCorrectIncorrect;
        }
    }
}

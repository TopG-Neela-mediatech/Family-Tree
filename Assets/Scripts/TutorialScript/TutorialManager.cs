using DG.Tweening;
using UnityEngine;


namespace TMKOC.CountWithMe
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;
        private Vector3 startPoint;
        [SerializeField] private GameObject tutorialHandObject;
        public bool notTappedYet { get; set; }
       
      
        private void Start()
        {
            startPoint = tutorialHandObject.transform.localPosition;
            notTappedYet = true;
        }
        private void Update()
        {
            if (Input.touchCount > 0 && notTappedYet)
            {
                notTappedYet = false;
            }
        }
        public void StartHint()
        {
            if (notTappedYet)
            {                
                PlayHintAnimation();
            }
        }
        private void PlayHintAnimation()
        {
            if (notTappedYet)
            {
                tutorialHandObject.transform.DOScale(0.15f, 0f);
                tutorialHandObject.transform.DOMove(firstPoint.position, 1f).OnComplete(() =>
                {
                    tutorialHandObject.transform.DOMove(secondPoint.position, 1f).OnComplete(() =>
                    {
                        tutorialHandObject.transform.DOScale(0f, 0.3f).OnComplete(() =>
                        {
                            tutorialHandObject.transform.localPosition = startPoint;
                            PlayHintAnimation();
                        });
                    });
                });
            }
            else
            {               
                tutorialHandObject.transform.DOScale(0f, 0.3f).OnComplete(() =>
                {
                    tutorialHandObject.transform.localPosition = startPoint;
                });
            }
        }
    }
}

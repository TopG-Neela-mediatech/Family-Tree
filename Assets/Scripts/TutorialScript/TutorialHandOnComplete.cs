using DG.Tweening;
using UnityEngine;

namespace TMKOC.CountWithMe
{
    public class TutorialHandOnComplete : MonoBehaviour
    {
        [SerializeField] private GameObject endTutorialHand;
        [SerializeField] private Transform endPoint;       
        private Vector3 startPoint;
        private bool isPlaying;

       
        private void Start()
        {
            startPoint = endTutorialHand.transform.localPosition;
        }
        public void DescaleEndHand() => endTutorialHand.transform.DOScale(0f, 0f);
        public void StartEndHint()
        {
            if (!isPlaying)
            {
                isPlaying = true;
                endTutorialHand.transform.DOScale(0.15f, 0f);
                endTutorialHand.transform.DOMove(endPoint.position, 1f).OnComplete(() =>
                {
                    endTutorialHand.transform.DOShakePosition(0.7f, new Vector3(0f, 15f, 0f), 3).SetLoops(3).OnComplete(() =>
                    {
                        endTutorialHand.transform.DOScale(0f, 0.3f).OnComplete(() =>
                        {
                            endTutorialHand.transform.localPosition = startPoint;
                            isPlaying = false;
                        });
                    });
                });
            }
        }
    }
}


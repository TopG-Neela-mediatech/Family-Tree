using DG.Tweening;
using System.Collections;
using UnityEngine;


namespace TMKOC.CountWithMe
{
    public class CountingTutorialManager : MonoBehaviour
    {
        private Vector3 startPoint;
        [SerializeField] private GameObject tutorialHandObject;        
        private bool isPlaying;
        private float timeSinceLastTouch = 0f; // Tracks the time since the last touch
        private float inactivityThreshold = 3f;
        private bool canBeCalled;

       
        private void Start()
        {
            GameManagerCounting.Instance.OnLevelStart += () => isPlaying = false;
            startPoint = tutorialHandObject.transform.localPosition;
            isPlaying = false;
            canBeCalled = true;
        }
        void Update()
        {
            if (Input.touchCount > 0)
            {
                timeSinceLastTouch = 0f;
                canBeCalled = false;
            }
            else
            {
                timeSinceLastTouch += Time.deltaTime;
            }
            if (timeSinceLastTouch > inactivityThreshold)
            {
                canBeCalled = true;
            }
        }
        public void StartHint(Vector3 firstPoint)
        {
            if ((!isPlaying) && canBeCalled)
            {
                isPlaying = true;
                tutorialHandObject.transform.DOScale(0.15f, 0f);
                tutorialHandObject.transform.DOMove(firstPoint, 1f).OnComplete(() =>
                {
                    tutorialHandObject.transform.DOShakePosition(0.7f, new Vector3(0f,15f,0f), 3).SetLoops(3).OnComplete(() =>
                     {
                         tutorialHandObject.transform.DOScale(0f, 0.3f).OnComplete(() =>
                         {
                             tutorialHandObject.transform.localPosition = startPoint;
                             StartCoroutine(ResetBoolAfterDelay());
                         });
                     });
                });
            }
        }
        private IEnumerator ResetBoolAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            isPlaying = false;
        }
        private void OnDestroy()
        {
            GameManagerCounting.Instance.OnLevelStart -= () => isPlaying = false;
        }
    }
}


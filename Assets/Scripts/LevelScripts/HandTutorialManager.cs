using DG.Tweening;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class HandTutorialManager : MonoBehaviour
    {
        [SerializeField] private Transform parentT;
        [SerializeField] private Transform handTransform;
        private Vector3 startPoint;
        private float inputTimer;
        private float inputTimeout = 5f;
        public bool isPlaying { get; private set; }


        private void Start()
        {
            startPoint = new Vector3(0, -4f, 0);
            this.enabled = false;
            GameManager.Instance.OnLevelStart += () => this.enabled = true;
            GameManager.Instance.OnTreeComplete += () => this.enabled = false;
            GameManager.Instance.OnLevelStart += () => isPlaying = false;
        }
        private void Update()
        {
            StartHandTutorialOnIdle
                (
                GameManager.Instance.LevelManager.GetDragPosition(),
                GameManager.Instance.LevelManager.GetDropTransform()
                );
        }
        private void StartHandTutorialOnIdle(Vector3 posi1, Transform p2)
        {
            if (!isPlaying)
            {
                if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
                {
                    inputTimer = 0f;
                }
                else
                {
                    inputTimer += Time.deltaTime;
                    if (inputTimer >= inputTimeout)
                    {
                        inputTimer = 0f;
                        StartHandTutorial(posi1, p2);
                    }
                }
            }
        }
        public void StartHandTutorial(Vector3 pos1, Transform pos2)
        {
            isPlaying = true;
            handTransform.DOLocalMove(pos1, 1.25f).OnComplete(() =>
            {
                handTransform.SetParent(pos2);
                handTransform.DOLocalMove(pos2.position, 1.25f).OnComplete(() =>
                {
                    handTransform.SetParent(parentT);
                    handTransform.DOLocalMove(startPoint, 0f).OnComplete(() =>
                    {
                        isPlaying = false;
                    });
                });
            });
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelStart -= () => this.enabled = true;
            GameManager.Instance.OnTreeComplete -= () => this.enabled = false;
            GameManager.Instance.OnLevelStart -= () => isPlaying = false;
        }
    }
}

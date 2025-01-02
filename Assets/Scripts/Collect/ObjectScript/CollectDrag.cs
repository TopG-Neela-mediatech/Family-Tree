using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace TMKOC.CountWithMe.Collect
{
    public class CollectDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image image;
        [SerializeField] private BoxCollider2D boxCollider;
        [HideInInspector] public Vector3 originalPosition;

        private Coroutine temp;
        private Vector3 offSet;


        public bool IsDraggable { get; set; }
        public int currentNumber { get; set; }
        public void ChangeAlphaToOne() => canvasGroup.alpha = 1f;
        private void DeScale() => this.gameObject.transform.DOScale(0f, 0f);
        private void ScaleUp() => this.gameObject.transform.DOScale(1f, 0f);


        private void Start()
        {                      
            originalPosition = transform.localPosition;
            IsDraggable = true;
            GameManagerCollect.Instance.OnLevelStart += ScaleUp;
            GameManagerCollect.Instance.LivesManager.OnLivesOver += DeScale;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsDraggable)
            {
                offSet = transform.localPosition - Input.mousePosition;
                canvasGroup.alpha = 0.5f;
                GameManagerCollect.Instance.SoundManager.PlaySFX(Sounds.numberPickSound);
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (IsDraggable)
            {
                transform.localPosition = Input.mousePosition + offSet;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (temp != null)
            {
                StopCoroutine(temp);
            }
            temp = StartCoroutine(ResetAfterDelay());
        }
        private IEnumerator ResetAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            if (transform.localPosition != originalPosition) transform.localPosition = originalPosition;
            canvasGroup.alpha = 1f;
        }


        public Image GetImage() => image;
        public void EnableCollider(bool status) => boxCollider.enabled = status;


        public void MoveToOGPosi()
        {           
            IsDraggable = false;
            transform.DOLocalMove(originalPosition, 0.1f).OnComplete(() =>
            {
                IsDraggable = true;               
                EnableCollider(true);
            }
            );
            ChangeAlphaToOne();
        }
        private void OnDestroy()
        {           
            GameManagerCollect.Instance.OnLevelStart -= ScaleUp;
            GameManagerCollect.Instance.LivesManager.OnLivesOver -= DeScale;
        }
    }
}


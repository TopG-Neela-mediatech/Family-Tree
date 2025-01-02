using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TMKOC.CountWithMe.Matching
{
    public class DragScriptMatching : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image objectImage;
        [SerializeField] private CanvasGroup canvasGroup;
        [HideInInspector] public Vector3 originalPosition;
        [SerializeField] private Collider2D collide;
        private Vector3 offSet;
        private bool IsBeingDragged;


        public bool IsDraggable { get; set; }
        public int currentNumber { get; set; }
        public void ChangeAlphaToOne() => canvasGroup.alpha = 1f;



        private void Start()
        {
            originalPosition = transform.localPosition;
            IsDraggable = false;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsDraggable)
            {
                GameManagerMatching.Instance.SoundManager.PlayCurrentNumberSound(currentNumber);
                IsBeingDragged = true;
                offSet = transform.localPosition - Input.mousePosition;
                canvasGroup.alpha = 0.5f;
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
            MoveToOGPosi();
        }
        private void MoveToOGPosi()
        {
            this.enabled = false;
            IsDraggable = false;
            transform.DOLocalMove(originalPosition, 0.1f).OnComplete(() =>
            {
                IsDraggable = true;
                this.enabled = true;
            }
            );
            IsBeingDragged = false;
            ChangeAlphaToOne();
            canvasGroup.alpha = 1f;
        }
        public Vector3 MoveToNewPosi(Vector3 newPosi)
        {
            collide.enabled = false;
            Vector3 temp = this.originalPosition;
            if (!IsBeingDragged)
            {
                GameManagerMatching.Instance.SoundManager.PlaySFX(Sounds.swipeSound);
                IsDraggable = false;
                this.originalPosition = newPosi;
                transform.DOLocalMove(originalPosition, 0.3f).OnComplete(() =>
                {
                    IsDraggable = true;
                    collide.enabled = true;
                }
                );
            }
            return temp;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<DragScriptMatching>(out DragScriptMatching nextObject))
            {
                if (nextObject != null && IsBeingDragged)
                {
                    Vector3 temp = nextObject.MoveToNewPosi(this.originalPosition);
                    this.originalPosition = temp;
                }
            }
        }
    }
}


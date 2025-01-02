using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace TMKOC.CountWithMe.Sequence
{
    public class Dragscript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image objectImage;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button resetDragButton;
        private Vector3 originalPosition;
        private Vector3 offSet;


        public bool IsDraggable { get; set; }
        public bool OnDropObject { get; set; }
        public int draggedNumber { get; private set; }


        private void Start()
        {
            originalPosition = transform.localPosition;
           GameManagerSequence.Instance.OnLevelWin += DescaleGameObject;
           GameManagerSequence.Instance.OnLevelWin += () => IsDraggable = false;
           GameManagerSequence.Instance.OnLevelLose += () => IsDraggable = false;
           GameManagerSequence.Instance.OnLevelStart += ResetDragObject;
           GameManagerSequence.Instance.OnLevelStart += () => resetDragButton.enabled = false;
           GameManagerSequence.Instance.LivesManager.OnLivesOver += DescaleGameObject;
            resetDragButton.onClick.AddListener(MoveToOGPosi);
            IsDraggable = false;
        }
        private void ResetDragObject()
        {
            MoveToOGPosi();
            this.transform.DOScale(1f, 0.5f);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsDraggable)
            {
                resetDragButton.enabled = false;
               GameManagerSequence.Instance.SoundManager.PlayCurrentNumberSound(draggedNumber);
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
            if (!OnDropObject)
            {
                MoveToOGPosi();
            }
        }
        public void MoveToOGPosi()
        {
            resetDragButton.enabled = false;
            IsDraggable = false;
            transform.DOLocalMove(originalPosition, 0.1f).OnComplete(() =>
                {
                    IsDraggable = true;
                }
            );
            ChangeAlphaToOne();
            canvasGroup.alpha = 1f;
            OnDropObject = false;
        }
        private void DescaleGameObject()
        {
            this.transform.DOScale(0f, 0.5f).OnComplete(() =>
            {
                transform.DOLocalMove(originalPosition, 0f);
            });
        }
        private IEnumerator EnableResetButtonAfterDelay()
        {
            yield return new WaitForSeconds(0.3f);
            resetDragButton.enabled = true;
        }


        public void ChangeAlphaToOne() => canvasGroup.alpha = 1f;
        public void SetDragNumber(int number) => draggedNumber = number;
        public void EnableResetButton() => StartCoroutine(EnableResetButtonAfterDelay());


        private void OnDestroy()
        {
           GameManagerSequence.Instance.OnLevelWin -= DescaleGameObject;
           GameManagerSequence.Instance.OnLevelWin -= () => IsDraggable = false;
           GameManagerSequence.Instance.OnLevelLose -= () => IsDraggable = false;
           GameManagerSequence.Instance.OnLevelStart -= ResetDragObject;
           GameManagerSequence.Instance.LivesManager.OnLivesOver -= DescaleGameObject;
           GameManagerSequence.Instance.OnLevelStart -= () => resetDragButton.enabled = false;
        }
    }
}

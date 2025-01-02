using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Sequence
{
    public class DropDetector : MonoBehaviour
    {
        private Dragscript draggedObject;
        public bool IsFilled;


        private void Start()
        {
            GameManagerSequence.Instance.OnLevelStart += () => IsFilled = false;
            GameManagerSequence.Instance.OnLevelStart += () => draggedObject = null;
            GameManagerSequence.Instance.OnLevelWin += () => SetGameObjectStatus(false);
            GameManagerSequence.Instance.OnLevelLose += () => SetGameObjectStatus(false);
            GameManagerSequence.Instance.OnLevelStart += () => SetGameObjectStatus(true);
        }
        private void SetGameObjectStatus(bool status) => this.gameObject.SetActive(status);
        private void OnTriggerEnter(Collider other)
        {
            if (draggedObject == null)
            {
                draggedObject = other.GetComponent<Dragscript>();
                if (draggedObject != null)
                {
                    GameManagerSequence.Instance.SoundManager.PlaySFX(Sounds.numberPlacedSound);
                    IsFilled = true;
                    GameManagerSequence.Instance.LevelManager.EnableCheckButton();
                    draggedObject.OnDropObject = true;
                    draggedObject.IsDraggable = false;
                    draggedObject.ChangeAlphaToOne();
                    draggedObject.transform.DOMove(transform.position, 0.5f);
                    StartCoroutine(EnableDraggingAfterDelay(draggedObject));
                    draggedObject.EnableResetButton();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            other.TryGetComponent<Dragscript>(out var drag);

            if (drag != null)
            {
                if (draggedObject == drag)
                {
                    IsFilled = false;
                    GameManagerSequence.Instance.LevelManager.EnableCheckButton();
                    draggedObject.OnDropObject = false;
                    draggedObject = null;
                }
            }
        }
        private IEnumerator EnableDraggingAfterDelay(Dragscript draggedObject)
        {
            yield return new WaitForSeconds(1f);
            draggedObject.IsDraggable = true;
        }
        public int GetStoredNumber()
        {
            if (draggedObject != null)
            {
                return draggedObject.draggedNumber;
            }
            return -1;
        }
        private void OnDestroy()
        {
            GameManagerSequence.Instance.OnLevelStart -= () => IsFilled = false;
            GameManagerSequence.Instance.OnLevelStart -= () => draggedObject = null;
            GameManagerSequence.Instance.OnLevelWin -= () => SetGameObjectStatus(false);
            GameManagerSequence.Instance.OnLevelLose -= () => SetGameObjectStatus(false);
            GameManagerSequence.Instance.OnLevelStart -= () => SetGameObjectStatus(true);
        }
    }
}

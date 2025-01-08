using DG.Tweening;
using TMPro;
using UnityEngine;


namespace TMKOC.FamilyTree
{
    public class DragScript : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [HideInInspector] public bool isOnTree;
        [SerializeField] private SpriteRenderer displaySprite;
        [SerializeField] private TextMeshProUGUI dataText;
        private Vector3 originalPosition;
        private Vector3 offset;
        private bool isBeingDragged = false;
        private int touchIndex = -1;
        public int value { get; private set; }


        void Start()
        {
            originalPosition = transform.position;
            isOnTree = false;
            this.enabled = false;
        }
        void Update()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    Vector2 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
                    if (hit.collider != null)
                    {
                        DragScript touchedObject = hit.collider.GetComponent<DragScript>();
                        if (touchedObject != null && touchedObject == this)
                        {
                            if (touch.phase == TouchPhase.Began && touchIndex == -1)
                            {
                                OnTouchDown(i);
                            }
                            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                            {
                                OnTouchUp();
                            }
                        }
                    }
                }
            }
            if (isBeingDragged && !isOnTree)
            {
                DragObject();
                if (IsOutOfBounds())
                {
                    ReturnToOriginalPosition();
                    isBeingDragged = false;
                    touchIndex = -1;
                }
            }
        }
        public void SetData(Sprite sprite, string data, int key)
        {
            displaySprite.sprite = sprite;
            dataText.text = data;
            value = key;
            NormalizeRenderer();
        }
        private void NormalizeRenderer()
        {
            displaySprite.drawMode = SpriteDrawMode.Sliced;
            displaySprite.size = Vector2.one;
            displaySprite.gameObject.transform.localScale = Vector2.one;
        }
        private bool IsOutOfBounds()
        {
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            Vector3 objectPos = transform.position;
            return objectPos.x < -cameraWidth / 2 || objectPos.x > cameraWidth / 2 ||
                   objectPos.y < -cameraHeight / 2 || objectPos.y > cameraHeight / 2;
        }
        private void OnTouchDown(int touchID)
        {
            isBeingDragged = true;
            touchIndex = touchID;
            offset = transform.position - GetTouchWorldPosition(touchID);
        }
        private void OnTouchUp()
        {
            if ((isBeingDragged && !isOnTree) || IsOutOfBounds())
            {
                ReturnToOriginalPosition();
                isBeingDragged = false;
                touchIndex = -1;
            }
        }
        private void DragObject()
        {
            Vector3 touchWorldPos = GetTouchWorldPosition(touchIndex);
            transform.position = touchWorldPos + offset;
        }
        private Vector3 GetTouchWorldPosition(int touchID)
        {
            Vector3 touchPos = Input.GetTouch(touchID).position;
            touchPos.z = mainCamera.transform.position.z;
            return mainCamera.ScreenToWorldPoint(touchPos);
        }
        public void ReturnToOriginalPosition()
        {
            this.transform.DOMove(originalPosition, 0.3f).OnComplete(() =>
            {
                isOnTree = false;
                isBeingDragged = false;
                touchIndex = -1;
                DropController.canCheck = true;
                GameManager.Instance.LevelManager.StartHint();
            });
        }
    }
}

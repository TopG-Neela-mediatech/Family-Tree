using DG.Tweening;
using TMPro;
using UnityEngine;


namespace TMKOC.FamilyTree
{
    public class DropController : MonoBehaviour
    {
        [SerializeField] private int value;
        [SerializeField] private SpriteRenderer displaySprite;
        [SerializeField] private TextMeshProUGUI dataText;
        public static bool canCheck;
        private bool isEmpty;


        public int GetValue() => value;


        private void Start()
        {
            canCheck = true;
            isEmpty = true;
        }
        public void SetRevealedData(Sprite sprite,string data)
        {
            displaySprite.sprite=sprite;
            dataText.text = data;
            NormalizeRenderer();
        }
        private void NormalizeRenderer()
        {
            displaySprite.drawMode = SpriteDrawMode.Sliced;
            displaySprite.size = Vector2.one;
            displaySprite.gameObject.transform.localScale = Vector2.one;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<DragScript>(out DragScript familyMember))
            {
                if (familyMember != null && canCheck && isEmpty)
                {
                    isEmpty = false;
                    canCheck = false;
                    familyMember.isOnTree = true;
                    familyMember.transform.DOMove(transform.position, 0.5f).OnComplete(() =>
                    {
                        if (value == familyMember.value)
                        {
                            Debug.Log("Member Key : " + familyMember.value + "\nTree key : " + value);
                            //check if all correct
                            Debug.Log("Win");
                            canCheck = true;
                            familyMember.enabled = false;
                        }
                        else
                        {
                            Debug.Log("Member Key : " + familyMember.value + "\nTree key : " + value);
                            familyMember.ReturnToOriginalPosition();
                            isEmpty = true;
                        }
                    });
                }
            }
        }
    }
}

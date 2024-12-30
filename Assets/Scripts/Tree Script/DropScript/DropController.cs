using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField] private int value;


    public int GetValue() => value;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DragScript>(out DragScript familyMember))
        {
            if (familyMember != null)
            {
                familyMember.isOnTree = true;
            }
        }
    }
}

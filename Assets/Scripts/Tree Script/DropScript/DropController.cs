using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField] private int value;
    public int GetValue() => value;
    //on trigger enter to check what came in
}

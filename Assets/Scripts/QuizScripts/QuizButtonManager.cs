using TMPro;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class QuizButtonManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI optionText;
        public int value { get; private set; }

        public void SetData(int val, string optionText)
        {
            value = val;
            this.optionText.text = optionText;
        }
    }
}

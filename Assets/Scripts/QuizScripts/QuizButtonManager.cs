using TMPro;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class QuizButtonManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI optionText;
        private int value;

        public void SetData(int val,string optionText)
        {
            value = val;
            this.optionText.text = optionText;
        }
    }
}

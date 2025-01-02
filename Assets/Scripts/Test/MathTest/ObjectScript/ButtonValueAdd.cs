using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.AddTest
{
    public class ButtonValueAdd : MonoBehaviour
    {
        [SerializeField] private Button button;


        public int buttonValue { get; set; }


        private void Start()
        {
            AddTestGameManager.Instance.OnLevelStart += () => StartCoroutine(EnableButtonAfterDelay());
            AddTestGameManager.Instance.LevelManager.OnOptionSelected += () => button.enabled =false;
            
        }
        private IEnumerator EnableButtonAfterDelay()
        {
            yield return new WaitForSeconds(1.5f); 
            button.enabled = true;
        }
        private void OnDestroy()
        {
            AddTestGameManager.Instance.OnLevelStart -= () => StartCoroutine(EnableButtonAfterDelay());
            AddTestGameManager.Instance.LevelManager.OnOptionSelected -= () => button.enabled = false;           
        }
    }
}

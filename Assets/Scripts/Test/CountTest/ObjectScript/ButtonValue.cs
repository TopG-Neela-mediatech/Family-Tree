using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Test
{
    public class ButtonValue : MonoBehaviour
    {
        [SerializeField] private Button button;


        public int buttonValue { get; set; }


        private void Start()
        {
            TestGameManager.Instance.OnLevelStart += () => StartCoroutine(EnableButtonAfterDelay());
            TestGameManager.Instance.LevelManager.OnOptionSelected += () => button.enabled =false;
            
        }
        private IEnumerator EnableButtonAfterDelay()
        {
            yield return new WaitForSeconds(1.5f); 
            button.enabled = true;
        }
        private void OnDestroy()
        {
            TestGameManager.Instance.OnLevelStart -= () => StartCoroutine(EnableButtonAfterDelay());
            TestGameManager.Instance.LevelManager.OnOptionSelected -= () => button.enabled = false;           
        }
    }
}

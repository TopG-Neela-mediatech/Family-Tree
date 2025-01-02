using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TMKOC.CountWithMe
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button playSchoolBackButton;
        [SerializeField] private Button playAgainButton;
        private void Start()
        {
            playSchoolBackButton.onClick.AddListener(()=>SceneManager.LoadScene(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu));
            playAgainButton.onClick.AddListener(GameManagerCounting.Instance.CountingLevelManager.LoadLevelOne);
        }
    }
}

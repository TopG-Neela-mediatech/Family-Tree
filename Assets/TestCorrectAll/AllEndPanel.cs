using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

using UnityEngine.UI;


public class AllEndPanel : MonoBehaviour
{
      public static AllEndPanel Instance;

         public Image[] starImage = new Image[5];
         public Sprite _goldStar;
          public Sprite _unHighlightedStar;


    
      
          public TMP_Text _motivatedtxtMid;
              public GameObject _endPanel;

           void Awake()
            {
                Instance = this;
               // PopUpEndPanel();
            }

            

            [Header("Audio Sources ")]
   
            public AudioSource _audioSource;
    //AllEndPanel.Instance.PopUpEndPanel();
    public void PopUpEndPanel(){

            _endPanel.SetActive(true);
                _motivatedtxtMid.transform.localScale = Vector2.zero;
                foreach (var star in starImage)
                {
                    star.sprite = _unHighlightedStar;
                    star.transform.localScale = Vector3.zero; 
                }
            

              for (int i = 0; i < 5 && i < starImage.Length; i++)
               {
                     int index = i; 
                     starImage[index].sprite = _goldStar; 
                       
                
                     starImage[index].transform.DOScale(Vector3.one, 0.3f)
                        .SetEase(Ease.OutBack)
                        .SetDelay(index * 0.2f); 
               }
            
                     _motivatedtxtMid.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
         }
         public void EndPanelOff(){
              _endPanel.SetActive(false);
         }

         public void BackToPlaySchool(){

                
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu);
         }

         public void BackToYourGame(string sceneName)
         {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
         }

        public void ReloadSceneAgain(){
                   UnityEngine.SceneManagement.SceneManager.LoadScene( UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }


 
}

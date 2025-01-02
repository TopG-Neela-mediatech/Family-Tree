
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestAudioManager : MonoBehaviour
{


        public static TestAudioManager Instance;

         public Image[] starImage = new Image[5];
         public Sprite _goldStar;
          public Sprite _unHighlightedStar;


         public Color32 _goldColour;
         public Color32 _silverColour;
         public Color32 _bronzeColour;

         public Sprite[] medals = new Sprite[3];
         public Image medalHolder ;
         public ParticleSystem _medalparentParticle;
         public int _scaleMe;
          public TMP_Text _motivatedtxtMid;
         public TMP_Text _motivatedtxtDown;

         public GameObject endPanelActive;
         //public ParticleSystem _medalchildParticle;
  
           void Awake()
            {
                Instance = this;
            }

            [Header("Audio Sources ")]
   
            public AudioSource _audioSource;     
           public AudioClip[] correctLastClip;

           public void PlayQuestionEnd(int _Rightanswer)
           {
              _audioSource.PlayOneShot(correctLastClip[_Rightanswer]);
           }

           public void EndPanelOff(){
              endPanelActive.SetActive(false);
         }

         public void PopUpStar(int correctAnswers){

            endPanelActive.SetActive(true);
            // _medalparentParticle.transform.DOScale();
            // _medalparentParticle.main.startColor = _goldColour;
            resetPopUi();
            

            foreach (var star in starImage)
            {
                  star.sprite = _unHighlightedStar;
            }
            switch(correctAnswers) 
            {
            case 3:
            medalHolder.sprite = medals[2];
            _motivatedtxtDown.text = "Bonze";
            _medalparentParticle.startColor = _bronzeColour;
               break;
            case 4:
                 medalHolder.sprite = medals[1];
                     _motivatedtxtDown.text = "Silver";
            _medalparentParticle.startColor = _silverColour;
               break;
            case 5:
              medalHolder.sprite = medals[0];
              _motivatedtxtDown.text = "Gold";
            _medalparentParticle.startColor = _goldColour;
               break;
               case 2:
              
              _motivatedtxtMid.text = "Keep Trying!";
           
               break;
               case 1:
            
              _motivatedtxtMid.text = "You can do better!";
          
               break;
               case 0:
            
              _motivatedtxtMid.text = "Try Again!";
          
               break;
         
            }
            if(correctAnswers >=  3 ){
               
            _medalparentParticle.transform.DOScale(_scaleMe, 0.5f).SetEase(Ease.OutBack);
            _motivatedtxtDown.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
               }else{

                  _motivatedtxtMid.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
               }

              for (int i = 0; i < correctAnswers && i < starImage.Length; i++)
               {
                     int index = i; 
                     starImage[index].sprite = _goldStar; 
                         starImage[index].transform.localScale = Vector3.zero; 
                
                     starImage[index].transform.DOScale(Vector3.one, 0.3f)
                        .SetEase(Ease.OutBack)
                        .SetDelay(index * 0.2f); 
               }
                for (int j = correctAnswers; j < 5 ; j++)
               {
                     starImage[j].transform.localScale = Vector3.one; 
               }
               
         }

         

     public void resetPopUi(){
         
            _medalparentParticle.transform.localScale = Vector2.zero;
            _motivatedtxtDown.transform.localScale = Vector2.zero;
            _motivatedtxtMid.transform.localScale = Vector2.zero;
               

     }

         public void BackToPlaySchool(){

                
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(TMKOCPlaySchoolConstants.TMKOCPlayMainMenu);
         }

         public void BackToYourGame(string sceneName)
         {
           UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
         }
    
}

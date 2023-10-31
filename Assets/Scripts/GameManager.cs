using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   public static GameManager Instance{set;get;}
    private bool isGameStarted=false;
    //@
    public GameObject Player;//@
    
    private PlayerMotor motor;
    public bool isDead{set;get;}
    public TextMeshProUGUI scoreText,coinText,modifierText;
    public float score,coinScore,modifierScore;
    private const int coinScoreAmount=5;
    private int lastScore;
    public Animator gameCanvas,menuAnim,diamondAnim;

    public Animator deathMenuAnim,shopAnim;
    public TextMeshProUGUI deadScoreText,deadCoinText,highScoreText;
    private loadInterstitial interstitial;
    private Shop shop;
    private HatLogic hatlogic;

    // Start is called before the first frame update
    void Awake()
    {
        Instance=this;
        modifierScore=1;
        coinText.text=coinScore.ToString("0");
        modifierText.text="x"+ modifierScore.ToString("0.0");
        scoreText.text=score.ToString("0");

        motor=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        highScoreText.text=PlayerPrefs.GetInt("HighScore").ToString();
        interstitial=GameObject.Find("AdsManager").GetComponent<loadInterstitial>();
        shop=GetComponent<Shop>();
        hatlogic=GameObject.Find("Player").GetComponent<HatLogic>();
        
        
        //PlayerTwo.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {   
        if(MobileInput.Instance.Tap && !isGameStarted){
            isGameStarted=true;
            motor.StartRunning();
            
            FindObjectOfType<GlacierSpawner>().IsScrolling=true;
            FindObjectOfType<CameraMotor>().IsMoving=true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");
            highScoreText.gameObject.SetActive(false);
        }
        if(isGameStarted && !isDead){
            
            score+=(Time.deltaTime*modifierScore);
            if(lastScore!=(int)score){
                lastScore=(int)score;
                scoreText.text=score.ToString("0");

            }
            
        }
    }
    
    public void GetCoin(){
        diamondAnim.SetTrigger("Collect");
        score+=coinScoreAmount;
        coinScore++;
        coinText.text=coinScore.ToString("0");
        scoreText.text=score.ToString("0");
    }
    
    public void UpdateModifier(float modifierAmount){
        modifierScore=1.0f+modifierAmount;
        modifierText.text="x"+ modifierScore.ToString("0.0");

    }
    public void OnPlayButton(){
        //interstitial.LoadAd();
        SceneManager.LoadScene("Game");
       
    }
    public void OnDeath(){
        isDead=true;
        
        FindObjectOfType<GlacierSpawner>().IsScrolling=false;
        deadCoinText.text=coinScore.ToString("0");
        deadScoreText.text=score.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        if(score>PlayerPrefs.GetInt("HighScore")){
            float s=score;
            if(s%1==0){
                s+=1;
            }
            PlayerPrefs.SetInt("HighScore",(int)s);
        }
        SaveCoin();
        
    }
    public void SaveCoin(){
        if(coinScore>PlayerPrefs.GetInt("HighCoin")){
            float c=coinScore;
            if(c%1==0){
                c+=1;
            }
            PlayerPrefs.SetInt("HighCoin",(int)c);
        
        }
        coinText.text=coinScore.ToString("0");
    }
    public void OnShopClick(){
        shopAnim.SetTrigger("Fall");
        GameObject.Find("Canvas").SetActive(false);
        shop.PopulateShop();
    }
    public void OnhomeClick(){
        GameObject.Find("CanvasShop").SetActive(false);
        SceneManager.LoadScene("Game");
        
    }

}

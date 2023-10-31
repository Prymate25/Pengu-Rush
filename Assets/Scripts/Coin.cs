using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{   private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable(){
        anim.SetTrigger("Spawn");
    }
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            
            GameManager.Instance.GetCoin();
            anim.SetTrigger("Collected");
            //Destroy(gameObject,1.5f);

        }
        else if(other.CompareTag("Player2")){
            
            GameManager.Instance.GetCoin();
            anim.SetTrigger("Collected");
        }
    }
}

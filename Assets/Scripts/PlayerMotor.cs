using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{   
    private const float laneDistance=2.5f;
    private const float turnSpeed=0.05f;
    private CharacterController controller;
    private Animator anim;
    private float jumpForce=4.0f;
    private float gravity=12.0f;
    private float verticalVelocity;
    private float speed=7.0f;
    private int desiredLane=1;
    private bool isGameActive=false;
    
    private float speedIncreaseLastTick;
    private float speedIncreaseTime=2.5f;
    private float speedIncreaseAmount=0.1f;
    private float originalSpeed=7.0f;
    
    // Start is called before the first frame update
    void Start()
    {   speed=originalSpeed;
        controller=GetComponent<CharacterController>();
        anim=GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(!isGameActive){
            return;
        }
        //speed modifier
        if(Time.time-speedIncreaseLastTick>speedIncreaseTime){
            speedIncreaseLastTick=Time.time;
            speed+=speedIncreaseAmount;
            GameManager.Instance.UpdateModifier(speed-originalSpeed);
        }
        //which lane we should be
        if(MobileInput.Instance.SwipeLeft){
            MoveLane(false);
        }
        if(MobileInput.Instance.SwipeRight){
            MoveLane(true);
        }
        //future position
        Vector3 targetPosition=transform.position.z*Vector3.forward;
        if(desiredLane==0){
            targetPosition+=Vector3.left*laneDistance;
        }
        else if(desiredLane==2){
            targetPosition+=Vector3.right*laneDistance;
        }
        //move delta
        Vector3 moveVector=Vector3.zero;
        moveVector.x=(targetPosition-transform.position).normalized.x*speed;

        bool isGrounded=IsGrounded();
        anim.SetBool("Grounded",isGrounded);
        //calculate y
        if(isGrounded){
            verticalVelocity=-0.1f;
            
            if(MobileInput.Instance.SwipeUp){
                //jump
                anim.SetTrigger("Jump");
                verticalVelocity=jumpForce;
            }
            else if(MobileInput.Instance.SwipeDown){
                StartSliding();
            }
        }
        else{
            verticalVelocity-=(gravity*Time.deltaTime);
            //fast fall
            if(MobileInput.Instance.SwipeDown){
                verticalVelocity=-jumpForce;
            }
        }
        moveVector.y=verticalVelocity;
        moveVector.z=speed;
        //move pengu
        controller.Move(moveVector*Time.deltaTime);
        //rotate pingu
        Vector3 dir=controller.velocity;
        if(dir!=Vector3.zero){
            dir.y=0;
            transform.forward=Vector3.Lerp(transform.forward,dir,turnSpeed);
        }
        
    }
    private void MoveLane(bool goingRight){
        desiredLane += (goingRight)? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane,0,2);
    }

    private bool IsGrounded(){
        Ray groundRay=new Ray(new Vector3(controller.bounds.center.x,(controller.bounds.center.y-controller.bounds.extents.y)+0.2f,controller.bounds.center.z),Vector3.down);
        //Debug.DrawRay(groundRay.origin,groundRay.direction,Color.cyan,1.0f);
        if(Physics.Raycast(groundRay,0.2f+0.1f)){
            return true;
        }
        return false;
    }
    public void StartRunning(){
        isGameActive=true;
        anim.SetTrigger("StartRunning");
    }
    private void StartSliding(){
        anim.SetBool("Sliding",true);
        controller.height/=2;
        controller.center=new Vector3(controller.center.x,controller.center.y/2,controller.center.z);
        Invoke("StopSliding",1.0f);
    }
    private void StopSliding(){
        anim.SetBool("Sliding",false);
        controller.height*=2;
        controller.center=new Vector3(controller.center.x,controller.center.y*2,controller.center.z);
    }
    private void Crash(){
        anim.SetTrigger("Death");
        isGameActive=false;
        GameManager.Instance.OnDeath();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit){
        switch(hit.gameObject.tag){
            case "Obstacle":
                Crash();
                break;
        }
    }
}

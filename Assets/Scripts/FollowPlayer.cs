using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{   
    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform=GameObject.FindGameObjectWithTag("Player").transform;
        //playerTransform2=GameObject.FindGameObjectWithTag("Player2").transform;
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position=Vector3.forward*playerTransform.position.z;
        
        
        
    }
}

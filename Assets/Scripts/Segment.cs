using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{   public int SegId{set;get;}
    public bool transition;
    public int length;
    public int beginY1,beginY2,beginY3;
    public int endY1,endY2,endY3;
    private PieceSpawner[] pieces;

    // Start is called before the first frame update
    void Awake()
    {
        pieces=gameObject.GetComponentsInChildren<PieceSpawner>();
        for(int i=0;i<pieces.Length;i++){
                foreach(MeshRenderer mr in pieces[i].GetComponentsInChildren<MeshRenderer>()){
                    mr.enabled=LevelManager.Instance.showCollider;
                }
                    
                
            }
    }

    // Update is called once per frame
    
    public void Spawn(){
        gameObject.SetActive(true);
        for(int i=0;i<pieces.Length;i++){
            pieces[i].Spawn();
        }
    }
    public void DeSpawn(){
        gameObject.SetActive(false);
        for(int i=0;i<pieces.Length;i++){
            pieces[i].DeSpawn();
        }
    }
}

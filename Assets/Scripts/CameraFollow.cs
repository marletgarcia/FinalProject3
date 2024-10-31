using UnityEngine;

public class CameraFollow : MonoBehaviour

{
    public GameObject player;
    Vector3 pos=Vector3.zero;

    void Start()
    {
         pos=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        pos.x=player.transform.position.x;
        transform.position=pos;
        
    }
}
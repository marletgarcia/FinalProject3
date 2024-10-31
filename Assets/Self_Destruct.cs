using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Self_Destruct : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x-transform.position.x>20)
        {
            Destroy(this.gameObject);
        }
        
    }
}

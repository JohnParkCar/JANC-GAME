using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAround : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if (hor != 0 || vert != 0)
        {
            
            
            player.position += new Vector3(0.02f * hor, 0.02f * vert, 0);
            
            
            
            
        }
    }
}
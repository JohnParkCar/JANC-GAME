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
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                player.position += new Vector3(0.2f * hor, 0.2f * vert, 0);
            }   
            else
            {
                player.position += new Vector3(0.1f * hor, 0.1f * vert, 0);
            }
            
            
            
        }
    }
}
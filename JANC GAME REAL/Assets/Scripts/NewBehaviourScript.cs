using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public Animatior anim;
    public AnimatorController walk;
    public AnimatorController run;
    public AnimatorController falling;
    public AnimatorController rising;
    public AnimatorController slash;
    public AnimatorController idle;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (player.velocity.x != 0)
        {
            if (player.velocity.y == 0)
            {
                anim = walking;
            } else if (player.velocity.y > 0)
            {
                anim = rising;
            }
            else
            {
                anim = falling;
            }
        }
        else
        {
            anim = idle;
        }
    }
}

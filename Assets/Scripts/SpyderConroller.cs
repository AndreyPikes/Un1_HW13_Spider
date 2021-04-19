using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class SpyderConroller : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    private CharacterController playerGrounded;
    private bool death = false;
    private bool landing = false;
    public float deathVelocity;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerGrounded = player.GetComponent<CharacterController>();        
    }

    void Update()
    {
        float v = player.GetComponent<PlayerController>().v; //W S -1;+1
        float h = player.GetComponent<PlayerController>().h; //
        AudioSource sound1 = player.GetComponent<SoundScript>().sound1;
        AudioSource sound2 = player.GetComponent<SoundScript>().sound2;
        AudioSource sound3 = player.GetComponent<SoundScript>().sound3;
        AudioSource sound4 = player.GetComponent<SoundScript>().sound4;

        if ((animator != null) && !death)
        {
            animator.SetFloat("Forward", v); //первое "Forward" из аниматора
            animator.SetFloat("Right", h);

            if (player.GetComponent<PlayerController>().space)
            {
                animator.SetTrigger("Attack"); //название из аниматора
                sound1.Play();
            }
            if (player.GetComponent<PlayerController>().jump && isGrounded)
            {
                animator.SetTrigger("Jump"); //название из аниматора
                Debug.Log("SetTriggerJump");
                sound2.Play();
            }
            if (player.GetComponent<PlayerController>().superAttack && isGrounded)
            {
                animator.SetTrigger("SuperAttack"); //название из аниматора  
                sound3.Play();
            }
            if (player.GetComponent<PlayerController>().bite)
            {
                animator.SetTrigger("Bite"); //название из аниматора                
            }
            if ((playerGrounded.velocity.y == 0))
            {                
                landing = false;
            }
            if ((playerGrounded.velocity.y < -7) && !landing)
            {
                animator.SetTrigger("Landing");
                landing = true;                
            }
            if ((playerGrounded.velocity.y < deathVelocity) && !death)
            {
                animator.SetTrigger("Death");
                death = true;
            }
            isGrounded = playerGrounded.isGrounded; //для сохранения состояния из предыдущего кадра
        }
    }
}

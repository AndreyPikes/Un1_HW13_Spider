using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float speed = 2;
    [SerializeField] private float speed_rotation = 1;
    float jspeed = 0;
    //переменные для передачи управления в SpiderScript для анимации
    public float v;
    public float h;
    public bool jump;
    public bool space;
    public bool bite;
    public bool superAttack;

    
    void Start() 
    {        
        characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        v = Input.GetAxis("Vertical"); //W S -1;+1
        h = Input.GetAxis("Horizontal"); //  
        jump = Input.GetKeyDown(KeyCode.J);
        space = Input.GetKeyDown(KeyCode.Space);
        superAttack = Input.GetKeyDown(KeyCode.K);
        bite = Input.GetKeyDown(KeyCode.B);

        if (characterController.isGrounded)
        {            
            jspeed = 0;
            if (jump)
            {
                jspeed = jumpSpeed;                
            }
        }        
        jspeed += gravity * Time.deltaTime;
        Vector3 jumpMove = new Vector3(0, jspeed * Time.deltaTime, 0);        
        transform.Rotate(0, h * speed_rotation, 0);
        Vector3 forwardMove = -transform.TransformDirection(Vector3.forward) * speed * v;        

        characterController.Move(forwardMove + jumpMove);
    }
}

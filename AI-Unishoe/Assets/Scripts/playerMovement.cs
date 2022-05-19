using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float deathDistance = 0f;
    public CharacterController2D controller;

    float horizontalMove = 0f;

    public float runSpeed = 0.7f;
    bool jump = false;

    private Animator anime;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            anime.SetTrigger("Jumping");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //firing script
        }
        anime.SetBool("running", horizontalMove != 0);
    }

    void FixedUpdate()
    {
        //move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
            
        if(this.transform.position.y <= deathDistance) {
            SceneManager.LoadScene("SampleScene");
        }
    }
}

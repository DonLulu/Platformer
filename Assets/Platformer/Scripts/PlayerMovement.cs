using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Animator animator;
    public Timer timer;
    public int lives;
    public int coins;
    public TMP_Text coinsUI;
    public GameObject camera;
    public float runSpeed = 40f;
    private bool takeOff = false;
    public bool isMarioBig = true;
    public int score;
    public TMP_Text message;
    public bool playerDied = false;



    public TMP_Text scoreUI;
    float horizontalMove = 0f;
    bool jump = false;
	
    // Update is called once per frame
    void Update () 
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Math.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            StartCoroutine(waiterLanding());
        }

        if (coins >= 100)
        {
            lives++;
            coins = 0;
        }

        scoreUI.text = String.Format("{0:0000000}", score);
        coinsUI.text = String.Format("x{0:00}", coins);

    }

    IEnumerator waiterLanding()
    {
        yield return new WaitForSeconds(0.1f);
        takeOff = true;
    }

    public void OnLanding()
    {
        if (takeOff)
        {
            animator.SetBool("IsJumping", false);
            takeOff = false;
        }
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Death")
        {
            print("dead");
            GameOver();
        }
        if (other.tag == "End")
        {
            print("dead");
            End();
        }
    }


    IEnumerator WaiterGameOver()
    {
        yield return new WaitForSeconds(1f);
    }

    public void GameOver()
    {
        timer.timeValue = 100;
        score = 0;
        coins = 0;
        transform.position = new Vector3(12f, 2.07999992f, 0f);
        camera.transform.position = new Vector3(12f, 12f, -10f);
        lives--;
        message.text = "Game Over";
        StartCoroutine(waiterMessage());
        playerDied = true;
    }
    
    public void End()
    {
        timer.timeValue = 100;
        score = 0;
        coins = 0;
        transform.position = new Vector3(12f, 2.07999992f, 0f);
        camera.transform.position = new Vector3(12f, 12f, -10f);
        message.text = "Congratulations";
        StartCoroutine(waiterMessage());
        
    }

    IEnumerator waiterMessage()
    {
        yield return new WaitForSeconds(3f);
        message.text = "";
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playercontroller : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI counttext;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count; 

    private AudioSource myAudio;

    public AudioClip myClip;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCounttext();
        winTextObject.SetActive(false);

        myAudio =   GetComponent<AudioSource>();

        
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCounttext()
    {
        counttext.text = "Count: " + count.ToString();
        if (count >= 12) 
		{
                    // Set the text value of your 'winText'
                    winTextObject.SetActive(true);
                    myAudio.PlayOneShot(myClip);
		}
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCounttext();

            myAudio.Play();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isOnGround = true;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float moveSpeed = 8.0f;
    float boundary = 19.5f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * moveSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * moveSpeed);

        if (transform.position.z > boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundary);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if (transform.position.z < -boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundary);
            playerRdr.material.color = playerMtrs[3].color;
        }
        else if (transform.position.x > boundary)
        {
            transform.position = new Vector3(boundary, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[4].color;
        }
        else if (transform.position.x < -boundary)
        {
            transform.position = new Vector3(-boundary, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[5].color;
        }

        PlayerJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            isOnGround = true;

            playerRdr.material.color = playerMtrs[0].color;
        }
    }

    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerRdr.material.color = playerMtrs[1].color;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float slowSpeed;
    [SerializeField] float boostSpeed;
    [SerializeField] float waitForSlow;
    [SerializeField] float waitForBoost;
    float originalSpeed;
    bool isSlowing;
    bool isBoosting;

    void Start()
    {
        originalSpeed = moveSpeed;
    }
    void Update()
    {
        MovePlayer();
    }

    // This just moves the player
    void MovePlayer()
    {
        float driveFloat = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, driveFloat, 0);

        float turnFloat;

        if (driveFloat >= 0)
        {
            turnFloat = -Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        }
        else
        {
            turnFloat = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        }
        transform.Rotate(0, 0, turnFloat);
    }

    // This slows the player down if they bump into something
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isSlowing)
        {
            StartCoroutine(SlowDown());
        }
    }

    IEnumerator SlowDown()
    {
        isSlowing = true;
        moveSpeed = slowSpeed;
        yield return new WaitForSeconds(waitForSlow);
        moveSpeed = originalSpeed;
        isSlowing = false;
    }
      
    // This gives the player a speed boost if they go through a boost pad
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isBoosting && other.tag == "Boost")
        {
            StartCoroutine(SpeedUp());
        }
    }

    IEnumerator SpeedUp()
    {
        isBoosting = true;
        moveSpeed = boostSpeed;
        yield return new WaitForSeconds(waitForBoost);
        moveSpeed = originalSpeed;
        isBoosting = false;
    }
}

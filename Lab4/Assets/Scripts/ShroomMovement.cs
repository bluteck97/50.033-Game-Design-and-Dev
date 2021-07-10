using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomMovement : MonoBehaviour
{
    private Rigidbody2D shroomBody;
    private Vector2 vector;
    private int moveRight = 1;
    public float shroomSpd;
    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        shroomBody = GetComponent<Rigidbody2D>();
        shroomBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
        CalculateRandom();
        ComputeVelocity();
    }
    void CalculateRandom()
    {
        System.Random random = new System.Random();
        int rnd = random.Next(0,2);
        if (rnd == 0) moveRight = -1;
        else moveRight = 1;
    }
    void ComputeVelocity()
    {
        vector = new Vector2((moveRight)*shroomSpd, 0);
    }

    void moveShroom()
    {
        shroomBody.MovePosition(shroomBody.position + vector * Time.fixedDeltaTime);
    }

    void  OnBecameInvisible(){
        Destroy(gameObject);	
    }

    // Update is called once per frame
    void Update()
    {
        // ComputeVelocity();
        moveShroom();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Hit Obstacle");
            moveRight *= -1;
            ComputeVelocity();
            moveShroom();
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            shroomSpd = 0;
            ComputeVelocity();
            moveShroom();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 10;
    public float upSpeed = 10;
    public Transform enemyLocation;
    public GameObject dustCloud;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRight = true;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }
    void  FixedUpdate()
    {
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            //stop
            speed = 0;
        };

        if (Input.GetKeyDown("a") || Input.GetKeyDown("d"))
        {
            speed = 70;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed){
                marioBody.AddForce(movement * speed);
            }
        }
        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true;
        }   
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goomba"))
        {
            Debug.Log("Collided with Goomba!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles"))
        {
            onGroundState = true;
            countScoreState = false;
            scoreText.text = "Score: " + score.ToString();
            dustCloud.GetComponent<ParticleSystem>().Play();
        };
    }

    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRight){
            faceRight = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRight){
            faceRight = true;
            marioSprite.flipX = false;
        }

        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }

        if (Mathf.Abs(marioBody.velocity.x) < 1.0 && onGroundState)
        {
            marioAnimator.SetTrigger("onSkid");
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
    }
}

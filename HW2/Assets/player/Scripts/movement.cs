using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpSpeed = 1f;
    [HideInInspector] public Vector3 dir;
    float hInput, vInput;
    CharacterController controller;
    Animator a;
    public bool hitted_by_toony = false;
    private int hittedState;
    float hitCount = 1.0f;
    GameObject hit_part;
    bool isdead = false;

    public int HP;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _healSound;
    private AudioSource _audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        hit_part = GameObject.Find("Hit Effect p");
        controller = GetComponent<CharacterController>();
        a = GameObject.Find("MaleCharacterPolyart").GetComponent<Animator>();
        hittedState = Animator.StringToHash("Base Layer.GetHit01_SwordAndShield");
        _audioPlayer = GetComponent<AudioSource>();
        //HP = 100;
        HP = GameManager.previousHP;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.previousHP = HP;
        if ( HP <= 0 )
        {
            a.SetBool( "die", true );
            isdead = true;
        }

        if(controller.isGrounded){
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
            dir = transform.forward * vInput * moveSpeed + transform.right * hInput * moveSpeed;
            
            if(Input.GetKey(KeyCode.Space)){
                dir.y += jumpSpeed;
                a.SetBool( "jump", true );
            }

            else{
                a.SetBool( "jump", false );
                if ( (hInput != 0) || (vInput != 0) ){
                    a.SetBool( "run", true );
                }
                else{
                    a.SetBool( "run", false );
                }
            }
            
        }

        else{
            a.SetBool( "jump", false );
        }

        AnimatorStateInfo currentState = a.GetCurrentAnimatorStateInfo(0);

        if (currentState.fullPathHash == hittedState){
            //print("test");
            a.SetBool( "hit", false );
        }

        if ( hitted_by_toony && Time.time >= hitCount )
        {
            hitCount = Time.time + 1.0f;
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                HP = HP - 5;
                hit_part.GetComponent<ParticleSystem>().Play();
                _audioPlayer.PlayOneShot(_hitSound);
            }
            else
            {
                a.SetBool( "die", true );
                isdead = true;
            }
        }



        dir += Physics.gravity * Time.deltaTime;
        controller.Move(dir * Time.deltaTime);


        // Pasue Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.pause();
        }
        a.speed = (GameManager.state == GameState.Pause) ? 0 : 1;

        // check for y position
        if (transform.position.y <= -10){
            isdead = true;
        }

        // if dead
        if (isdead) {
            gameObject.GetComponent<movement>().enabled = false;
            GameManager.gameOver();
        }

    }

    void OnTriggerEnter( Collider other )
    {
        if ( other.gameObject.name == "little_boom(Clone)" )
        {
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                HP = HP - 5;
                hit_part.GetComponent<ParticleSystem>().Play();
                _audioPlayer.PlayOneShot(_hitSound);
            }
            else
            {
                a.SetBool( "die", true );
                isdead = true;
            }
        }

        else if ( other.gameObject.tag == "heart" )
        {
            int temp = HP + 30;
            Destroy(other.gameObject);
            if ( temp > 100 )
            {
                HP = 100;
                _audioPlayer.PlayOneShot(_healSound);
            }

            else
            {
                HP = temp;
                _audioPlayer.PlayOneShot(_healSound);
            }
        }

        else if(other.gameObject.name == "door")
        {
            GameManager.nextStage(HP);
        }
    }

    void OnTriggerStay( Collider other )
    {

        if ( (other.gameObject.tag == "Enemies") && (Time.time >= hitCount) && (other.gameObject.name != "little_boom(Clone)") )
        {
            //print("hit");
            hitCount = Time.time + 1.0f;
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                hit_part.GetComponent<ParticleSystem>().Play();
                HP = HP - 5;
                _audioPlayer.PlayOneShot(_hitSound);
            }
            else
            {
                a.SetBool( "die", true );
                isdead = true;
            }
            //hitted = true;
        }

        
        
    }

    void OnParticleCollision(GameObject other)
    {
        //print( "pc2" );
        if ( other.gameObject.name == "blue_att" )
        {
            //print("hit");
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                hit_part.GetComponent<ParticleSystem>().Play();
                HP = HP - 5;
                _audioPlayer.PlayOneShot(_hitSound);
            }
            else
            {
                a.SetBool( "die", true );
                isdead = true;
            }
        }
    }


}

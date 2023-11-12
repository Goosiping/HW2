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

    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        a = GameObject.Find("MaleCharacterPolyart").GetComponent<Animator>();
        hittedState = Animator.StringToHash("Base Layer.GetHit01_SwordAndShield");
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if ( HP <= 0 )
        {
            a.SetBool( "die", true );
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
            }
            else
            {
                a.SetBool( "die", true );
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
    }

    void OnTriggerEnter( Collider other )
    {
        if ( other.gameObject.name == "little_boom(Clone)" )
        {
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                HP = HP - 5;
            }
            else
            {
                a.SetBool( "die", true );
            }
        }

        else if ( other.gameObject.name == "Heart" )
        {
            int temp = HP + 30;
            Destroy(other.gameObject);
            if ( temp > 100 )
            {
                HP = 100;
            }

            else
            {
                HP = temp;
            }
        }
    }

    void OnTriggerStay( Collider other )
    {

        if ( other.gameObject.name == "TT_RTS_Demo_Character Variant" && Time.time >= hitCount )
        {
            //print("hit");
            hitCount = Time.time + 1.0f;
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                HP = HP - 5;
            }
            else
            {
                a.SetBool( "die", true );
            }
            //hitted = true;
        }

        else if ( other.gameObject.name == "PolyArtWizardMaskTintMat Variant" && Time.time >= hitCount )
        {
            //print("hit");
            hitCount = Time.time + 1.0f;
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                HP = HP - 5;
            }
            else
            {
                a.SetBool( "die", true );
            }
        }

        else if ( other.gameObject.name == "PolyArtWizardStandardMat Variant" && Time.time >= hitCount )
        {
            //print("hit");
            hitCount = Time.time + 1.0f;
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                HP = HP - 5;
            }
            else
            {
                a.SetBool( "die", true );
            }
        }
        
    }

    void OnParticleCollision(GameObject other)
    {
        print( "pc2" );
        if ( other.gameObject.name == "blue_att" )
        {
            //print("hit");
            if ( HP > 0 )
            {
                a.SetBool( "hit", true );
                HP = HP - 5;
            }
            else
            {
                a.SetBool( "die", true );
            }
        }
    }

    
}

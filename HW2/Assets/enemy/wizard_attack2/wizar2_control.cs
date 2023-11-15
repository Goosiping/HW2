using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wizar2_control : MonoBehaviour, IDestroyable
{
    GameObject wizar2;
    public Slider blood;
    float speed = 0.1f;
    float angle = 100.0f;
    float timer = 5;
    float count = 0;
    Animator a;
    private int hittedState;
    GameObject part;
    GameObject hit_part;
    // Start is called before the first frame update
    void Start()
    {
        part = GameObject.Find("blue_att");
        hit_part = GameObject.Find("Hit Effect2");
        a = gameObject.GetComponent<Animator>();
        hittedState = Animator.StringToHash("Base Layer.GetHit");
        wizar2 = gameObject;
        Walk();
        blood.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        count = count + Time.deltaTime;
        if ( blood.value > 0 )
        {

            AnimatorStateInfo currentState = a.GetCurrentAnimatorStateInfo(0);

            if (currentState.fullPathHash == hittedState){
                a.SetBool( "hit", false );
                Turn();
                Walk();
            }

            else
            {
                Turn();
                Walk();
            }
        }

        else
        {
            a.SetBool( "die", true );
            part.GetComponent<particle_control>().Des();
            Destroy(gameObject,3);
            GameManager.checkNextStage();
        }
        
    }

    void Turn()
    {
        if ( count > timer )
        {
            wizar2.transform.Rotate(0, angle, 0, Space.Self);
            count = 0;
            timer = Random.Range(0, 5);
            angle = Random.Range(90, 180);
        }
    }

    void Walk()
    {
        Vector3 v;
        v = Vector3.forward * speed;
        wizar2.transform.Translate(v*Time.fixedDeltaTime, Space.Self);

    }

    public void damage(int damage_value)
    {
        a.SetBool( "hit", true );
        float d = (float)damage_value / 100f;
        blood.value = blood.value - d;
        hit_part.GetComponent<ParticleSystem>().Play();
    }
}

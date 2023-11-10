using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    [SerializeField] public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.state != GameState.Pause)
        {
            int current = (int)(Time.time - GameManager.startTime);
            text.text = string.Format("{0}:{1:00}", current / 60, current % 60);
        }
    }
}

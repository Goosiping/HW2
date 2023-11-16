using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class finishTime : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_Text text;
    public void backToMenu()
    {
        GameManager.audioSourceBGM.Stop();
        GameManager.backToMenu();
    }
    void Start()
    {
        int current = (int)(GameManager.finishTime);
        text.text = string.Format("{0}:{1:00}", current / 60, current % 60);
    }
}

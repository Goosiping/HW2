using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passDoorScript : MonoBehaviour
{
    private GameObject _particleSystem;
    private GameObject _door;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = transform.GetChild(0).gameObject;
        _door = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _particleSystem.SetActive(GameManager.isPass());
        _door.SetActive(GameManager.isPass());
    }
}

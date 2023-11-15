using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _tutorial;
    private MenuPage currentPage;

    // Start is called before the first frame update
    void Start()
    {
        currentPage = MenuPage.Menu;
        _menu.SetActive(true);
        _tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tutorialPage()
    {
        currentPage = MenuPage.Tutorial;
        _menu.SetActive(false);
        _tutorial.SetActive(true);
    }
    public void backToMenu()
    {
        currentPage = MenuPage.Menu;
        _menu.SetActive(true);
        _tutorial.SetActive(false);
    }
}

public enum MenuPage
{
    Menu,
    Tutorial
}
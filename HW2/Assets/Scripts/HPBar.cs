using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Text _HpText;
    [SerializeField] private GameObject _HpBar;
    [SerializeField] private GameObject _player;

    private movement _playerInfo;
    private Image _HpImage;

    // Start is called before the first frame update
    void Start()
    {
        _playerInfo = _player.GetComponent<movement>();
        _HpImage = _HpBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Change Text
        _HpText.text = _playerInfo.HP.ToString() + "%";

        // Change HP Bar Image
        float ratio = (float)_playerInfo.HP / 100;
        _HpImage.fillAmount = ratio;
        _HpImage.color = new Color((1 - ratio) * 255, ratio * 255, 0, 255);
    }
}

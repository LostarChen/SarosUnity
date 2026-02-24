using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _Name;
    [SerializeField] private Slider _Slider;
    // Start is called before the first frame update
    public void InitPlayerUI(string name)
    { 
        _Name.text = name;
    }
    public void UpdateHealth(float damage)
    {
        _Slider.value += damage;
    }
}

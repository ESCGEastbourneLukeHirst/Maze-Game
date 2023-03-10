using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficulty : MonoBehaviour
{
    [SerializeField] public Text difficultyChange;

    void Start()
    {
        difficultyChange.text = PlayerPrefs.GetString("DifficultySave").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

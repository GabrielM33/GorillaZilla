using System;
using System.Collections;
using System.Collections.Generic;
using GorillaZilla;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        GameObject.FindObjectOfType<GameManager>().StartGame();
    }
}

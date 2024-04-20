using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GorillaZilla
{
    public class SettingsMenu : MonoBehaviour
    {
        public Toggle tgl_Music;
        public Toggle tgl_ShowDebug;
        public Button btn_RemapRoom;
        private void Start()
        {
            tgl_Music.onValueChanged.AddListener(OnMusicValueChanged);
            tgl_ShowDebug.onValueChanged.AddListener(OnShowDebugValueChanged);
            btn_RemapRoom.onClick.AddListener(OnRemapRoomClicked);
        }
        private void OnRemapRoomClicked()
        {
            var sceneManager = GameObject.FindObjectOfType<OVRSceneManager>();
            sceneManager.RequestSceneCapture();
        }
        private void OnShowDebugValueChanged(bool isOn)
        {
            PlayerSettings.ShowDebug = isOn;
        }
        private void OnMusicValueChanged(bool isOn)
        {
            PlayerSettings.MuteMusic = isOn;
        }
    }

}

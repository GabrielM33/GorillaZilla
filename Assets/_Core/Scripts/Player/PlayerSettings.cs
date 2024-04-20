using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GorillaZilla
{
    public class PlayerSettings
    {
        public static bool MuteMusic
        {
            get
            {
                return PlayerPrefs.GetInt("MuteMusic", 1) == 1;
            }
            set
            {
                PlayerPrefs.SetInt("MuteMusic", value ? 1 : 0);
            }
        }
        public static bool ShowDebug
        {
            get
            {
                return PlayerPrefs.GetInt("ShowDebug", 0) == 1;
            }
            set
            {
                PlayerPrefs.SetInt("ShowDebug", value ? 1 : 0);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Settings
{
    private static bool isMuted = false;
    private static bool isVfxActive = true;

    public static bool IsMuted
    {
        get
        {
            return isMuted;
        }
        set
        {
            isMuted = value;
            PlayerPrefs.SetInt("mute", Convert.ToInt32(isMuted));
            PlayerPrefs.Save();
            AudioListener.volume = isMuted ? 0.0f : 1.0f;
        }
    }

    public static bool IsVfxActive
    {
        get
        {
            return isVfxActive;
        }
        set
        {
            isVfxActive = value;
            PlayerPrefs.SetInt("vfx", Convert.ToInt32(isVfxActive));
            PlayerPrefs.Save();
        }
    }
}

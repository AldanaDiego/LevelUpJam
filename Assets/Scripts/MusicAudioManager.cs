using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioManager : Singleton<MusicAudioManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}

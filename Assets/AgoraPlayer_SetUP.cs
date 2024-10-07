using Agora.Rtc;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class AgoraPlayer_SetUP : NetworkBehaviour
{
    public RawImage playerStream;
    public VideoSurface videoSurface;
    public string channel_ID="pikamoon";
    [Networked]
    public uint agoraStreaming_UID { get; set; } = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (HasStateAuthority == false)
        {
            videoSurface.SetForUser(agoraStreaming_UID, channel_ID, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
            videoSurface.SetEnable(true);
        }
        else
        {
            GameManager.instance.gameObject.GetComponent<AgoraChat>().PreviewSelf();
            videoSurface.SetForUser(0, "");
            // Start rendering video
            //   LocalView.SetEnable(true);
            videoSurface.SetEnable(true);
            //   gameObject.GetComponent<AgoraPlayer_SetUP>().enabled = false;
        }
    }
    public void StreamingCall()
    {
        if (HasStateAuthority == false)
        {
            videoSurface.SetForUser(agoraStreaming_UID, channel_ID, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
            videoSurface.SetEnable(true);
        }
        else
        {
            print("You Are local ");
        }
    }


}

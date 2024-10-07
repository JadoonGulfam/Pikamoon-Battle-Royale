using Agora.Rtc;
using Fusion;
using NanoSockets;
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

    public bool videoStreamStatus;
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
            GameManager.instance.transform.GetChild(0).GetComponent<AgoraChat>().PreviewSelf();
            videoSurface.SetForUser(0, "");
            // Start rendering video
            //   LocalView.SetEnable(true);
            videoSurface.SetEnable(true);
            //   gameObject.GetComponent<AgoraPlayer_SetUP>().enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
           StreamingCall(true);
        else if (Input.GetKeyDown(KeyCode.V))
                 StreamingCall(false);
    }
    public void StreamingCall(bool status)
    {
        videoStreamStatus = status;
        VideoStream(status);
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void VideoStream(bool status)
    {
        if (HasStateAuthority == false)
        {
            videoSurface.SetForUser(agoraStreaming_UID, channel_ID, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
            videoSurface.SetEnable(status);
        }
        else
        {
            if (status)
            
                GameManager.instance.transform.GetChild(0).GetComponent<AgoraChat>().PreviewSelf();
            
            else
            
                GameManager.instance.transform.GetChild(0).GetComponent<AgoraChat>().PreviewSelfOFF();
            
            videoSurface.SetForUser(0, "");
            
            videoSurface.SetEnable(status);
          //  }
        }
    }


}

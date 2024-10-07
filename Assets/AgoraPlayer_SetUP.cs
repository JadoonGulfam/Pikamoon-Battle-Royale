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
    }
    private void Update()
    {

        if (HasStateAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            //   StreamingCall(true);
            {
             //   Streamingstatus = true;
                DealDamageRpc(true);
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {

              //  Streamingstatus = false;
                DealDamageRpc(false);
            }
        }
    }
    public void StreamingCall(bool status)
    {
        videoStreamStatus = status;
      //  VideoStream(status);
    }


    // [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
   // [Rpc(RpcSources.All, RpcTargets.All)]
    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void DealDamageRpc(bool status)
    {
        if(status)
        {
            VideoStreamON();
        }
        else
            VideoStreamOFF();
    }

    bool Streamingstatus;
    //  [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void VideoStreamON()
    {
    
        if(HasInputAuthority)
        {
         //   if (Streamingstatus)
            
                GameManager.instance.transform.GetChild(0).GetComponent<AgoraChat>().PreviewSelf();
          //  else
            //    GameManager.instance.transform.GetChild(0).GetComponent<AgoraChat>().PreviewSelfOFF();
            
            videoSurface.SetForUser(0, "");
            videoSurface.SetEnable(true);

            print("i am on state authority On");
        }
        else
        {
            videoSurface.SetForUser(agoraStreaming_UID, channel_ID, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
            videoSurface.SetEnable(true);
            print("i am on client");
            // return 0;
        }
    }
    public void VideoStreamOFF()
    {

        if (HasInputAuthority)
        {
            
                GameManager.instance.transform.GetChild(0).GetComponent<AgoraChat>().PreviewSelfOFF();

            videoSurface.SetForUser(0, "");
            videoSurface.SetEnable(false);
            print("i am on state authority OFF");
        }
        else
        {
            print("i am on client Off");
            videoSurface.SetForUser(agoraStreaming_UID, channel_ID, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
            videoSurface.SetEnable(false);

            // return 0;
        }
    }

}

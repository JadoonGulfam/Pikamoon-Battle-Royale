using System;
using System.Collections;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Constants Instance;
    public GameObject _curretClickedBtn;
    public GameObject _lastAvatarClickedBtn;
    public static Func<string, bodyType,GameObject, IEnumerator> downloadAddressableObject;
    public static Func<string, bodyType,GameObject, IEnumerator> downloadAddressableTexture;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

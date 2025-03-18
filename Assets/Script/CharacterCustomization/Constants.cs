using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class Constants : MonoBehaviour
{
    public static Constants Instance;
    public GameObject _curretClickedBtn;
    public GameObject _lastAvatarClickedBtn;
    public static Func<string, BodyType, GameObject, bool, IEnumerator> downloadAddressableObject;
    public static Func<string, BodyType, GameObject, Task> downloadAddressableTexture;
    public static Action getColorObject;
    public static Action resetBlendShapes;
    public BodyType bodyType;
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
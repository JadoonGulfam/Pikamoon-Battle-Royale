using Cinemachine;
using SickscoreGames.HUDNavigationSystem;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCameraRoot;
    public int myCharacterindex { get; set; } = 0;
    public GameObject[] characters;
    public GameObject virtualCamera;
    Button Destroypika;
    Button[] PikaButtons = new Button[6];
    void Start()
    {
        GetComponent<HNSPlayerController>().enabled = true;
        myCharacterindex = GameManager.instance.myCharacter;
        GameObject myPlayerAvatar = Instantiate(characters[myCharacterindex], gameObject.transform);
        GetComponent<Animator>().avatar = myPlayerAvatar.GetComponent<Animator>().avatar;

        virtualCamera = GameObject.Find("PlayerFollowCamera");
        virtualCamera.GetComponent<CinemachineFreeLook>().Follow = playerCameraRoot;
        virtualCamera.GetComponent<CinemachineFreeLook>().LookAt = playerCameraRoot;

        // Temp button for pika to spawn in environment
        //Transform temp = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1);
        //Destroypika = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();
        //Destroypika.onClick.AddListener(DeSpawnPikamoon);

        //for (int i = 0; i < PikaButtons.Length; i++)
        //{
        //    var x = i;
        //    PikaButtons[x] = temp.transform.GetChild(x).GetComponent<Button>();
        //    PikaButtons[x].onClick.AddListener(delegate { Spawn_PikaMoon(x); });
        //}
    }

    void DeSpawnPikamoon()
    {
        //foreach (NetworkObject obj in pikaMoon_CharacterList)
        //{
        //    Destroy(obj.gameObject);
        //}
        //pikaMoon_CharacterList.Clear();
    }
    void Spawn_PikaMoon(int pika)
    {
        //NetworkObject temp;
        //switch (pika)
        //{
        //    case 0:// "Barkian":
        //        temp = Runner.Spawn(PikaMoon_Barkian, transform.position, Quaternion.identity);
        //        temp.GetComponent<BarkianPlayerFollowAI>().followMaster = this.transform;
        //        pikaMoon_CharacterList.Add(temp);
        //        break;
        //    case 1:// "Blazeving":
        //        temp = Runner.Spawn(PikaMoon_Blazeving, transform.position, Quaternion.identity);
        //        temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
        //        pikaMoon_CharacterList.Add(temp);
        //        break;
        //    case 2:// "Sylvolt":
        //        temp = Runner.Spawn(PikaMoon_Sylvolt, transform.position, Quaternion.identity);
        //        // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
        //        pikaMoon_CharacterList.Add(temp);
        //        break;
        //    case 3:// "Dracodilla":
        //        temp = Runner.Spawn(PikaMoon_Dracodilla, transform.position, Quaternion.identity);
        //        // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
        //        pikaMoon_CharacterList.Add(temp);
        //        break;
        //    case 4:// "Soarcrow":
        //        temp = Runner.Spawn(PikaMoon_Soarcrow, transform.position, Quaternion.identity);
        //        // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
        //        pikaMoon_CharacterList.Add(temp);
        //        break;
        //    case 5:// "Torrentar":
        //        temp = Runner.Spawn(PikaMoon_Torrentar, transform.position, Quaternion.identity);
        //        // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
        //        pikaMoon_CharacterList.Add(temp);
        //        break;
        //}
        //temp = null;
    }
    void Update()
    {

    }
}

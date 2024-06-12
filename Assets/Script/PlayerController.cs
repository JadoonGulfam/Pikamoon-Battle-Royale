using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] Transform playerCameraRoot;
    // Start is called before the first frame update
    public TMP_Text playerName;
    public GameObject PikaMoon_Barkian, PikaMoon_Blazeving,PikaMoon_Sylvolt,PikaMoon_Dracodilla,PikaMoon_Soarcrow,PikaMoon_Torrentar;
    public GameObject canvasData;
    Button Destroypika;
    Button[] PikaButtons=new Button[6];
    public GameObject[] characters;
   // public NetworkObject myPikamoon;
    public List<NetworkObject> pikaMoon_CharacterList;
    [Networked]
    public string userName { get; set; } = " ";
    [Networked]
    public int myCharacterindex { get; set; } = 0;

   // [Networked, OnChangedRender(nameof(HealthChanged))]
   // public int myHealth { get; set; } = 100;
    
    public GameObject virtualCamera;
    IEnumerator Start()
    {
        canvasData.GetComponent<LookAtConstraint>().rotationOffset = new Vector3(-180, 0, 180);
        ConstraintSource sc = new ConstraintSource();
        sc.weight = 1.0f;
        sc.sourceTransform = Camera.main.transform;
        canvasData.GetComponent<LookAtConstraint>().SetSource(0, sc);
      //  HealthChanged();
        // AttackButton.onClick.AddListener(DealDamageRpc);


        if (HasStateAuthority == false)
        {
            yield return new WaitForSeconds(1);
            playerName.text = userName;
            GameObject myPlayerAvatar = Instantiate(characters[myCharacterindex], gameObject.transform);
            GetComponent<Animator>().avatar = myPlayerAvatar.GetComponent<Animator>().avatar;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<CameraScrollZoom>().enabled = false;
        }
        else
        {
            myCharacterindex = GameManager.instance.myCharacter;
            GameObject myPlayerAvatar = Instantiate(characters[myCharacterindex], gameObject.transform);
            GetComponent<Animator>().avatar = myPlayerAvatar.GetComponent<Animator>().avatar;

            playerName.text = GameManager.instance._playerName;
            virtualCamera = GameObject.Find("PlayerFollowCamera");
            virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;
            GetComponent<ThirdPersonController>().enabled = true;
            GetComponent<PlayerInput>().enabled = true;
            userName = GameManager.instance._playerName;
            //   StartCoroutine(SpawnTest());// userName);

            // Temp button for pika to spawn in environment
            Transform temp = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1);
            Destroypika = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();
            Destroypika.onClick.AddListener(DeSpawnPikamoon);

            for (int i = 0; i < PikaButtons.Length; i++)
            {
                var x = i;
                PikaButtons[x] = temp.transform.GetChild(x).GetComponent<Button>();
                PikaButtons[x].onClick.AddListener(delegate { Spawn_PikaMoon(x); });
            }
        }
        canvasData.SetActive(true);
    }

   // void HealthChanged()
  //  {
       // AttackButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = myHealth.ToString();
        //   Debug.Log($"Health changed to: {NetworkedHealth}");
   // }
    //[Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    void DeSpawnPikamoon()
    {
        //   yield return new WaitForSeconds(5f);
        foreach (NetworkObject obj in pikaMoon_CharacterList)
        {
            Destroy(obj.gameObject);
            // Update is called once per frame
        }
        pikaMoon_CharacterList.Clear();
    }

  //  [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
  //  public void DealDamageRpc()
  //  {
        // The code inside here will run on the client which owns this object (has state and input authority).
    //    Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
    //    myHealth = myHealth - 1;//  damage;
       // StartCoroutine(DeSpawnPikamoon());
   // }

  /*  [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    void Test2ndRPC()
    {
        print("Test");
    }*/
    void Spawn_PikaMoon(int pika)
    {
        NetworkObject temp;
        switch (pika)
        {
            case 0:// "Barkian":
                temp = Runner.Spawn(PikaMoon_Barkian, transform.position, Quaternion.identity);
                temp.GetComponent<BarkianPlayerFollowAI>().followMaster = this.transform;
                pikaMoon_CharacterList.Add(temp);
                break;
            case 1:// "Blazeving":
                temp = Runner.Spawn(PikaMoon_Blazeving, transform.position, Quaternion.identity);
                temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
                pikaMoon_CharacterList.Add(temp);
                break;
            case 2:// "Sylvolt":
                temp = Runner.Spawn(PikaMoon_Sylvolt, transform.position, Quaternion.identity);
               // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
                pikaMoon_CharacterList.Add(temp);
                break;
            case 3:// "Dracodilla":
                temp = Runner.Spawn(PikaMoon_Dracodilla, transform.position, Quaternion.identity);
               // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
                pikaMoon_CharacterList.Add(temp);
                break;
            case 4:// "Soarcrow":
                temp = Runner.Spawn(PikaMoon_Soarcrow, transform.position, Quaternion.identity);
               // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
                pikaMoon_CharacterList.Add(temp);
                break;
            case 5:// "Torrentar":
                temp = Runner.Spawn(PikaMoon_Torrentar, transform.position, Quaternion.identity);
               // temp.GetComponent<BlazewingPlayerFollowAI>().player = this.transform;
                pikaMoon_CharacterList.Add(temp);
                break;
        }
                temp = null;
    }
}

using Pikamoon.UI;
using UnityEngine;


namespace Pikamoon.Controller
{
    public class ReferencesHolder : MonoBehaviour
    {
        public static ReferencesHolder Instance;

        public GameObject PlayerPrefab;

        public PlayerController _playerController;
        [Space]
        [Space]
        public PlayerInput _playerInput;

        public CameraController _cameraController;

        public UIManagerSK _uiManager;

        public Transform[] _SpawnPoint;

        void Awake()
        {
            Instance = this;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            CamefromMPCAll = false;

        }
        [SerializeField] bool CamefromMPCAll;
        private void Start()
        {
            if (!CamefromMPCAll)
                InstantiatePlayer();
        }

        void InstantiatePlayer()
        {
            if (_playerController == null)
            {
                GameObject GO = Instantiate(PlayerPrefab) as GameObject;
                _playerController = GO.GetComponent<PlayerController>();
            }

            _playerController.transform.position = _SpawnPoint[0].position;
            _playerController.transform.rotation = _SpawnPoint[0].rotation;

            _cameraController.AssignPlayer(_playerController.transform, _playerController.Head);

            _playerController.Inititalize(_playerInput,_cameraController, _uiManager);
        }

        public void InstantiatePlayer(GameObject GO, int i)
        {
            CamefromMPCAll = true;

            _playerController = GO.GetComponent<PlayerController>();

            _playerController.transform.position = _SpawnPoint[i].position;
            _playerController.transform.rotation = _SpawnPoint[i].rotation;

            _cameraController.AssignPlayer(_playerController.transform, _playerController.Head);

            _playerController.Inititalize(_playerInput, _cameraController, _uiManager);
        }

        public void InstantiatePlayerFromMultiplayer(GameObject GO)
        {


            _playerController = GO.GetComponent<PlayerController>();

            if(!_playerController.GetComponent<PlayerSetupForMultiplayer>().isMinePlayer)
            {
                return;
            }
            
            
            _playerController.transform.position = _SpawnPoint[0].position;
            _playerController.transform.rotation = _SpawnPoint[0].rotation;

            _cameraController.AssignPlayer(_playerController.transform, _playerController.Head);

            _playerController.Inititalize(_playerInput, _cameraController, _uiManager);
        }



        private void OnApplicationFocus(bool focus)
        {
            //Cursor.visible = !focus;
            //Cursor.lockState = focus ? CursorLockMode.Locked:CursorLockMode.None;
        }
    }


}


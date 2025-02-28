using Pikamoon.UI;
using UnityEngine;


namespace Pikamoon.Controller
{
    public class ReferencesHolder : MonoBehaviour
    {
        public static ReferencesHolder Instance;

        public PlayerInput _playerInput;

        public GameObject PlayerPrefab;

       public PlayerController _playerController;

        public CameraController _cameraController;

        public HUDController _hudController;

        [SerializeField] Transform _SpawnPoint;

        void Awake()
        {
            Instance = this;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            CamefromMPCAll = false;

        }
        bool CamefromMPCAll;
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

            _playerController.transform.position = _SpawnPoint.position;
            _playerController.transform.rotation = _SpawnPoint.rotation;

            _cameraController.AssignPlayer(_playerController.transform, _playerController.Head);

            _playerController.Inititalize(_playerInput,_cameraController, _hudController);
        }

        public void InstantiatePlayer(GameObject GO)
        {
            CamefromMPCAll = true;

            _playerController = GO.GetComponent<PlayerController>();

            _playerController.transform.position = _SpawnPoint.position;
            _playerController.transform.rotation = _SpawnPoint.rotation;

            _cameraController.AssignPlayer(_playerController.transform, _playerController.Head);

            _playerController.Inititalize(_playerInput, _cameraController, _hudController);
        }

        public void InstantiatePlayerFromMultiplayer(GameObject GO)
        {


            _playerController = GO.GetComponent<PlayerController>();

            if(!_playerController.GetComponent<PlayerSetupForMultiplayer>().isMinePlayer)
            {
                return;
            }
            
            
            _playerController.transform.position = _SpawnPoint.position;
            _playerController.transform.rotation = _SpawnPoint.rotation;

            _cameraController.AssignPlayer(_playerController.transform, _playerController.Head);

            _playerController.Inititalize(_playerInput, _cameraController, _hudController);
        }



        private void OnApplicationFocus(bool focus)
        {
            Cursor.visible = !focus;
            Cursor.lockState = focus ? CursorLockMode.Locked:CursorLockMode.None;
        }
    }


}


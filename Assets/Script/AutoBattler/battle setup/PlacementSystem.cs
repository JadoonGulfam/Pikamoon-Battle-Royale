using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem Instance { get; private set; }
    public static event Action OnPlacementComplete;

    [SerializeField]
    private GameObject mouseIndicator; //, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private ObjectDatabaseSO database;
    private int selectedObjectIndex = -1;
    [SerializeField]
    private GameObject gridVisualization;
    [SerializeField]
    private AutoBattlerUIManager autoBattlerUIManager;
    [SerializeField]
    private PreViewSystem preview;
    private GridData floorData, objectData;

    private List<GameObject> placedGameObjects = new();
    private Vector3 lastDetectedPosition = Vector3.zero;

    private bool isPreviewEnabled = false;
    private int userPlacedItemsCount=0;
    private int maxItemsToPlace=5;

    public List<GameObject> aIPikas = new List<GameObject>();
    public List<GameObject> playerPika = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StopPlacement();
        floorData = new();
        objectData = new();
       // StartCoroutine(AIPlaceObjects());
    }

    public void StartPlacement()
    {
        StartCoroutine(AIPlaceObjects());
    }

    private IEnumerator AIPlaceObjects()
    {
        float duration = 5f;
        int itemsToPlace = 5;
        float endTime = Time.time + duration;
        int placedItems = 0;

        while (Time.time < endTime && placedItems < itemsToPlace)
        {
            int randomIndex = UnityEngine.Random.Range(0, database.objectData.Count);
            Vector3Int randomPosition = new Vector3Int(
                UnityEngine.Random.Range(-4, 5), // X range: -5 to 5
                0,
                UnityEngine.Random.Range(1, 5)  // Z range: 0 to 5
            );

            if (CheckPlacementValidity(randomPosition, randomIndex))
            {
                PlaceStructureAt(randomIndex, randomPosition, true);
                placedItems++;
            }

            yield return new WaitForSeconds(0.5f);
        }
        autoBattlerUIManager.StartPlayerTeamSelection();


    }

    public void StartPlacement(int ID)
    {
        if (userPlacedItemsCount >= maxItemsToPlace)
        {
            Debug.Log("Maximum number of items placed.");
            autoBattlerUIManager.BattleInProgressPanel();
            autoBattlerUIManager.TeamSelectionCompleted();
            OnPlacementComplete?.Invoke();
            return;
        }
        isPreviewEnabled = true;
        StopPlacement();
        selectedObjectIndex = database.objectData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            print($"No ID found {ID}");
        }
        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(
            database.objectData[selectedObjectIndex].Prefab,
            database.objectData[selectedObjectIndex].Size
        );
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (isPreviewEnabled)
        {
            if (inputManager.IsPointerOverUI())
            {
                return;
            }
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            if (CheckPlacementValidity(gridPosition, selectedObjectIndex))
            {
                PlaceStructureAt(selectedObjectIndex, gridPosition, false);
                isPreviewEnabled = false;
            }
        }
    }

    private void PlaceStructureAt(int objectIndex, Vector3Int gridPosition, bool isAIPlacement)
    {
        GameObject gameObject = Instantiate(database.objectData[objectIndex].Prefab);
        Vector3 cellWorldPosition = grid.CellToWorld(gridPosition);

        gameObject.transform.position = new Vector3(cellWorldPosition.x, 0, cellWorldPosition.z);

        // Initialize the CharacterController script with the appropriate opponent list
        CharacterController characterController = gameObject.GetComponent<CharacterController>();

        if (isAIPlacement)
        {
            gameObject.transform.Rotate(0, 180, 0);
            aIPikas.Add(gameObject);
            gameObject.GetComponent<PikamoonController>().isAIPikamood = true;
          
        }
        else
        {
            userPlacedItemsCount++;
            playerPika.Add(gameObject);
            gameObject.GetComponent<PikamoonController>().isAIPikamood = false;
        }

        preview.StopShowingPreView();
        placedGameObjects.Add(gameObject);
        GridData selectData = database.objectData[objectIndex].ID == -1 ? floorData : objectData;

        selectData.AddOjectAt(
            gridPosition,
            database.objectData[objectIndex].Size,
            database.objectData[objectIndex].ID,
            placedGameObjects.Count - 1
        );
    }


    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectData = database.objectData[selectedObjectIndex].ID == -1 ? floorData : objectData;

        return selectData.CanPlaceObjectAt(gridPosition, database.objectData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPreView();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3.zero;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition && isPreviewEnabled)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            mouseIndicator.transform.position = new Vector3(mousePosition.x, 0, mousePosition.z);
            Vector3 cellWorldPosition = grid.CellToWorld(gridPosition);
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            lastDetectedPosition = gridPosition;
        }
    }
}


#region ____________________________mvvm__________________
//using System;
//using UnityEngine;

//public class PlacementSystem : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject mouseIndicator, cellIndicator, gridVisualization;
//    [SerializeField]
//    private InputManager inputManager;
//    [SerializeField]
//    private ObjectDatabaseSO database;
//    [SerializeField]
//    private GridWrapper grid;

//    private PlacementSystemViewModel viewModel;

//    private void Start()
//    {
//        viewModel = new PlacementSystemViewModel(database, grid);
//        viewModel.OnMouseIndicatorMove += MoveMouseIndicator;
//        viewModel.OnCellIndicatorMove += MoveCellIndicator;
//        viewModel.OnObjectPlaced += ObjectPlaced;
//        viewModel.OnPlacementStopped += StopPlacementVisuals;

//        inputManager.OnClicked += () => viewModel.PlaceStructure(inputManager.GetSelectedMapPosition(), inputManager.IsPointerOverUI());

//        inputManager.OnExit += viewModel.StopPlacement;

//        StopPlacementVisuals();
//    }

//    private void Update()
//    {
//        if (viewModel != null)
//        {
//            viewModel.UpdateMousePosition(inputManager.GetSelectedMapPosition());
//        }
//    }

//    public void StartPlacement(int ID)
//    {
//        viewModel.StartPlacement(ID);
//        gridVisualization.SetActive(true);
//        cellIndicator.SetActive(true);
//    }

//    private void MoveMouseIndicator(Vector3 position)
//    {
//        mouseIndicator.transform.position = position;
//    }

//    private void MoveCellIndicator(Vector3 position)
//    {
//        cellIndicator.transform.position = position;
//    }

//    private void ObjectPlaced(GameObject gameObject)
//    {
//        // Custom logic when object is placed, if any
//    }

//    private void StopPlacementVisuals()
//    {
//        gridVisualization.SetActive(false);
//        cellIndicator.SetActive(false);
//    }
//}
#endregion
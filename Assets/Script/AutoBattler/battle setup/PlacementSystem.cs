using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private AutoBattlerUIManager autoBattlerUIManager;
    [SerializeField] private PreViewSystem preview;

    [Header("System References")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid;
    [SerializeField] private ObjectDatabaseSO database;

    [Header("Placement Settings")]
    private int selectedObjectIndex = -1;
    private bool isPreviewEnabled = false;
    private int userPlacedPlayerCount = 0;
    private int maxPlayerToPlace = 6;
    private List<GameObject> placedGameObjects = new List<GameObject>();
    private Vector3 lastDetectedPosition = Vector3.zero;
    private GridData floorData, objectData;

    public static PlacementSystem Instance { get; private set; }
    public List<GameObject> aIPikas { get; private set; } = new List<GameObject>();
    public List<GameObject> playerPika { get; private set; } = new List<GameObject>();

    public AttackVisualPooler attackVisualPooler;
    public SoundManager soundManager;

    public static event Action OnPlacementComplete;

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
        floorData = new GridData();
        objectData = new GridData();
    }

    public void StartPlacement()
    {
        StartCoroutine(AIPlaceObjects());
    }

    private IEnumerator AIPlaceObjects()
    {
        const float duration = 10f;
        const int AIPlayerToPlace = 6;
        float endTime = Time.time + duration;
        int placedItems = 0;

        while (Time.time < endTime && placedItems < AIPlayerToPlace)
        {
            int randomIndex = UnityEngine.Random.Range(0, database.objectData.Count);
            Vector3Int randomPosition = new Vector3Int(
                UnityEngine.Random.Range(-4, 5),
                0,
                UnityEngine.Random.Range(1, 5)
            );

            try
            {
                if (CheckPlacementValidity(randomPosition, randomIndex))
                {
                    PlaceStructureAt(randomIndex, randomPosition, true);
                    placedItems++;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during AI placement: {ex.Message}");
            }

            yield return new WaitForSeconds(1f);
        }

        autoBattlerUIManager.StartPlayerTeamSelection();
    }

    public void StartPlacement(int ID)
    {
        if (userPlacedPlayerCount >= maxPlayerToPlace)
        {
            Debug.Log("Maximum number of players placed.");
            StartCoroutine( StartBattle());  // Automatically start the battle when the player finishes placing
            return;
        }

        soundManager.PlaySoundByID(2);
        isPreviewEnabled = true;
        StopPlacement();
        selectedObjectIndex = database.objectData.FindIndex(data => data.ID == ID);

        if (selectedObjectIndex < 0)
        {
            Debug.LogWarning($"No ID found for {ID}");
            return;
        }

        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(
            prefabs[database.objectData[selectedObjectIndex].ID],
            Vector2Int.one
        );

        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (isPreviewEnabled && !inputManager.IsPointerOverUI())
        {
            try
            {
                Vector3 mousePosition = inputManager.GetSelectedMapPosition();
                Vector3Int gridPosition = grid.WorldToCell(mousePosition);

                if (CheckPlacementValidity(gridPosition, selectedObjectIndex))
                {
                    PlaceStructureAt(selectedObjectIndex, gridPosition, false);
                    isPreviewEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error placing structure: {ex.Message}");
            }
        }
    }

    private void PlaceStructureAt(int objectIndex, Vector3Int gridPosition, bool isAIPlacement)
    {
        try
        {
            GameObject gameObject = Instantiate(prefabs[objectIndex]);
            Vector3 cellWorldPosition = grid.CellToWorld(gridPosition);

            // Offset to center the object in the cell
            Vector3 offset = new Vector3(grid.cellSize.x / 2f, 0, grid.cellSize.z / 2f);
            gameObject.transform.position = cellWorldPosition + offset;

            PikamoonBattleAnimationController pikamoonController1 = gameObject.GetComponent<PikamoonBattleAnimationController>();
            pikamoonController1.SetAttackVisualPooler(attackVisualPooler);

            PikamoonController pikamoonController = gameObject.GetComponent<PikamoonController>();
            pikamoonController.pikamoonID = database.objectData[objectIndex].ID;
            pikamoonController.InitializeComponents();
            pikamoonController.InitializePikamoonAttributes();

            if (isAIPlacement)
            {
                gameObject.transform.Rotate(0, 180, 0);
                aIPikas.Add(gameObject);
                pikamoonController.isAIPikamoon = true;
            }
            else
            {
                userPlacedPlayerCount++;
                playerPika.Add(gameObject);
                pikamoonController.isAIPikamoon = false;
            }

            preview.StopShowingPreView();
            placedGameObjects.Add(gameObject);

            GridData selectData = database.objectData[objectIndex].ID == -1 ? floorData : objectData;
            selectData.AddOjectAt(
                gridPosition,
                Vector2Int.one,
                database.objectData[objectIndex].ID,
                placedGameObjects.Count - 1
            );
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error placing structure at index {objectIndex}: {ex.Message}");
        }

        soundManager.PlaySoundByID(0);

        // Automatically start the battle when the maximum number of players are placed
        if (userPlacedPlayerCount >= maxPlayerToPlace)
        {
            StartCoroutine(StartBattle());
        }
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectData = database.objectData[selectedObjectIndex].ID == -1 ? floorData : objectData;
        return selectData.CanPlaceObjectAt(gridPosition, Vector2Int.one);
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
        if (selectedObjectIndex < 0) return;

        try
        {
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            if (lastDetectedPosition != gridPosition && isPreviewEnabled)
            {
                bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
                mouseIndicator.transform.position = new Vector3(mousePosition.x, 0, mousePosition.z);
                preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
                lastDetectedPosition = gridPosition;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error during update: {ex.Message}");
        }
    }

    private IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Starting the battle...");
        autoBattlerUIManager.BattleInProgressPanel();
        autoBattlerUIManager.TeamSelectionCompleted();
        AutoBattlerEvents.TriggerPlacementComplete();
        OnPlacementComplete?.Invoke();
    }
}

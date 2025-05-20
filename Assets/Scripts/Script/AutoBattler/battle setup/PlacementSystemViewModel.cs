//using System;
//using UnityEngine;
//public class PlacementSystemViewModel
//{
//    private readonly IObjectDatabase database;
//    private readonly IGrid grid;
//    private int selectedObjectIndex = -1;

//    public event Action<Vector3> OnMouseIndicatorMove;
//    public event Action<Vector3> OnCellIndicatorMove;
//    public event Action<GameObject> OnObjectPlaced;
//    public event Action OnPlacementStopped;

//    public PlacementSystemViewModel(IObjectDatabase database, IGrid grid)
//    {
//        this.database = database;
//        this.grid = grid;
//    }

//    public void StartPlacement(int ID)
//    {
//        StopPlacement();
//        var data = database.GetObjectDataById(ID);
//        if (data == null)
//        {
//            Debug.Log($"No ID found {ID}");
//            return;
//        }
//        selectedObjectIndex = ID;
//        OnPlacementStopped?.Invoke();
//    }

//    public void PlaceStructure(Vector3 mousePosition)
//    {
//        if (isPointerOverUI)
//        {
//            return;
//        }

//        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
//        GameObject gameObject = GameObject.Instantiate(database.GetObjectDataById(selectedObjectIndex).Prefab);
//        Vector3 cellWorldPosition = grid.CellToWorld(gridPosition);
//        gameObject.transform.position = new Vector3(cellWorldPosition.x, 0, cellWorldPosition.z);

//        OnObjectPlaced?.Invoke(gameObject);
//    }

//    public void UpdateMousePosition(Vector3 mousePosition)
//    {
//        if (selectedObjectIndex < 0)
//        {
//            return;
//        }

//        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
//        Vector3 cellWorldPosition = grid.CellToWorld(gridPosition);

//        OnMouseIndicatorMove?.Invoke(new Vector3(mousePosition.x, 0, mousePosition.z));
//        OnCellIndicatorMove?.Invoke(new Vector3(cellWorldPosition.x, 0, cellWorldPosition.z));
//    }

//    public void StopPlacement()
//    {
//        selectedObjectIndex = -1;
//        OnPlacementStopped?.Invoke();
//    }
//}

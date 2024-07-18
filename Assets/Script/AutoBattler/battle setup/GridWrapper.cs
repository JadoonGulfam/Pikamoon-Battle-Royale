//using UnityEngine;
//public interface IGrid
//{
//    Vector3Int WorldToCell(Vector3 position);
//    Vector3 CellToWorld(Vector3Int position);
//}

//public class GridWrapper : MonoBehaviour, IGrid
//{
//    private Grid grid;

//    private void Awake()
//    {
//        grid = GetComponent<Grid>();
//        if (grid == null)
//        {
//            Debug.LogError("Grid component is missing from this GameObject.");
//        }
//    }

//    public Vector3Int WorldToCell(Vector3 position)
//    {
//        return grid.WorldToCell(position);
//    }

//    public Vector3 CellToWorld(Vector3Int position)
//    {
//        return grid.CellToWorld(position);
//    }
//}

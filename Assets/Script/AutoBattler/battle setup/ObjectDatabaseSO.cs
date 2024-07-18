using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ObjectDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectData;
}
[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
}



#region ________________MVVM____________________
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public interface IObjectDatabase
//{
//    ObjectData GetObjectDataById(int id);
//}

//[CreateAssetMenu]
//public class ObjectDatabaseSO : ScriptableObject, IObjectDatabase
//{
//    public List<ObjectData> objectData;

//    public ObjectData GetObjectDataById(int id)
//    {
//        return objectData.Find(data => data.ID == id);
//    }
//}

//[Serializable]
//public class ObjectData
//{
//    [field: SerializeField]
//    public string Name { get; private set; }

//    [field: SerializeField]
//    public int ID { get; private set; }

//    [field: SerializeField]
//    public Vector2Int Size { get; private set; }

//    [field: SerializeField]
//    public GameObject Prefab { get; private set; }
//}
#endregion
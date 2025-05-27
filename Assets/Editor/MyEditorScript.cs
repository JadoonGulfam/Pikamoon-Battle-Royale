using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;
using Object = UnityEngine.Object;
using UnityEditor.IMGUI.Controls;

public delegate void ValueChanged();

public struct CustomVector3
{
    public CustomVariable<float> X;
    public CustomVariable<float> Y;
    public CustomVariable<float> Z;

    public CustomVector3(float x, float y, float z)
    {
        X = new CustomVariable<float>();
        Y = new CustomVariable<float>();
        Z = new CustomVariable<float>();

        X.Value = x;
        Y.Value = y;
        Z.Value = z;
    }

    public void AssignDelegates(ValueChanged x, ValueChanged y, ValueChanged z)
    {
        X.OnValueChanged = x;
        Y.OnValueChanged = y;
        Z.OnValueChanged = z;
    }

    public void UpdateVector(Vector3 newVector)
    {
        X.Value = newVector.x;
        Y.Value = newVector.y;
        Z.Value = newVector.z;
    }
}

/// <summary>
/// has OnValueChanged delegate, called whenever value is changed
/// </summary>
/// <typeparam name="T">Generic weaponType</typeparam>
public class CustomVariable<T>
{
    private T value;
    public ValueChanged OnValueChanged;

    // default constructor
    public CustomVariable()
    {
        
    }

    public CustomVariable(T value, ValueChanged valueChangedDelegate)
    {
        OnValueChanged = valueChangedDelegate;
        this.value = value;
    }

    public T Value
    {
        get { return value; }

        set
        {
            // Check if the new value is different from the current value
            if (!EqualityComparer<T>.Default.Equals(value, this.value))
            {
                // Assign the new value to the variable
                this.value = value;

                // Invoke the OnValueChange event if it is not null
                OnValueChanged?.Invoke();
            }
        }
    }
}

public class ArrayModifier : EditorWindow
{
    public enum ArrayType { Straight = 1, Circular = 2 }

    public static ArrayModifier Instance;

    // Serialized
    private CustomVariable<bool> incrementalDistance, incrementalRotation, incrementalScaling;
    private CustomVector3 distance;
    private CustomVector3 rotation;
    private CustomVector3 scaling;
    private CustomVariable<GameObject> objectToReplicate;
    private CustomVariable<int> numberOfObjects;
    private CustomVariable<ArrayType> arrayType;
    private CustomVariable<float> radius;
    private CustomVariable<float> angle;

    // non-Serialized
    private GameObject[] objects;
    private Transform parent;
    private float angleIncrement = 0f;
    NewArray _currentArray;

    [MenuItem("MyEditorScript/Array")]
    public static void ShowWindow()
    {
        // get window
        EditorWindow currentWindow = GetWindow<ArrayModifier>();
        currentWindow.titleContent = new GUIContent("Objects Array");
        Instance = (ArrayModifier)currentWindow;

        Instance.InitializeArray();
    }

    private void InitializeArray()
    {
        // max Length is 5000
        Instance.objects = new GameObject[500];

        // new object update in script as well
        Instance.objectToReplicate = new CustomVariable<GameObject>();
        Instance.objectToReplicate.OnValueChanged = Instance.UpdateReferencedObject;

        // new position, and also subscribe to on value change delegate
        Instance.distance = new CustomVector3(0f, 0f, 0f);
        Instance.distance.AssignDelegates(Instance.PositionChangedX, Instance.PositionChangedY, Instance.PositionChangedZ);

        // new rotation, and also subscribe to on value change delegate
        Instance.rotation = new CustomVector3(0f, 0f, 0f);
        Instance.rotation.AssignDelegates(Instance.RotationChangedX, Instance.RotationChangedY, Instance.RotationChangedZ);

        // new scaling, and also subscribe to on value change delegate
        Instance.scaling = new CustomVector3(0f, 0f, 0f);
        Instance.scaling.AssignDelegates(Instance.ScalingChangedX, Instance.ScalingChangedY, Instance.ScalingChangedZ);

        // new Number of Objects, also subscribe to on value change
        Instance.numberOfObjects = new CustomVariable<int>();
        Instance.numberOfObjects.Value = 0;
        Instance.numberOfObjects.OnValueChanged = Instance.UpdateNumberOfObjects;

        // for changing array TYPE
        Instance.arrayType = new CustomVariable<ArrayType>();
        Instance.arrayType.Value = ArrayType.Straight;
        Instance.arrayType.OnValueChanged = Instance.ReSubscribe;

        // also renew Radius
        Instance.radius = new CustomVariable<float> { Value = 3f };
        Instance.radius.OnValueChanged = Instance.RedoCircularPositions;

        // and then angle
        Instance.angle = new CustomVariable<float>() { Value = 360f };
        Instance.angle.OnValueChanged = Instance.RedoCircularPositions;

        Instance.parent = new GameObject("NewArray").transform;

        // create array Data, and link up Delegates
        Instance.CreateArrayData();
    }

    public void SetOldArrayData(NewArray oldArray)
    {
        _currentArray = oldArray;

        // re-get new Objects from parent
        this.parent = oldArray.transform;

        // manually feed in objects
        for (int i = 0; i < parent.childCount; i++)
        {
            objects[i] = parent.GetChild(i).gameObject;
        }

        // load back old values
        objectToReplicate.Value = oldArray.ReferencedObject.gameObject;
        arrayType.Value = oldArray.ArrayData.ArrayType;

        // positions, rotations, and scaling
        distance.UpdateVector(oldArray.ArrayData.Distance);
        rotation.UpdateVector(oldArray.ArrayData.Rotation);
        scaling.UpdateVector(oldArray.ArrayData.Scaling);

        // if old array was circular then update these as well
        if (arrayType.Value == ArrayType.Circular)
        {
            radius.Value = oldArray.ArrayData.Radius;
            angle.Value = oldArray.ArrayData.Angle;
        }

        numberOfObjects.Value = oldArray.ArrayData.NumberOfObjects;
    }

    private void CreateArrayData()
    {
        // saving array, so it can be modified again from context Menu
        Instance.parent.gameObject.AddComponent<NewArray>();
        Instance._currentArray = Instance.parent.GetComponent<NewArray>();
        Instance._currentArray.CreateObject();
        Instance._currentArray.UpdateArrayType(arrayType.Value);

        // for increments, delegates
        Instance.incrementalDistance = new CustomVariable<bool>(true, () =>
        Instance._currentArray.SetIncrementalDistance(Instance.incrementalDistance.Value));
        Instance.incrementalRotation = new CustomVariable<bool>(true, () =>
        Instance._currentArray.SetIncrementalRotaion(Instance.incrementalRotation.Value));
        Instance.incrementalScaling = new CustomVariable<bool>(true, () =>
        Instance._currentArray.SetIncrementalScaling(Instance.incrementalScaling.Value));

        // search for arrays previously created, and assign them this Instance
        NewArray[] arrays = FindObjectsOfType<NewArray>(includeInactive: true);
        foreach (NewArray array in arrays)
        {
            array.SetArrayModifier(Instance);
        }
    }

    private void UpdateReferencedObject() => _currentArray.UpdateReferencedObject(objectToReplicate.Value.GetComponent<Transform>());

    private void OnValidate()
    {
        Debug.Log("Script Reloaded, closing window");
        Close();
    }

    /// <summary>
    /// Updates the Objects positions, whenever radius or angle is changed
    /// </summary>
    private void RedoCircularPositions()
    {
        angleIncrement = angle.Value / numberOfObjects.Value;
        if (objects.Length < 1)
            return;

        for (int i = 0; i < numberOfObjects.Value; i++)
        {
            objects[i].transform.position = RadiusPosition(i, angleIncrement);
        }

        _currentArray.UpdateAngle(angle.Value);
        _currentArray.UpdateRadius(radius.Value);
    }

    /// <summary>
    /// Changes the Delegates Functions, when array type is changed
    /// </summary>
    private void ReSubscribe()
    {
        if (arrayType.Value.Equals(ArrayType.Circular))
        {
            numberOfObjects.OnValueChanged = UpdateNumberOfObjectsCircular;
        }
        else if (arrayType.Value.Equals(ArrayType.Straight))
        {
            numberOfObjects.OnValueChanged = UpdateNumberOfObjects;
        }
        _currentArray.UpdateArrayType(arrayType.Value);
        Debug.Log("Changed to " + arrayType.Value);
    }

    /// <summary>
    /// Updates the Number of Objects, when number of objects is changed
    /// </summary>
    private void UpdateNumberOfObjectsCircular()
    {
        if (numberOfObjects.Value <= 1)
        { Debug.Log("Please use atleast 2 Number Of Objects"); return; }

        if (objectToReplicate.Value == null)
        {
            Debug.Log("No Object Referenced in Object To Replicate");
            return;
        }

        // check if new value is range with Objects Length
        if (numberOfObjects.Value > objects.Length)
        {
            Debug.Log("Can't Exceed" + objects.Length + " Objects Limit!!");
            return;
        }

        // delete old objects
        for (int i = 0; i < objects.Length; i++)
        {
            DestroyImmediate(objects[i]);
        }

        // if parent is null then create a New parent
        if (parent == null)
            CreateArrayData();

        // add more objects
        AddOnObjectsCircular();

        // rotation Change
        RotationChangedX();
        RotationChangedY();
        RotationChangedZ();

        // scale change
        ScalingChangedX();
        ScalingChangedY();
        ScalingChangedZ();
    }

    private void UpdateNumberOfObjects()
    {
        if (numberOfObjects.Value <= 1)
        { Debug.Log("Please use atleast 2 Number Of Objects"); return; }

        if (objectToReplicate.Value == null)
        {
            Debug.Log("No Object Referenced in Object To Replicate");
            return;
        }

        // check if new value is range with Objects Length
        if (numberOfObjects.Value > objects.Length)
        {
            Debug.Log("Can't Exceed" + objects.Length + " Objects Limit!!");
            return;
        }

        // delete old objects
        for (int i = 0; i < objects.Length; i++)
        {
            DestroyImmediate(objects[i]);
        }

        // if parent is null then create a New parent
        if (parent == null)
            CreateArrayData();

        AddOnObjects();

        // recall position Change
        PositionChangedX();
        PositionChangedY();
        PositionChangedZ();

        // rotation Change
        RotationChangedX();
        RotationChangedY();
        RotationChangedZ();

        // scale change
        ScalingChangedX();
        ScalingChangedY();
        ScalingChangedZ();

        _currentArray.UpdateObjects(numberOfObjects.Value);
    }

    private Vector3 RadiusPosition(int index, float angleIncrement)
    {
        float angleInDegrees = index * angleIncrement;
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        float x = parent.position.x + radius.Value * Mathf.Cos(angleInRadians);
        float y = parent.position.y + radius.Value * Mathf.Sin(angleInRadians);
        return new Vector3(x, y, parent.position.z);
    }

    private void AddOnObjectsCircular()
    {
        angleIncrement = angle.Value / numberOfObjects.Value;

        // check what to instantiate Prefab or simple object
        if (PrefabUtility.IsPartOfPrefabInstance(objectToReplicate.Value))
        {
            // get Prefab from object
            Object objectToInstantiate = PrefabUtility.GetCorrespondingObjectFromSource<Object>(objectToReplicate.Value);

            // instantiate prefabs
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                objects[i] = PrefabUtility.InstantiatePrefab(objectToInstantiate, parent) as GameObject;
                objects[i].transform.position = RadiusPosition(i, angleIncrement);
            }
        }
        else
        {
            // Instantiate new Objects and store in References
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                objects[i] = Instantiate(objectToReplicate.Value, RadiusPosition(i, angleIncrement), Quaternion.identity, parent);
            }
        }
    }

    private void AddOnObjects()
    {
        // check what to instantiate Prefab or simple object
        if (PrefabUtility.IsPartOfPrefabInstance(objectToReplicate.Value))
        {
            // get Prefab from object
            Object objectToInstantiate = PrefabUtility.GetCorrespondingObjectFromSource<Object>(objectToReplicate.Value);

            // instantiate prefabs
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                objects[i] = PrefabUtility.InstantiatePrefab(objectToInstantiate, parent) as GameObject;
            }
        }
        else
        {
            // Instantiate new Objects and store in References
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                objects[i] = Instantiate(objectToReplicate.Value, parent);
            }
        }
    }

    private void PositionChangedX()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Position Changed X");

        Vector3 currentPosition = objects[0].transform.position;

        if (incrementalDistance.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentPosition.x += distance.X.Value;
                currentPosition.y = objects[i].transform.position.y;
                currentPosition.z = objects[i].transform.position.z;

                // update Position
                objects[i].transform.position = currentPosition;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentPosition.x = distance.X.Value;
                currentPosition.y = objects[i].transform.position.y;
                currentPosition.z = objects[i].transform.position.z;

                // update Position
                objects[i].transform.position = currentPosition;
            }
        }

        _currentArray.DistanceUpdateX(distance.X.Value);
    }

    private void PositionChangedY()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Position Changed Y");

        Vector3 currentPosition = objects[0].transform.position;

        if (incrementalDistance.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentPosition.x = objects[i].transform.position.x;
                currentPosition.y += distance.Y.Value;
                currentPosition.z = objects[i].transform.position.z;

                // update position
                objects[i].transform.position = currentPosition;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentPosition.x = objects[i].transform.position.x;
                currentPosition.y = distance.Y.Value;
                currentPosition.z = objects[i].transform.position.z;

                // update position
                objects[i].transform.position = currentPosition;
            }
        }

        _currentArray.DistanceUpdateY(distance.Y.Value);
    }

    private void PositionChangedZ()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Position Changed Z");

        Vector3 currentPosition = objects[0].transform.position;

        if (incrementalDistance.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentPosition.x = objects[i].transform.position.x;
                currentPosition.y = objects[i].transform.position.y;
                currentPosition.z += distance.Z.Value;

                // update position
                objects[i].transform.position = currentPosition;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentPosition.x = objects[i].transform.position.x;
                currentPosition.y = objects[i].transform.position.y;
                currentPosition.z = distance.Z.Value;

                // update position
                objects[i].transform.position = currentPosition;
            }
        }
        _currentArray.DistanceUpdateZ(distance.Z.Value);
    }

    private void RotationChangedX()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Rotation Changed X");
        Quaternion currentRotation = objects[0].transform.rotation;

        if (incrementalRotation.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentRotation.x += rotation.X.Value;
                currentRotation.y = objects[i].transform.rotation.y;
                currentRotation.z = objects[i].transform.rotation.z;

                // update Rotation
                objects[i].transform.localRotation = currentRotation;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentRotation.x = rotation.X.Value;
                currentRotation.y = objects[i].transform.rotation.y;
                currentRotation.z = objects[i].transform.rotation.z;

                // update Rotation
                objects[i].transform.localRotation = currentRotation;
            }
        }
        _currentArray.RotationUpdateX(rotation.X.Value);
    }

    private void RotationChangedY()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Rotation Changed Y");
        Quaternion currentRotation = objects[0].transform.rotation;

        if (incrementalRotation.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentRotation.x = objects[i].transform.rotation.x;
                currentRotation.y += rotation.Y.Value;
                currentRotation.z = objects[i].transform.rotation.z;

                // update Rotation
                objects[i].transform.localRotation = currentRotation;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentRotation.x = objects[i].transform.rotation.x;
                currentRotation.y = rotation.Y.Value;
                currentRotation.z = objects[i].transform.rotation.z;

                // update Rotation
                objects[i].transform.localRotation = currentRotation;
            }
        }
        _currentArray.RotationUpdateY(rotation.Y.Value);
    }

    private void RotationChangedZ()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Rotation Changed Z");
        Quaternion currentRotation = objects[0].transform.rotation;

        if (incrementalRotation.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentRotation.x = objects[i].transform.rotation.x;
                currentRotation.y = objects[i].transform.rotation.y;
                currentRotation.z += rotation.Z.Value;

                // update Rotation
                objects[i].transform.localRotation = currentRotation;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentRotation.x = objects[i].transform.rotation.x;
                currentRotation.y = objects[i].transform.rotation.y;
                currentRotation.z = rotation.Z.Value;

                // update Rotation
                objects[i].transform.localRotation = currentRotation;
            }
        }
        _currentArray.RotationUpdateZ(rotation.Z.Value);
    }

    private void ScalingChangedX()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Scale Changed X");

        Vector3 currentScale = objects[0].transform.localScale;

        if (incrementalScaling.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentScale.x += scaling.X.Value;
                currentScale.y = objects[i].transform.localScale.y;
                currentScale.z = objects[i].transform.localScale.z;

                // update Position
                objects[i].transform.localScale = currentScale;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentScale.x = scaling.X.Value;
                currentScale.y = objects[i].transform.localScale.y;
                currentScale.z = objects[i].transform.localScale.z;

                // update Position
                objects[i].transform.localScale = currentScale;
            }
        }
        _currentArray.ScalingUpdateX(scaling.X.Value);
    }

    private void ScalingChangedY()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Scale Changed Y");

        Vector3 currentScale = objects[0].transform.localScale;

        if (incrementalScaling.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentScale.x = objects[i].transform.localScale.x;
                currentScale.y += scaling.Y.Value;
                currentScale.z = objects[i].transform.localScale.z;

                // update Position
                objects[i].transform.localScale = currentScale;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentScale.x = objects[i].transform.localScale.x;
                currentScale.y = scaling.Y.Value;
                currentScale.z = objects[i].transform.localScale.z;

                // update Position
                objects[i].transform.localScale = currentScale;
            }
        }
        _currentArray.ScalingUpdateY(scaling.Y.Value);
    }

    private void ScalingChangedZ()
    {
        if (objects.Length < 1)
            return;

        Debug.Log("Scale Changed Z");

        Vector3 currentScale = objects[0].transform.localScale;

        if (incrementalScaling.Value)
        {
            for (int i = 1; i < numberOfObjects.Value; i++)
            {
                currentScale.x = objects[i].transform.localScale.x;
                currentScale.y = objects[i].transform.localScale.y;
                currentScale.z += scaling.Z.Value;

                // update Position
                objects[i].transform.localScale = currentScale;
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjects.Value; i++)
            {
                currentScale.x = objects[i].transform.localScale.x;
                currentScale.y = objects[i].transform.localScale.y;
                currentScale.z = scaling.Z.Value;

                // update Position
                objects[i].transform.localScale = currentScale;
            }
        }
        _currentArray.ScalingUpdateZ(scaling.Z.Value);
    }

    public void OnGUI()
    {
        if (numberOfObjects == null)
        {
            Debug.Log("ReOpen Window");
            return;
        }

        objectToReplicate.Value = EditorGUILayout.ObjectField("ObjectToReplicate", objectToReplicate.Value, typeof(Object), true) as GameObject;
        numberOfObjects.Value = EditorGUILayout.IntField("NumberOfObjects: ", numberOfObjects.Value);

        // type of array to create
        arrayType.Value = (ArrayType)EditorGUILayout.EnumPopup("ArrayType:", arrayType.Value);

        GUILayout.Space(10f);

        if (GUILayout.Button("Reset Values"))
        {
            ResetValues();
        }

        if (GUILayout.Button("Clear Temp"))
        {
            CleanTemp();
        }

        GUILayout.Space(10f);

        // Fields for position
        GUILayout.Label("Position ", EditorStyles.boldLabel);
        incrementalDistance.Value = EditorGUILayout.Toggle("Incremental", incrementalDistance.Value);
        //GUILayout.BeginHorizontal();
        //GUILayout.Label("X:");
        distance.X.Value = EditorGUILayout.FloatField("X: ", distance.X.Value);
        //GUILayout.Label("Y:");
        distance.Y.Value = EditorGUILayout.FloatField("Y: ", distance.Y.Value);
        //GUILayout.Label("Z:");
        distance.Z.Value = EditorGUILayout.FloatField("Z: ", distance.Z.Value);
        //GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        // Fields for rotation
        GUILayout.Label("Rotation ", EditorStyles.boldLabel);
        incrementalRotation.Value = EditorGUILayout.Toggle("Incremental", incrementalRotation.Value);
        GUILayout.BeginHorizontal();
        GUILayout.Label("X:");
        rotation.X.Value = EditorGUILayout.Slider(rotation.X.Value, -1f, 1f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Y:");
        rotation.Y.Value = EditorGUILayout.Slider(rotation.Y.Value, -1f, 1f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Z:");
        rotation.Z.Value = EditorGUILayout.Slider(rotation.Z.Value, -1f, 1f);
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        // Fields for scaling
        GUILayout.Label("Scaling ", EditorStyles.boldLabel);
        incrementalScaling.Value = EditorGUILayout.Toggle("Incremental", incrementalScaling.Value);
        scaling.X.Value = EditorGUILayout.FloatField("X: ", scaling.X.Value);
        scaling.Y.Value = EditorGUILayout.FloatField("Y: ", scaling.Y.Value);
        scaling.Z.Value = EditorGUILayout.FloatField("Z: ", scaling.Z.Value);

        // if array type is Circular, show addtional Fields
        if (arrayType.Value.Equals(ArrayType.Circular))
        {
            GUILayout.Space(10f);
            GUILayout.Label("Radius & Angle", EditorStyles.boldLabel);
            radius.Value = EditorGUILayout.FloatField("Radius", radius.Value);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Angle:");
            angle.Value = EditorGUILayout.Slider(angle.Value, 0, 360);
            GUILayout.EndHorizontal();
        }
    }

    private void CleanTemp()
    {
        // since all arrays data is stored in _temp folder, delete em
        string[] guids = AssetDatabase.FindAssets("", new[] { "Assets/_ArrayTemp" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            //scriptable.gui
            AssetDatabase.DeleteAsset(path);
        }
    }

    private void ResetValues()
    {
        switch (arrayType.Value)
        {
            case ArrayType.Straight:
                distance.UpdateVector(Vector3.zero);
                rotation.UpdateVector(Vector3.zero);
                scaling.UpdateVector(Vector3.zero);
                break;

            case ArrayType.Circular:
                distance.UpdateVector(Vector3.zero);
                rotation.UpdateVector(Vector3.zero);
                scaling.UpdateVector(Vector3.zero);
                radius.Value = 3f;
                angle.Value = 360f;
                break;
        }
    }

    public class NewArray : MonoBehaviour
    {
        [SerializeField] Transform referencedObject;
        public ArrayData ArrayData => currentArrayData;
        public Transform ReferencedObject => referencedObject;

        // private hidden
        private ArrayData currentArrayData;
        private ArrayModifier _arrayModifier;
        private string path = "Assets/_ArrayTemp";

        public void CreateObject()
        {
            currentArrayData = ScriptableObject.CreateInstance<ArrayData>();

            Debug.Log("Create arrayData at Path: " + path);

            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", "_ArrayTemp");
            }

            // Save the ScriptableObject asset to the specified path
            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/NewArray.asset");
            AssetDatabase.CreateAsset(currentArrayData, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // remove this component, when scene changes, or is unloaded
            //SceneManager.sceneUnloaded += (scene) => RemoveThis();

            //EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        private void PlayModeStateChanged(PlayModeStateChange newState)
        {
            if (newState == PlayModeStateChange.ExitingEditMode)
            {
                Remove();

                // unsubscribe
                EditorApplication.playModeStateChanged -= PlayModeStateChanged;
            }
        }

        public void Remove()
        {
            DestroyImmediate(this);
            //SceneManager.sceneUnloaded -= (scene) => RemoveThis();
        }

        public void ReModifyArray()
        {
            if (referencedObject == null)
            {
                Debug.LogError("Referenced Object is Lost, Please Re-Drop in Inspector");
                return;
            }

            if (_arrayModifier == null)
            {
                Debug.Log("Please Open Array Window");
                return;
            }

            if (currentArrayData == null)
            {
                Debug.Log("No Old Array data found");
                Remove();
                return;
            }

            _arrayModifier.SetOldArrayData(this);
        }

        public void OnActions(ValueChanged[] actions)
        {

        }

        public void ShowObject() => currentArrayData.ShowArrayData();
        public void SetArrayModifier(ArrayModifier modifier) => _arrayModifier = modifier;
        public void UpdateObjects(int objects) => currentArrayData.NumberOfObjects = objects;
        public void DistanceUpdateX(float newX) => currentArrayData.Distance.x = newX;
        public void DistanceUpdateY(float newY) => currentArrayData.Distance.y = newY;
        public void DistanceUpdateZ(float newZ) => currentArrayData.Distance.z = newZ;
        public void RotationUpdateX(float newX) => currentArrayData.Rotation.x = newX;
        public void RotationUpdateY(float newY) => currentArrayData.Rotation.y = newY;
        public void RotationUpdateZ(float newZ) => currentArrayData.Rotation.z = newZ;
        public void ScalingUpdateX(float newX) => currentArrayData.Scaling.x = newX;
        public void ScalingUpdateY(float newY) => currentArrayData.Scaling.y = newY;
        public void ScalingUpdateZ(float newZ) => currentArrayData.Scaling.z = newZ;
        public void UpdateArrayType(ArrayType type) => currentArrayData.ArrayType = type;
        public void UpdateRadius(float radius) => currentArrayData.Radius = radius;
        public void UpdateAngle(float angle) => currentArrayData.Angle = angle;
        public void UpdateReferencedObject(Transform obj) => referencedObject = obj;
        public void SetIncrementalDistance(bool value) => currentArrayData.DistanceIncrement = value;
        public void SetIncrementalRotaion(bool value) => currentArrayData.RotationIncrement = value;
        public void SetIncrementalScaling(bool value) => currentArrayData.ScalingIncrement = value;
    }

    [CustomEditor(typeof(NewArray))]
    public class NewArrayEditor : Editor
    {
        private NewArray targetArray;

        private void OnEnable()
        {
            targetArray = (NewArray)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Re-ModifyArray"))
            {
                targetArray.ReModifyArray();
            }

            if (GUILayout.Button("Remove"))
            {
                targetArray.Remove();
            }

            if (GUILayout.Button("Debug Data"))
            {
                targetArray.ShowObject();
            }
        }
    }

    public class ArrayData : ScriptableObject
    {
        public ArrayType ArrayType;
        public int NumberOfObjects;
        public Vector3 Distance;
        public Vector3 Rotation;
        public Vector3 Scaling;
        public float Radius;
        public float Angle;
        public bool DistanceIncrement;
        public bool RotationIncrement;
        public bool ScalingIncrement;

        public void ShowArrayData()
        {
            Debug.Log("NumberOfObjects: " + NumberOfObjects);
            Debug.Log("Distance: " + Distance);
            Debug.Log("DistanceIncrement: " + DistanceIncrement);
            Debug.Log("Rotation: " + Rotation);
            Debug.Log("RotationIncrement: " + RotationIncrement);
            Debug.Log("Scaling: " + Scaling);
            Debug.Log("ScalingIncrement: " + ScalingIncrement);
            Debug.Log("ArrayType: " + ArrayType);
            Debug.Log("Radius: " + Radius);
            Debug.Log("Angle: " + Angle);
        }
    }
}

public class MyEditorScriptWindow : EditorWindow
{
    public static MyEditorScriptWindow Instance;

    // serialized
    private Object[] MyObjects;
    private List<Object> DuplicatedObjects = new List<Object>();
    private CustomVariable<int> NumberOfObjects;
    private float yAxisOffset;
    private bool useSameScale;
    private string searchIn = string.Empty;
    private Transform anotherTransform;

    // non-serialized
    Vector2 scrollPosition = Vector2.zero;
    int duplicates;
    string name = string.Empty;

    [MenuItem("MyEditorScript/Show Tool")]
    public static void ShowWindow()
    {
        EditorWindow currentWindow = GetWindow<MyEditorScriptWindow>();
        currentWindow.titleContent = new GUIContent("My Modelling Tool");
        Instance = (MyEditorScriptWindow)currentWindow;

        InitializeNumOfObjects();

        Instance.searchIn = "Assets";

        // when in Scene View
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void InitializeNumOfObjects()
    {
        // New up customNumber of Objects
        Instance.NumberOfObjects = new CustomVariable<int>();
        Instance.NumberOfObjects.Value = 0;
        Instance.NumberOfObjects.OnValueChanged = () =>
        {
            Instance.MyObjects = new Object[Instance.NumberOfObjects.Value];
        };
    }

    private void OnDisable()
    {
        // when in Scene View
        SceneView.duringSceneGui -= OnSceneGUI;

        Instance = null;
    }

    //private void Update()
    //{
    //    if (dKeyPressed)
    //    {
    //        RepeatDuplication();
    //        Debug.Log("Duplicate");
    //        dKeyPressed = false;
    //    }
    //}

    public static void OnSceneGUI(SceneView sceneView)
    {
        // Check for key press in the Scene View
        if (Event.current.type != EventType.KeyDown)
            return;

        Event currentEvent = Event.current;

        // if shift is Being Held
        if (currentEvent.shift)
        {
            switch (currentEvent.keyCode)
            {
                case KeyCode.R:
                    Debug.Log("Shift + R");
                    Instance.RepeatDuplication();
                    break;

                case KeyCode.S:
                    Debug.Log("Shift + S pressed in Scene View");
                    //if (Physics.Raycast(Selection.activeTransform.position, Vector3.down, out RaycastHit hit, 500f))
                    //{
                    //    float distanceToBottom = Selection.activeTransform.GetComponent<Collider>().bounds.extents.y;
                    //    Vector3 newPosition = hit.point + new Vector3(0f, distanceToBottom, 0f);
                    //    Debug.Log(newPosition);
                    //    Selection.activeTransform.position = newPosition;
                    //}
                    break;

            }
            currentEvent = null;
            return;
        }
    }


    private void OnGUI()
    {
        if (NumberOfObjects == null)
        {
            Debug.Log("ReOpen Window");
            return;
        }

        GUILayout.Space(10);

        GUILayout.Label("Fields ", EditorStyles.boldLabel);
        NumberOfObjects.Value = EditorGUILayout.IntField("Size", NumberOfObjects.Value);
        yAxisOffset = EditorGUILayout.FloatField("Y-AxisOffset", yAxisOffset);
        useSameScale = EditorGUILayout.Toggle("UseSameScale", useSameScale);
        searchIn = EditorGUILayout.TextField("Search Prefabs in: ", searchIn);
        anotherTransform = EditorGUILayout.ObjectField("AnotherTransform", anotherTransform, typeof(Transform), true) as Transform;

        GUILayout.Space(10f);
        GUILayout.Label("Prefabs/Objects ", EditorStyles.boldLabel);

        if (GUILayout.Button("Search For Prefabs"))
        {
            if (searchIn.Equals(string.Empty))
            {
                Debug.Log("Please Provide some path");
                Array.Clear(MyObjects, 0, NumberOfObjects.Value);
                return;
            }
            string[] guid = AssetDatabase.FindAssets("t:prefab", new[] { searchIn });

            NumberOfObjects.Value = guid.Length;

            for (int i = 0; i < NumberOfObjects.Value; i++)
                MyObjects[i] = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid[i]));
        }

        if (NumberOfObjects.Value > 0)
        {
            float newMaxHeight = NumberOfObjects.Value * 25f;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MaxHeight(Mathf.Clamp(newMaxHeight, 50f, 220f)));

            for (int i = 0; i < NumberOfObjects.Value; i++)
            {
                GUILayout.BeginHorizontal();
                MyObjects[i] = EditorGUILayout.ObjectField(MyObjects[i], typeof(Object), true);

                if (GUILayout.Button("Place In Selected Transform(s)"))
                {
                    // if has more than one object selected
                    if (Selection.gameObjects.Length > 1)
                    {
                        var objects = from x in Selection.gameObjects select x.transform;
                        Transform[] targets = objects.ToArray();

                        for (int j = 0; j < targets.Length; j++)
                        {
                            UpdateWithPrefab(MyObjects[i], targets[j]);
                        }
                        return;
                    }

                    Transform target = Selection.gameObjects[0].transform;
                    UpdateWithPrefab(MyObjects[i], target);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }

        if (DuplicatedObjects.Count > 0)
            if (DuplicatedObjects[0] == null)
            {
                DuplicatedObjects.Clear();
            }

        GUILayout.Space(15f);
        GUILayout.Label("Transform Helper", EditorStyles.boldLabel);

        if (GUILayout.Button("Reset Selected Position(s) To 0", GUILayout.MinHeight(25f)))
        {
            if (Selection.gameObjects.Length > 1)
            {
                foreach (GameObject gameObject in Selection.gameObjects)
                {
                    ChangePositionToZero(gameObject.transform);
                }
            }
            else
                ChangePositionToZero(Selection.activeTransform);
        }

        if (GUILayout.Button("Reseat Selected Object childs positions on Y", GUILayout.MinHeight(25f)))
        {
            Transform target = Selection.activeTransform;

            foreach (Transform x in target)
            {
                Reseat(x);
            }
        }

        if (GUILayout.Button("Reseat Selected Transform(s) position on Y", GUILayout.MinHeight(25f)))
        {
            if (Selection.gameObjects.Length > 1)
            {
                foreach (GameObject gameObject in Selection.gameObjects)
                {
                    Reseat(gameObject.transform);
                }
            }
            else
                Reseat(Selection.activeTransform);
        }

        if (GUILayout.Button("Derive Selected Position from AnotherTransform", GUILayout.MinHeight(25f)))
        {
            if (anotherTransform == null)
            {
                Debug.Log("No Object Passed in AnotherTransform");
                return;
            }

            Transform target = Selection.activeTransform;

            if (target.childCount == 0)
            {
                target.position = anotherTransform.position;
                return;
            }

            for (int i = 0; i < target.childCount; i++)
            {
                target.GetChild(i).position -= anotherTransform.position;
            }
            target.position = anotherTransform.position;
            //Debug
        }

        if (GUILayout.Button("Redo Selected Names", GUILayout.MinHeight(25f)))
        {
            int selectedObjectsCount = Selection.gameObjects.Length;

            if (selectedObjectsCount <= 1)
                return;

            int i = 1;

            for (; i < selectedObjectsCount; i++)
            {
                Selection.gameObjects[i].name += " (" + i.ToString() + ")";
            }
        }

        // some space and label
        GUILayout.Space(15f);
        GUILayout.Label("Duplicate Objects, Repeat Duplicated Step", EditorStyles.boldLabel);

        if (GUILayout.Button("Duplicate", GUILayout.MinHeight(25f)))
        {
            DuplicateObject();
        }

        if (GUILayout.Button("RepeatDuplication", GUILayout.MinHeight(25f)))
        {
            RepeatDuplication();
        }

        for (int i = 0; i < DuplicatedObjects.Count; i++)
        {
            DuplicatedObjects[i] = EditorGUILayout.ObjectField($"Duplicated Temp {i}", DuplicatedObjects[i], typeof(Object), true);
        }

        //HandleKeyboard();

        EditorGUILayout.Space();

        if (GUILayout.Button("Enable Random Placer"))
        {

        }

        if (GUILayout.Button("Auto Prefab All"))
        {
            Debug.Log("Might still wanna wait for this feature");
            return;

            if (Selection.gameObjects.Length > 1)
            {
                MeshFilter go1Filter = Selection.gameObjects[0].GetComponent<MeshFilter>();
                MeshFilter go2Filter = Selection.gameObjects[1].GetComponent<MeshFilter>();

                if (go1Filter.sharedMesh == go2Filter.sharedMesh)
                {
                    Debug.Log("Same Mesh");
                }
                else
                    Debug.Log("Mesh is different");
            }
        }
    }

    private void UpdateWithPrefab(Object prefab, Transform target)
    {
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab, target.parent) as GameObject;
        instantiatedPrefab.transform.position = target.position;
        instantiatedPrefab.transform.rotation = target.rotation;

        if (useSameScale)
            instantiatedPrefab.transform.localScale = target.localScale;

        // let undo know we instantiated an Object, and deleted another
        Undo.RegisterCreatedObjectUndo(instantiatedPrefab, "SwapedObjectWithPrefab");

        Debug.Log("Updated " + target.name + " at Position: " + target.position);

        // and Destory selected object, and also add in Undo
        Undo.DestroyObjectImmediate(target.gameObject);
    }

    private void ChangePositionToZero(Transform target)
    {
        Vector3 position = target.position;

        if (target.childCount == 0)
        {
            Debug.Log("Has no child transforms, use the Transform Component to Reset");
            return;
        }

        // if target object has Parent, then change local position
        if (target.parent)
        {
            position = target.localPosition;

            if (target.localPosition == Vector3.zero)
            {
                Debug.Log($"Transform {target.name} is already Positioned zero");
                return;
            }
        }
        // if dont have parent, check its position
        else if (target.position == Vector3.zero)
        {
            Debug.Log($"Transform {target.name} is already Positioned zero");
            return;
        }

        for (int i = 0; i < target.childCount; i++)
        {
            target.GetChild(i).position += position;
        }

        if (target.parent)
            target.localPosition = Vector3.zero;
        else
            target.position = Vector3.zero;

        Debug.Log("Done Positioning " + target.name + " to Zero");
    }

    private void Reseat(Transform target)
    {
        if (Physics.Raycast(target.position, Vector3.down, out RaycastHit hit, 500f))
        {
            Vector3 newPosition = hit.point + new Vector3(0f, yAxisOffset, 0f);
            Debug.Log(newPosition);
            target.position = newPosition;
        }
    }

    public void DuplicateObject()
    {
        // starting new duplication
        duplicates = 0;
        DuplicatedObjects.Clear();

        // get currentObject name
        name = Selection.activeGameObject.name;

        CloneObject(Selection.activeGameObject);
    }

    private void RepeatDuplication()
    {
        if (DuplicatedObjects.Count == 0)
        {
            return;
        }

        // if any of the object is missing, then return out
        if (DuplicatedObjects[0] == null || DuplicatedObjects[1] == null)
        {
            DuplicatedObjects.Clear();
            Debug.Log("Retry");
            return;
        }

        // Extract transform of Target and Source
        Transform source = ((GameObject)DuplicatedObjects[0]).transform;
        Transform target = ((GameObject)DuplicatedObjects[1]).transform;

        // produce new Position
        Vector3 newPosition = target.position - source.position;

        // clear list, as it will now hold new Objects
        DuplicatedObjects.Clear();

        // cloned object and update name and its position
        GameObject clonedObject = CloneObject(target.gameObject) as GameObject;
        clonedObject.transform.position = newPosition + target.position;
        clonedObject.name = name + $" ({duplicates})";

        Debug.Log("New Cloned Object Position: " + (newPosition + target.position));
    }

    private Object CloneObject(Object objectToDuplicate)
    {
        Transform parent = ((GameObject)objectToDuplicate).transform.parent;
        Object[] newObjects = new Object[1];

        // Add to List
        DuplicatedObjects.Add(objectToDuplicate);

        // if selected object is an Prefab
        if (PrefabUtility.IsPartOfPrefabInstance(objectToDuplicate))
        {
            Debug.Log("Selected Object is a prefab");

            // get Prefab from object
            Object objectToInstantiate = PrefabUtility.GetCorrespondingObjectFromSource<Object>(objectToDuplicate);

            // instantiate prefab
            newObjects[0] = PrefabUtility.InstantiatePrefab(objectToInstantiate, parent);

            // new object might have its position, we might wanna give it currentObject position
            ((GameObject)newObjects[0]).transform.position = ((GameObject)objectToDuplicate).transform.position;
        }
        else
        {
            newObjects[0] = Instantiate((GameObject)objectToDuplicate, parent);
        }

        // add new prefab in list
        DuplicatedObjects.Add(newObjects[0]);

        // increment
        duplicates++;

        // new object name
        newObjects[0].name = name + $" ({duplicates})";

        // also register in Undo Class
        Undo.RegisterCreatedObjectUndo(newObjects[0], "NewDuplicatedObject");

        Selection.objects = newObjects;
        return newObjects[0];
    }

    public class RandomPlacer : MonoBehaviour
    {
        public Transform ObjectToPlace;
        [Range(10, 10000)] public int NumberOfObjects = 50;
        [Tooltip("Put in your Distance, then Click on RemoveNearByObjects, that will remove the objects In range with this distance")]
        public float DistanceBetweenObjects;
        public CustomVariable<int> RandomSeed;
        public bool KeepYSame;
        public Color PreviewBoundsColor = new Color(1f, 0f, 0f, .3f);
        [HideInInspector]
        public Bounds bounds = new Bounds(Vector3.zero, new Vector3(500f, 100f, 500f));
        [HideInInspector]
        public GameObject[] Objects = new GameObject[50];

        void OnDrawGizmos()
        {
            if (Selection.activeGameObject != this.gameObject)
                return;

            // draw map bounds
            Gizmos.color = PreviewBoundsColor;
            Gizmos.DrawCube(bounds.center, bounds.size);
        }

        public void UpdateNumberOfObjectsFromChilds()
        {
            NumberOfObjects = transform.childCount - 1;

            // hold reference to new Number of Objects
            Objects = new GameObject[NumberOfObjects];

            for (int i = 0; i < NumberOfObjects; i++)
            {
                Objects[i] = transform.GetChild(i).gameObject;   
            }
        }
    }

    [CustomEditor(typeof(RandomPlacer))]
    public class RandomPlacerEditor : Editor
    {
        private RandomPlacer targetPlacer;
        private BoxBoundsHandle boxBoundsHandle = new BoxBoundsHandle();

        public GUIStyle myStyle1, boxStyle;

        [MenuItem("MyEditorScript/Random Placer")]
        public static void LaunchRandomPlacer()
        {
            // create empty
            Transform placer = new GameObject("RandomPlacer").transform;

            placer.gameObject.AddComponent<RandomPlacer>();

            Selection.activeTransform = placer;
        }

        private void OnEnable()
        {
            targetPlacer = (RandomPlacer)target;

            // hide default tools
            Tools.hidden = true;

            targetPlacer.RandomSeed = new CustomVariable<int>();
            targetPlacer.RandomSeed.OnValueChanged += (() =>
            {
                UnityEngine.Random.InitState(targetPlacer.RandomSeed.Value);
            });
            targetPlacer.RandomSeed.OnValueChanged += (() =>
            {
                foreach (GameObject go in targetPlacer.Objects)
                {
                    DestroyImmediate(go);
                }
            });
            targetPlacer.RandomSeed.OnValueChanged += ProduceObjects;
        }

        private void OnDestroy()
        {
            // enable back tools
            Tools.hidden = false;
        }

        private void OnDisable()
        {
            // enable back tools
            Tools.hidden = false;
        }

        public override void OnInspectorGUI()
        {
            targetPlacer.RandomSeed.Value = EditorGUILayout.IntField("RandomSeed", targetPlacer.RandomSeed.Value);

            base.OnInspectorGUI();

            // box Style
            if (boxStyle == null)
            {
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
                boxStyle.fontStyle = FontStyle.Bold;
                boxStyle.alignment = TextAnchor.UpperLeft;
            }

            // style
            if (myStyle1 == null)
            {
                myStyle1 = new GUIStyle(GUI.skin.label);
                myStyle1.richText = true;
                myStyle1.fontStyle = FontStyle.Bold;
            }

            // show map bounds
            EditorGUILayout.BeginVertical(boxStyle);
            Vector3Int intMapBounds = Vector3Int.RoundToInt(targetPlacer.bounds.size);
            EditorGUILayout.LabelField(new GUIContent(string.Format("Map Bounds (XYZ): <b>{0} x {1} x {2}</b>", intMapBounds.x, intMapBounds.y, intMapBounds.z), "Current map bounds."), myStyle1);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(10f);

            GUILayout.Label("Buttons", myStyle1);
            if (GUILayout.Button("Show Bounds Data", GUILayout.Height(30)))
            {
                Debug.Log($"Center: {targetPlacer.bounds.center}, Size:  {targetPlacer.bounds.size}, Extents: {targetPlacer.bounds.extents}");
            }

            if (GUILayout.Button("Do Random In Bounds", GUILayout.Height(30)))
            {
                ProduceObjects();
            }

            if (GUILayout.Button("Remove NearBy Objects",GUILayout.Height(30)))
            {
                RemoveInRange();
            }

            if (GUILayout.Button("Reset Bounds", GUILayout.Height(30)))
            {
                targetPlacer.bounds.center = Vector3.zero;
                targetPlacer.bounds.size = new Vector3(500f, 100f, 500f);

                // also update boundboxHandles
                BoundBoxHandles();
            }

            if (GUILayout.Button("Clear Bounds", GUILayout.Height(30)))
            {
                ClearRegion();
            }
        }

        private void ClearRegion()
        {
            GameObject[] objects = new GameObject[targetPlacer.transform.childCount];
            for (int i = 0; i < targetPlacer.transform.childCount; i++)
            {
                objects[i] = targetPlacer.transform.GetChild(i).gameObject;
            }

            // now del all
            foreach (GameObject go in objects)
            {
                DestroyImmediate(go);
            }

            targetPlacer.NumberOfObjects = 10;
        }

        private void RemoveInRange()
        {
            foreach (Transform t in targetPlacer.transform)
            {
                if (t == null)
                    continue;

                // do overlap sphere
                Collider[] colliders = Physics.OverlapSphere(t.position, targetPlacer.DistanceBetweenObjects);
                
                foreach (Collider collider in colliders)
                {
                    if (collider==null)
                        continue;

                    if (collider.gameObject == t.gameObject)
                    {
                        continue;
                    }

                    // delete object
                    DestroyImmediate(collider.gameObject);
                }
            }

            // update in NumberOfObjects
            targetPlacer.UpdateNumberOfObjectsFromChilds();
        }

        private void ProduceObjects()
        {
            if (targetPlacer.ObjectToPlace == null)
            {
                Debug.LogError("Please Drop a Reference Object in Inspector");
                return;
            }

            targetPlacer.Objects = new GameObject[targetPlacer.NumberOfObjects];

            if (IsTargetAPrefab(targetPlacer.ObjectToPlace))
            {
                // use prefab utility to duplicate
                if (targetPlacer.KeepYSame)
                {
                    for (int i = 0; i < targetPlacer.NumberOfObjects; i++)
                    {
                        GameObject obj = PrefabUtility.InstantiatePrefab(targetPlacer.ObjectToPlace.gameObject, targetPlacer.transform) as GameObject;
                        obj.transform.localPosition = GetRandomPosInExtent(targetPlacer.transform.position.y);                       
                        obj.transform.localRotation = Quaternion.identity;

                        targetPlacer.Objects[i] = obj;
                    }
                }
                else
                {
                    for (int i = 0; i < targetPlacer.NumberOfObjects; i++)
                    {
                        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(targetPlacer.ObjectToPlace.gameObject, targetPlacer.transform);
                        obj.transform.localPosition = GetRandomPosInExtent();
                        obj.transform.localRotation = Quaternion.identity;

                        targetPlacer.Objects[i] = obj;
                    }
                }
                return;
            }

            if (targetPlacer.KeepYSame)
            {
                for (int i = 0; i < targetPlacer.NumberOfObjects; i++)
                {
                    targetPlacer.Objects[i] = Instantiate(targetPlacer.ObjectToPlace, GetRandomPosInExtent(targetPlacer.transform.position.y), Quaternion.identity, targetPlacer.transform).gameObject;
                }
            }
            else
            {
                for (int i = 0; i < targetPlacer.NumberOfObjects; i++)
                {
                    targetPlacer.Objects[i] = Instantiate(targetPlacer.ObjectToPlace, GetRandomPosInExtent(), Quaternion.identity, targetPlacer.transform).gameObject;
                }
            }
        }

        private Vector3 GetRandomPosInExtent()
        {
            Vector3 randomPos;
            randomPos.x = UnityEngine.Random.Range(-targetPlacer.bounds.size.x / 2, targetPlacer.bounds.size.x / 2) + targetPlacer.bounds.center.x;
            randomPos.y = UnityEngine.Random.Range(-targetPlacer.bounds.size.y / 2, targetPlacer.bounds.size.y / 2) + targetPlacer.bounds.center.y;
            randomPos.z = UnityEngine.Random.Range(-targetPlacer.bounds.size.z / 2, targetPlacer.bounds.size.z / 2) + targetPlacer.bounds.center.z;
            return randomPos;
        }

        private Vector3 GetRandomPosInExtent(float y)
        {
            Vector3 randomPos;
            randomPos.x = UnityEngine.Random.Range(-targetPlacer.bounds.size.x / 2, targetPlacer.bounds.size.x / 2) + targetPlacer.bounds.center.x;
            randomPos.y = y;
            randomPos.z = UnityEngine.Random.Range(-targetPlacer.bounds.size.z / 2, targetPlacer.bounds.size.z / 2) + targetPlacer.bounds.center.z;
            return randomPos;
        }

        private bool IsTargetAPrefab(Object obj)
        {
            return PrefabUtility.IsPartOfAnyPrefab(obj);
        }

        protected virtual void OnSceneGUI()
        {
            // keep TextureCreator selected
            //HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            // return if busy
            //if (hudTarget._isBusy)
            // return;

            // update bounds center with position handle
            EditorGUI.BeginChangeCheck();
            Vector3 newCenter = Handles.PositionHandle(targetPlacer.bounds.center, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(targetPlacer, "Move Bounds");
                targetPlacer.bounds.center = newCenter;
                targetPlacer.transform.position = newCenter;
            }

            // update bounds with box handles
            EditorGUI.BeginChangeCheck();
            BoundBoxHandles();
            boxBoundsHandle.DrawHandle();
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(targetPlacer, "Change Bounds");
                targetPlacer.bounds = new Bounds(boxBoundsHandle.center, boxBoundsHandle.size);
            }

        }

        private void BoundBoxHandles()
        {
            boxBoundsHandle.center = targetPlacer.bounds.center;
            boxBoundsHandle.size = targetPlacer.bounds.size;
        }
    }
}
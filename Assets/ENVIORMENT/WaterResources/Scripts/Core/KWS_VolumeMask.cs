//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace KWS
//{
//    [ExecuteAlways]
//    [Serializable]
//    public class KWS_VolumeMask : MonoBehaviour
//    {
//        [SerializeField] public WaterSystemVolumeData VolumeSettings;
//        public                         VolumeMaskTypeEnum    VolumeMaskType = VolumeMaskTypeEnum.Box;

//        public Mesh               VolumeMesh;

//        public enum VolumeMaskTypeEnum
//        {
//            Box,
//            Sphere,
//            //CustomMesh
//        }

//        internal KWS_WaterVolumeVariables.WaterVolumeData _volumeData;
//        internal Transform                                _cachedTransform;
//        internal Matrix4x4                                _rotationMatrix;

//        [SerializeField] internal bool ShowColorSettings           = true;
//        [SerializeField] internal bool ShowVolumetricLightSettings = false;

//        void Awake()
//        {
//            LoadOrCreateSettings();
//        }

//        void OnEnable()
//        {
//            LoadOrCreateSettings();
//            WaterSharedResources.VolumeMasks.Add(this);
//            _cachedTransform = transform;
//            UpdateAll();
//        }

//        void OnDisable()
//        {
//            WaterSharedResources.VolumeMasks.Remove(this);
//        }

//        void Update()
//        {
//            if (_cachedTransform.hasChanged)
//            {
//                _cachedTransform.hasChanged = false;
//                UpdateAll();
//            }
//        }

//        void OnValidate()
//        {
//            UpdateAll();
//        }

//        internal void LoadOrCreateSettings()
//        {
//            if (VolumeSettings == null) VolumeSettings = ScriptableObject.CreateInstance<WaterSystemVolumeData>();
//        }


//        void UpdateAll()
//        {
//            if (_cachedTransform == null) return;

//            var rotationQuaternion = _cachedTransform.rotation;
//            _rotationMatrix = Matrix4x4.Rotate(rotationQuaternion).inverse;
//            _rotationMatrix.m03 = 0;
//            _rotationMatrix.m13 = 0;
//            _rotationMatrix.m23 = 0;
//            _rotationMatrix.m30 = 0;
//            _rotationMatrix.m31 = 0;
//            _rotationMatrix.m32 = 0;
//            _rotationMatrix.m33 = 1;


//            UpdateVolumeData();
//        }

//        internal void UpdateVolumeData()
//        {
//            _volumeData.Position = _cachedTransform.position;
//            _volumeData.Rotation = _rotationMatrix;
//            _volumeData.Size = _cachedTransform.localScale * 0.5f;

//            VolumeSettings.UpdateVolumeData(ref _volumeData);
//        }
//    }

//}

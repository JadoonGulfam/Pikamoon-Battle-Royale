using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieImpulseAssets
{
    public class SceneDemo : MonoBehaviour
    {
        public List<GameObject> objects; // Assign the list of GameObjects in the Inspector

        private int currentIndex = 0;

        void Start()
        {
            // Ensure there are objects in the list
            if (objects.Count > 0)
            {
                // Set the first object active
                SetActiveObject(currentIndex);
            }
        }

        void Update()
        {
            // Check for space bar press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Deactivate current object
                objects[currentIndex].SetActive(false);

                // Move to the next object
                currentIndex = (currentIndex + 1) % objects.Count;

                // Activate the new object
                SetActiveObject(currentIndex);
            }
        }

        void SetActiveObject(int index)
        {
            if (objects.Count > 0)
            {
                objects[index].SetActive(true);
            }
        }
    }
}

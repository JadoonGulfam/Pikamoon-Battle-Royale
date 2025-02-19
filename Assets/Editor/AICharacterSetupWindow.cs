using Fusion;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterSetupWindow : EditorWindow
{
    private GameObject selectedGameObject;
    private RuntimeAnimatorController animatorController;
    private Transform masterCharacterTransform;

    [MenuItem("Tools/Create AI Character")]
    public static void ShowWindow()
    {
        // Show the custom window
        AICharacterSetupWindow window = GetWindow<AICharacterSetupWindow>("AI Character Setup");
        window.minSize = new Vector2(400, 200);
    }

    private void OnGUI()
    {
        GUILayout.Label("AI Character Setup", EditorStyles.boldLabel);

        // Display the selected GameObject
        selectedGameObject = EditorGUILayout.ObjectField("Selected GameObject", Selection.activeGameObject, typeof(GameObject), true) as GameObject;

        if (selectedGameObject == null)
        {
            EditorGUILayout.HelpBox("Please select a GameObject in the Hierarchy.", MessageType.Warning);
            return;
        }

        // Input fields for references
        animatorController = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Animator Controller", animatorController, typeof(RuntimeAnimatorController), false);
        masterCharacterTransform = (Transform)EditorGUILayout.ObjectField("Master Character Transform", masterCharacterTransform, typeof(Transform), true);

        // Button to add components and set references
        if (GUILayout.Button("Setup AI Character"))
        {
            SetupAICharacter();
        }
    }

    private void SetupAICharacter()
    {
        if (selectedGameObject == null)
        {
            Debug.LogError("No GameObject selected! Please select a GameObject.");
            return;
        }

        // Add required components
        AddComponentIfMissing<NavMeshAgent>(selectedGameObject);
        AddComponentIfMissing<Animator>(selectedGameObject);
        AddComponentIfMissing<PikamoonAI>(selectedGameObject);
        AddComponentIfMissing<BarkianPlayerFollowAI>(selectedGameObject);
        AddComponentIfMissing<PikamoonRoaming>(selectedGameObject);
        AddComponentIfMissing<PikamoonCommandHandler>(selectedGameObject);
        AddComponentIfMissing<PikamoonFollow>(selectedGameObject);
        AddComponentIfMissing<CharacterController>(selectedGameObject);

        // Set references
        Animator animator = selectedGameObject.GetComponent<Animator>();
        if (animator != null && animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }

        PikamoonFollow pikamoonFollow = selectedGameObject.GetComponent<PikamoonFollow>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in players)
        {
            // Check if the player object has authority (is the master player)
            if (playerObject.GetComponent<NetworkObject>().HasStateAuthority)
            {
                masterCharacterTransform = playerObject.transform;
                break; // Exit the loop once we find the master player
            }
        }

        if (pikamoonFollow != null && masterCharacterTransform != null)
        {

            pikamoonFollow.SetMasterCharacter(masterCharacterTransform); // Assuming SetMasterCharacter exists
        }

        Debug.Log($"AI Character setup completed for {selectedGameObject.name}.");
    }

    private void AddComponentIfMissing<T>(GameObject gameObject) where T : Component
    {
        if (gameObject.GetComponent<T>() == null)
        {
            gameObject.AddComponent<T>();
        }
    }
}

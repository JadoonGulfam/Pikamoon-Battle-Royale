using UnityEngine;

public class Material_Transition : MonoBehaviour
{
    public Material material;
    private float dissolve = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<SkinnedMeshRenderer>().material;
        material.SetFloat("_Cutoff", dissolve);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            dissolve -= 0.01f;
            material.SetFloat("_Cutoff", dissolve);
        }
    }
}

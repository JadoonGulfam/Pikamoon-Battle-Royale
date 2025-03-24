using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class CapatureEffect : MonoBehaviour
{
    //public PikamoonRoaming pikamoonRoaming;
   // public PikamoonInventory pikamoonInventory;
    [SerializeField] VisualEffect capture_new;
    public Material[] material;
    private void Awake()
    {
        for (int i = 0; i < material.Length; i++)
        material[i].SetFloat("_Cutoff", 4);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            print("captured");
           // StartCoroutine(capturePikamoon(this.gameObject));
            //gameObject.transform.DOScale(0, 0.5f).OnComplete(Capture);
            Capture();
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < material.Length; i++)
            material[i].SetFloat("_Cutoff", 4);
    }

    public void Capture()
    {
        StartCoroutine(capturePikamoon(this.gameObject));
        // pikamoonInventory.AddPikamoon(this.gameObject);
    }
    IEnumerator capturePikamoon(GameObject pikamoon)
    {
        float duration = 3f;
        float startValue = 4;
        float endValue = 0f; // Target value
        float stepSize = 0.1f; // Reduce by 0.1 at a time
        float totalSteps = (startValue - endValue) / stepSize; // Total steps required
        float delay = duration / totalSteps; // Delay between each step
        float currentValue = startValue;
        capture_new.Play();
        while (currentValue > endValue)
        {
            currentValue -= stepSize;
            for (int i = 0; i < material.Length; i++)
                material[i].SetFloat("_Cutoff", currentValue);
            yield return new WaitForSeconds(delay);
        }
        Destroy(gameObject);
//pikamoonInventory.AddPikamoon(this.gameObject);
        Debug.Log("Reduction complete! Final Value: " + currentValue);
    }
    private void OnEnable()
    {
        // StartCoroutine(SetPikamoonMaterial());
    }
    public IEnumerator SetPikamoonMaterial()
    {
       // pikamoonRoaming.DisableRoaming();
        float duration = 3f;
        float startValue = 0f;
        float endValue = 4f;
        float stepSize = 0.1f;
        float totalSteps = (endValue - startValue) / stepSize; // Correct total steps calculation
        float delay = duration / totalSteps; // Delay per step
        float currentValue = startValue;
        while (currentValue < 4)
        {
            Debug.Log("Current Value: " + currentValue);
            for (int i = 0; i < material.Length; i++)
                material[i].SetFloat("_Cutoff", currentValue);
            currentValue = Mathf.Min(currentValue + stepSize, endValue); // Ensure it doesn't exceed endValue
            yield return new WaitForSeconds(delay);
        }
    }
    //private void OnMouseDown()
    //{
    //    print("captured");
    //    pikamoonInventory.AddPikamoon(this.gameObject);
    //}
   
}

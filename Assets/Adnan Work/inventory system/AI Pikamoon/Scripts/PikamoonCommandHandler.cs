using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;
using INab.Dissolve;
public enum PikamoonCommand
{
    Follow,
    Attack,
    Roam,
    Release
}

public class PikamoonCommandHandler : MonoBehaviour
{
    private PikamoonAI pikamoonAI;
    public PikamoonInventory pikamoonInventory;

    [SerializeField] VisualEffect capture_new;
    public Material material;
    private void Awake()
    {
        pikamoonAI = GetComponent<PikamoonAI>();
        material.SetFloat("_Cutoff", 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            print("captured");

            //StartCoroutine(capture(this.gameObject));

            //gameObject.transform.DOScale(0, 0.5f).OnComplete(Capture);
            Capture();

        }
    }
    //IEnumerator capture(GameObject go)
    //{
    //    yield return new WaitForSeconds((0f));
    //    pikamoonInventory.AddPikamoon(this.gameObject);
    //}

    void Capture()
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
            material.SetFloat("_Cutoff", currentValue);
            yield return new WaitForSeconds(delay);
        }
        
        pikamoonInventory.AddPikamoon(this.gameObject);
        
        Debug.Log("Reduction complete! Final Value: " + currentValue);
    }

    private void OnEnable()
    {
       // StartCoroutine(SetPikamoonMaterial());
    }
    public IEnumerator SetPikamoonMaterial()
    {
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
            material.SetFloat("_Cutoff", currentValue);

            currentValue = Mathf.Min(currentValue + stepSize, endValue); // Ensure it doesn't exceed endValue
            yield return new WaitForSeconds(delay);
        }
    }



    
    //private void OnMouseDown()
    //{
    //    print("captured");
    //    pikamoonInventory.AddPikamoon(this.gameObject);
    //}
    public void ExecuteCommand(PikamoonCommand command, Transform target = null)
    {
        if (pikamoonAI == null) return;

        switch (command)
        {
            case PikamoonCommand.Follow:
                pikamoonAI.FollowPlayer();
                break;
            case PikamoonCommand.Attack:
                pikamoonAI.AttackEnemy(target);
                break;
            case PikamoonCommand.Roam:
                pikamoonAI.StartRoaming();
                break;
            case PikamoonCommand.Release:
                pikamoonAI.ReleasePikamoon();
                break;
        }
    }

}

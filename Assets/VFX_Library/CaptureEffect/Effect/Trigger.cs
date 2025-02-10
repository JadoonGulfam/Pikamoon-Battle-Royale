using UnityEngine;
using UnityEngine.VFX;

public class Trigger : MonoBehaviour
{
    [SerializeField] VisualEffect _explodeEffect;
    VisualEffect capture_new;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        capture_new = Instantiate(_explodeEffect, transform.position, transform.rotation);

    }

    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.E))
        {
            _explodeEffect.Play();
        }
       */

        if (Input.GetKeyDown(KeyCode.F))
        {
            capture_new.Play();
        }
        
        Destroy(capture_new.gameObject, 4f);

    }
   
}

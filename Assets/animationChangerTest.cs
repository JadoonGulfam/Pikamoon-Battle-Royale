using UnityEngine;

public class animationChangerTest : MonoBehaviour
{

    [SerializeField] Animator animator;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            NextWeaponAnimation();
        }
    }

    
    public void NextWeaponAnimation()
    {
        animator.SetTrigger("Next");
    }
}

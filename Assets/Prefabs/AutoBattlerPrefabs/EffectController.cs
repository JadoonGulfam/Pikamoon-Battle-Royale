using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] private float _duration = 1;
    public float Duration => _duration;

    public virtual void OnEnable()
    {
        Invoke("Sleep", _duration);
    }

    private void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}

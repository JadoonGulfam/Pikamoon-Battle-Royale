using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;
using Pikamoon.Controller;
using System.Collections;

public class TestRotationScript : MonoBehaviour
{
    [SerializeField] CinemachineOrbitalFollow orbitalFollow;
    [Space]
    
    [SerializeField] Locomotion LOC;
    [SerializeField] PlayerInput input;


    [Space]
    [SerializeField] float StartHorAxVal;
    [SerializeField] float EndHorAxVal;
    [SerializeField] float HorDuration;

    [Space]
    [SerializeField] float StartVerAxVal;
    [SerializeField] float EndVerAxVal;
    [SerializeField] float VerDuration;


    [Space]

    public bool IsStarted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsStarted = false;


    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(CheckForMovmentINput());
            //StartCinematic();
        }
    }
    IEnumerator CheckForMovmentINput()
    {
        while(!input.isMoving)
        {
            yield return null;
        }
        StartCinematic();
    }


    public void StartCinematic()
    {
        IsStarted = true;
        orbitalFollow.HorizontalAxis.Value = StartHorAxVal;
        orbitalFollow.VerticalAxis.Value = StartVerAxVal;

        //DOTween.To(() => orbitalFollow.VerticalAxis.Value, x => orbitalFollow.VerticalAxis.Value = x, EndVerAxVal, VerDuration).SetEase(Ease.Linear);
        DOTween.To(() => orbitalFollow.HorizontalAxis.Value, x => orbitalFollow.HorizontalAxis.Value = x, EndHorAxVal, HorDuration).SetEase(Ease.Linear);
        LOC.EnableWalk();
        LOC.DisableCameraRot();
    }
}

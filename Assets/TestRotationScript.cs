using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;
using Pikamoon.Controller;
using System.Collections;

public class TestRotationScript : MonoBehaviour
{

    [SerializeField] Transform sTARTcaM;

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

        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(CheckForMovmentINput());
            //StartCinematic();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            DisableStartCam();
            //StartCinematic();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            EnableStartCam();
            //StartCinematic();
        }


    }



    void DisableStartCam()
    {

        if (sTARTcaM && sTARTcaM.gameObject.activeInHierarchy)
        {
            orbitalFollow.transform.gameObject.SetActive(true);
            sTARTcaM.transform.gameObject.SetActive(false);
        }
    }
    void EnableStartCam()
    {

        if (sTARTcaM && sTARTcaM.gameObject.activeInHierarchy)
        {
            orbitalFollow.transform.gameObject.SetActive(false);
            sTARTcaM.transform.gameObject.SetActive(true);
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
        //orbitalFollow.HorizontalAxis.Value = StartHorAxVal;
        orbitalFollow.VerticalAxis.Value = StartVerAxVal ;

        float Val = StartVerAxVal + EndHorAxVal;
        //DOTween.To(() => orbitalFollow.VerticalAxis.Value, x => orbitalFollow.VerticalAxis.Value = x, EndVerAxVal, VerDuration).SetEase(Ease.Linear);
        DOTween.To(() => orbitalFollow.HorizontalAxis.Value, x => orbitalFollow.HorizontalAxis.Value = x, Val, HorDuration).SetEase(Ease.Linear);
        LOC.EnableWalk();
        LOC.DisableCameraRot();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private bool slicing;
    private Collider theBlade;
    [SerializeField] private float minVelocityThresold = 0.01f;

    private TrailRenderer bladeTrail;
    public float force = 5f;
    public Vector3 directon { get; private set; }

    private void Awake()
    {
        theBlade = GetComponent<Collider>();
        mainCamera = Camera.main;
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        stopSlicing();
    }

    private void OnDisable()
    {
        stopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            stopSlicing();
        }
        else if (slicing)
        {
            continueSlicing();
        }
    }

    private void startSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        slicing = true;
        theBlade.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void stopSlicing()
    {
        slicing = false;
        theBlade.enabled = false;
        bladeTrail.enabled = false;
    }

    private void continueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        directon = newPosition - transform.position;

        float velocity = directon.magnitude / Time.deltaTime;

        theBlade.enabled = velocity > minVelocityThresold;

        transform.position = newPosition;
    }


}

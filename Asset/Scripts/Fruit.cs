using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject whole;
    [SerializeField] private GameObject sliced;

    private Collider fruitCollider;
    private Rigidbody fruitRigidBody;
    private ParticleSystem juiceParticalsystem;

    private void Awake()
    {
        fruitCollider = GetComponent<Collider>();
        fruitRigidBody = GetComponent<Rigidbody>();
        juiceParticalsystem = GetComponentInChildren<ParticleSystem>();
    }

    private void slice(Vector3 direction ,Vector3 position,float force)
    {
        FindObjectOfType<GameManager>().increseScore();
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        juiceParticalsystem.Play();
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f,angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            slice(blade.directon,blade.transform.position,blade.force);
        }
    }
}

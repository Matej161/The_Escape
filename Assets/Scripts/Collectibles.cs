using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectibles : MonoBehaviour
{
    public static event Action OnCollected;
    public static int total;

    void Awake() => total++;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.deltaTime * 100f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealt;
    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private Transform playerTransform;

    private float currentHealth;

    

    private void Start()
    {
        currentHealth = startingHealt;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}

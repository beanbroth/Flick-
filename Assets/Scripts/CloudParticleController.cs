using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CloudParticleController : MonoBehaviour
{
    [SerializeField] private BoxCollider _col;

    private float leftConstraint;
    private float bottomConstraint;
    private float topConstraint;
    private float rightConstraint;
    ParticleSystem _ps;

    [SerializeField] float buffer;


    void OnEnable()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        leftConstraint = transform.position.x - _col.bounds.size.x / 2;
        rightConstraint = transform.position.x + _col.bounds.size.x / 2;
        bottomConstraint = transform.position.y - _col.bounds.size.y / 2;
        topConstraint = transform.position.y + _col.bounds.size.y / 2;
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        int numExit = _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, exit);

        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle temp = exit[i];
            temp.remainingLifetime = 0f;
            exit[i] = temp;
        }

        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, exit);

    }
}
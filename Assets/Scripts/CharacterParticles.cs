using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticles : MonoBehaviour
{
    public ParticleSystem walkParticle;
    public void PlayWalkParticle()
    {
        walkParticle.Play();
    }
}

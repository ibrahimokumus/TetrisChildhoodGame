using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    public ParticleSystem[] allEffects;

    private void Start()
    {
        allEffects = GetComponentsInChildren<ParticleSystem>();
    }

    public void playEffect()
    {
        foreach (ParticleSystem effect in allEffects)
        {
            effect.Stop();
            effect.Play();
        }
        
    }
}

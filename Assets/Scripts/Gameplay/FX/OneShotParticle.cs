using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine;

namespace AOP.GamePlay.FX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class OneShotParticle : MonoBehaviour, IObjectCampMember
    {
        public ParticleSystem particle;
        public OneShotParticleSO particleSO;

        public void Play()
        {
            StartCoroutine(PlayCoroutine());
        }
        private IEnumerator PlayCoroutine()
        {
            particle.Play();
            yield return new WaitForSeconds(particle.main.duration);
            ObjectCamp.PushObject(this, _variation: particleSO.ParticleName);
        }
    }

}

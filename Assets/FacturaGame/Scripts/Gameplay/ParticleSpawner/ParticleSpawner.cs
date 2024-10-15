using System.Collections;
using System.Collections.Generic;
using FacturaGame.Scripts.Utils;
using UnityEngine;

namespace FacturaGame.Scripts.Gameplay.ParticleSpawner
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _enemyDeathParticle;
        [SerializeField] private Transform _container;

        private readonly Queue<ParticleSystem> _enemyDeathParticles = new ();

        public void PlayEnemyDeathAt(Vector3 point)
        {
            ParticleSystem particle;
            
            if (_enemyDeathParticles.Count > 0)
            {
                particle = _enemyDeathParticles.Dequeue();
                particle.transform.position = point;
                particle.gameObject.SetActive(true);
                particle.Play();
                CoroutineRunner.RunSelfStopCoroutine(ParticleWaitingCoroutine(particle));
                
                return;
            }

            particle = Instantiate(_enemyDeathParticle, point, Quaternion.identity, _container);
            particle.Play();
            CoroutineRunner.RunSelfStopCoroutine(ParticleWaitingCoroutine(particle));
        }

        private IEnumerator ParticleWaitingCoroutine(ParticleSystem particle)
        {
            yield return new WaitUntil(() => particle.isStopped);
            particle.gameObject.SetActive(false);
            _enemyDeathParticles.Enqueue(particle);
        }
    }
}
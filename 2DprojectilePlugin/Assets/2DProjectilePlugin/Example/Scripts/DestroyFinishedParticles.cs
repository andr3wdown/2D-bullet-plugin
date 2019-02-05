using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpGamesWeapon2D.Example
{
    public class DestroyFinishedParticles : MonoBehaviour
    {
        ParticleSystem ps;
        void Start()
        {
            ps = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (!ps.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}


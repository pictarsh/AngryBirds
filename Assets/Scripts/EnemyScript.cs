using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float maxVelocity = 10f;
    public ParticleSystem deathEffect;
    private AudioSource audio;

    public GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audio = GameObject.Find("Poof").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float velocity = other.relativeVelocity.magnitude;

        if (velocity > maxVelocity)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            audio.Play();
            gm.enemyCount--;
        }
    }
}

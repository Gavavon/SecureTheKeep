using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBall : MonoBehaviour
{
    [Header("Earth Ball")]
    public GameObject earthFX;
    public GameObject explosion;
    public float timeBeforeDeath = 15;
    public LayerMask enemy;
    public bool takenDamage = false;

    [Header("Earth Ball Effects")]
    public float earthDamageAmount = 200;
    public float aoeSize = 4;

    List<GameObject> particleFX = new List<GameObject>();

    public static EarthBall instance;
    private void Awake()
    {
        instance = this;
        StartCoroutine(killBullet());
    }
    void OnCollisionEnter(Collision collision)
    {
        damageBallHit();
    }
    public void damageBallHit()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        earthFX.GetComponent<ParticleSystem>().Stop();
        explosion.SetActive(true);
        checkForEnemies();
        StartCoroutine(bulletFinish());

    }
    public IEnumerator killBullet()
    {
        yield return new WaitForSeconds(timeBeforeDeath);
        damageBallHit();
    }
    public IEnumerator bulletFinish()
    {
        yield return new WaitForSeconds(2.5f);
        clearParticleFX();
        Destroy(gameObject);
    }
    public void clearParticleFX() 
    {
        foreach (GameObject c in particleFX)
        {
            Destroy(c);
        }
    }
    private void checkForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, aoeSize, enemy);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<EnemyActions>())
            {
                c.GetComponent<EnemyActions>().takeDamage(earthDamageAmount);
                GameObject instBullet = Instantiate(explosion, new Vector3(c.transform.position.x, c.transform.position.y-1, c.transform.position.z), Quaternion.identity) as GameObject;
                particleFX.Add(instBullet);
                instBullet.SetActive(true);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeSize);
    }
}

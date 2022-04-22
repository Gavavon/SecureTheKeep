using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBall : MonoBehaviour
{
    [Header("Vine Ball")]
    public GameObject vineFX;
    public GameObject explosion;
    public GameObject vineTrap;
    public float timeBeforeDeath = 15;
    public LayerMask enemy;

    [Header("Vine Ball Effects")]
    public float aoeSize = 2;
    public float vineDuration = 3;

    List<GameObject> particleFX = new List<GameObject>();

    public static VineBall instance;
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
        vineFX.SetActive(false);
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
        yield return new WaitForSeconds(3);
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
                StartCoroutine(c.GetComponent<EnemyActions>().stopEnemies(vineDuration));
                GameObject instBullet = Instantiate(vineTrap, c.transform.position, Quaternion.identity) as GameObject;
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

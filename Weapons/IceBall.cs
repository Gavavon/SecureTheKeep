using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{
    [Header("Ice Ball")]
    public GameObject iceFX;
    public GameObject explosion;
    public float timeBeforeDeath = 15;
    public LayerMask enemy;
    public bool blizzardActive = false;

    [Header("Ice Ball Effects")]
    public float aoeSize = 2;
    public float slowDuration = 3;
    public float speedDecreasePercent = 0.25f;

    public static IceBall instance;
    private void Awake()
    {
        instance = this;
        StartCoroutine(killBullet());
    }
    void OnCollisionEnter(Collision collision)
    {
        damageBallHit();
    }
	private void Update()
	{
        if (blizzardActive) 
        {
            checkForEnemies();
        }
	}
	public void damageBallHit()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        iceFX.GetComponent<ParticleSystem>().Stop();
        explosion.SetActive(true);
        //checkForEnemies();
        blizzardActive = true;
        explosion.GetComponent<ParticleSystem>().Stop();
        StartCoroutine(bulletFinish());
    }
    public IEnumerator killBullet()
    {
        yield return new WaitForSeconds(timeBeforeDeath);
        damageBallHit();
    }
    public IEnumerator bulletFinish()
    {
        yield return new WaitForSeconds(2);
        blizzardActive = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void checkForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, aoeSize, enemy);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<EnemyActions>())
            {
                StartCoroutine(c.GetComponent<EnemyActions>().slowEnemies(slowDuration));
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeSize);
    }
}

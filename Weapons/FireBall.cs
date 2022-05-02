using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Fire Ball")]
    public GameObject fireFX;
    public GameObject explosion;
    public float timeBeforeDeath = 15;
    public LayerMask enemy;
    public bool takenDamage = false;

    [Header("Fire Ball")]
    public float fireDamageAmount = 50;
    public float aoeSize = 2;

    public static FireBall instance;
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
        fireFX.GetComponent<ParticleSystem>().Stop();
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
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
    private void checkForEnemies() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, aoeSize, enemy);
        foreach (Collider c in colliders) 
        {
            if (c.GetComponent<EnemyActions>()) 
            {
                c.GetComponent<EnemyActions>().takeDamage(fireDamageAmount);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeSize);
    }
}

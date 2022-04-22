using DG.Tweening;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("FireBall Info")]
    public GameObject fireBall;
    public bool fireBallReady;
    public float fireBallResetTime;
    private float fireBallTimer;
    public float fireBallSpeed = 500f;

    [Header("IceBall Info")]
    public GameObject iceBall;
    public bool iceBallReady;
    public float iceBallResetTime;
    private float iceBallTimer;
    public float iceBallSpeed = 500f;

    [Header("VineBall Info")]
    public GameObject vineBall;
    public bool vineBallReady;
    public float vineBallResetTime;
    private float vineBallTimer;
    public float vineBallSpeed = 500f;

    [Header("EarthBall Info")]
    public GameObject earthBall;
    public bool earthBallReady;
    public float earthBallResetTime;
    private float earthBallTimer;
    public float earthBallSpeed = 500f;

    [Header("universal Info")]
    public Transform shootPoint;
    public Animator animator;
    

    public static PlayerShoot instance;
    private void Awake()
    {
        instance = this;
    }

	private void Update()
	{
        if (!fireBallReady && Time.time > fireBallTimer) 
        {
            fireBallReady = true;
            fireBallTimer = Time.time + fireBallResetTime;
        }
        if (!iceBallReady && Time.time > iceBallTimer)
        {
            iceBallReady = true;
            iceBallTimer = Time.time + iceBallResetTime;
        }
        if (!vineBallReady && Time.time > vineBallTimer)
        {
            vineBallReady = true;
            vineBallTimer = Time.time + vineBallResetTime;
        }
        if (!earthBallReady && Time.time > earthBallTimer)
        {
            earthBallReady = true;
            earthBallTimer = Time.time + earthBallResetTime;
        }
    }

	public void shoot()
    {
        switch (StarterAssetsInputs.instance.currentShootType)
        {
            case StarterAssetsInputs.shootType.fire:
                if (fireBallReady) 
                {
                    fireBallReady = false;
                    StartCoroutine(shootFireBall());
                }
                break;
            case StarterAssetsInputs.shootType.ice:
                if (iceBallReady)
                {
                    iceBallReady = false;
                    StartCoroutine(shootIceBall());
                }
                break;
            case StarterAssetsInputs.shootType.vine:
                if (vineBallReady)
                {
                    vineBallReady = false;
                    StartCoroutine(shootVineBall());
                }
                break;
            case StarterAssetsInputs.shootType.earth:
                if (earthBallReady)
                {
                    earthBallReady = false;
                    StartCoroutine(shootEarthBall());
                }
                break;
        }
    }
    public IEnumerator shootFireBall() 
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(.5f);
        GameObject instBullet;
        instBullet = Instantiate(fireBall, shootPoint.position, Quaternion.identity) as GameObject;
        Physics.IgnoreLayerCollision(instBullet.layer, 7, true);
        Physics.IgnoreLayerCollision(instBullet.layer, 10, true);
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(shootPoint.forward * fireBallSpeed);
        animator.SetBool("Attacking", false);

    }
    public IEnumerator shootIceBall()
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(.5f);
        GameObject instBullet;
        instBullet = Instantiate(iceBall, shootPoint.position, Quaternion.identity) as GameObject;
        Physics.IgnoreLayerCollision(instBullet.layer, 7, true);
        Physics.IgnoreLayerCollision(instBullet.layer, 10, true);
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(shootPoint.forward * iceBallSpeed);
        animator.SetBool("Attacking", false);

    }
    public IEnumerator shootVineBall()
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(.5f);
        GameObject instBullet;
        instBullet = Instantiate(vineBall, shootPoint.position, Quaternion.identity) as GameObject;
        Physics.IgnoreLayerCollision(instBullet.layer, 7, true);
        Physics.IgnoreLayerCollision(instBullet.layer, 10, true);
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(shootPoint.forward * vineBallSpeed);
        animator.SetBool("Attacking", false);

    }
    public IEnumerator shootEarthBall()
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(.5f);
        GameObject instBullet;
        instBullet = Instantiate(earthBall, shootPoint.position, Quaternion.identity) as GameObject;
        Physics.IgnoreLayerCollision(instBullet.layer, 7, true);
        Physics.IgnoreLayerCollision(instBullet.layer, 10, true);
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidbody.AddForce(shootPoint.forward * earthBallSpeed);
        animator.SetBool("Attacking", false);

    }
}

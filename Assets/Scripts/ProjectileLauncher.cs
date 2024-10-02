using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;
    public GameObject bullet;
    public Transform firePoint;
    [SerializeField] DetectionZone zone;
    Animator animator;
    public float fireRate = 2f;
    public float nextFireTime;
    private void Awake()
    {
    animator = GetComponent<Animator>();
        nextFireTime = Time.time;
    }
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 orgScale = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(
            orgScale.x * transform.localScale.x > 0 ? 1:-1,
            orgScale.y,
            orgScale.z

            );
    }
    private void FixedUpdate()
    {
        Cannon();
    }
    public void Cannon()
    {
        if (zone.detectedColliders.Count > 1 && Time.time >= nextFireTime) 
        { 
            Debug.Log("1");GameObject bulletF = Instantiate(bullet, firePoint.position, bullet.transform.rotation);
            animator.SetBool("shoot", true);
            nextFireTime = Time.time + fireRate;
        }
        else if(zone.detectedColliders.Count == 1)
        {
            animator.SetBool("shoot", false);
        }
        
    }
}

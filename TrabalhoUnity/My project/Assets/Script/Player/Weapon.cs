using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // O prefab do projétil
    public Transform shootPoint; // O ponto de onde os projéteis serão disparados
    public float shootForce = 40f; // A força com que os projéteis são disparados
    public float fireRate = 0.5f; // Tempo entre disparos

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * shootForce;
        Destroy(bullet, 2f); // Destruir o projétil após 2 segundos para evitar excesso de objetos na cena
    }
}
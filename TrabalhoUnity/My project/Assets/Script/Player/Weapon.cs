using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // O prefab do proj�til
    public Transform shootPoint; // O ponto de onde os proj�teis ser�o disparados
    public float shootForce = 40f; // A for�a com que os proj�teis s�o disparados
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
        Destroy(bullet, 2f); // Destruir o proj�til ap�s 2 segundos para evitar excesso de objetos na cena
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
/*
public GameObject bulletPrefab;
public float fireDelay = 0.25f;
private float cooldownTimer = 0;

// Update is called once per frame
void Update()
{
    cooldownTimer -= Time.deltaTime;
    if (Input.GetButton("Fire1") && cooldownTimer <= 0)
    {
        //shoot
        Debug.Log("Pew");
        cooldownTimer = fireDelay;

        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
*/
    public float maxSpeed = 5f;

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }


}

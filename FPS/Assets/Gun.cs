
using UnityEngine;

public class Gun : MonoBehaviour
{
  public float damage = 10f;
  public float firerate = 15f;
  public float Range = 100f;

    public Camera fpsCam;
    public ParticleSystem Muzzleflash;
    public GameObject Impact;
    public float impactforce = 60f;

    private float nextTimeToFire = 0f;


    // Update is called once per frame
    void Update()
    {
     if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Muzzleflash.Play();
            Shoot();
        }
    }

    void Shoot()
    {
        
        RaycastHit Hit;
       if( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, Range))
        {
            Debug.Log(Hit.transform.name);
            Enemy enemy = Hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

            }
            if(Hit.rigidbody != null)
            {
                Hit.rigidbody.AddForce(-Hit.normal * impactforce);
            }

            GameObject Impact_GO = Instantiate(Impact, Hit.point, Quaternion.LookRotation(Hit.normal));
            Destroy(Impact_GO, 0.5f);


        }
    }
}

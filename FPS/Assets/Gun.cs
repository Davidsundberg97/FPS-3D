
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
  public float damage = 10f;
  public float firerate = 15f;
  public float Range = 100f;
  public float impactforce = 60f;

    public Animator animator;


    public int maxAmmo = 10;
    private int currentAmmo;
    public int currAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem Muzzleflash;
    public GameObject Impact;
    

    private float nextTimeToFire = 0f;

    private void Start()
    {
            currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Realoading", false);
    }


    // Update is called once per frame
    void Update()
    {

        currAmmo = currentAmmo;
        if (isReloading)
            return;

        if(currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine( Reload());
                return;
        }

     if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Muzzleflash.Play();
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool(("Realoading"), true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool(("Realoading"), false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {

        currentAmmo--;

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

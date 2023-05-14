using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject submarine;
    public GameObject torpedo;
    public float launchVelocity = 700f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(torpedo, transform.position ,transform.rotation);
           
            projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * launchVelocity);
            StartCoroutine(DestroyTorp(projectile));
        }
    }

    IEnumerator DestroyTorp(GameObject obj)
    {


        yield return new WaitForSeconds(5f);
        Destroy(obj);

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float lifetime;

    private Transform pTransfrom;
    private float timer = 0.0f; 
 
    // Start is called before the first frame update
    void Start()
    {
        pTransfrom = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        timer += Time.deltaTime;
        if(timer >= lifetime)
        {
            Destroy(this.gameObject);
        }
       
    }
}

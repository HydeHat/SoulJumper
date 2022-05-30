using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform muzzelTransform;
    [SerializeField] float fireRate = 5.0f;

    [SerializeField] bool isFiring = false;
    [SerializeField] float timer = 100;
    [SerializeField] int _shotSound;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetFiring(bool firing)
    {
        isFiring = firing;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer  >= 60 / fireRate && isFiring)
        {
            if (Instantiate(projectile, muzzelTransform.position, muzzelTransform.rotation)) 
            {
                Events.Instance.PlayFXSoundClip(_shotSound);
            }
            timer = 0f;
        }
    }




}

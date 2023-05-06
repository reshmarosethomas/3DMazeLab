using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollectGem : MonoBehaviour
{
    public ParticleSystem CollectEffect;
    GameManager GM;
    //private AudioSource collectSFX;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //collectSFX = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider col)
    {   
        if (col.name =="PlayerCapsule")
        {
            //collectSFX.Play();

            GM.updateGemCount();

            Destroy(gameObject);
            CollectEffect.Play();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

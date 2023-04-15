using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGem : MonoBehaviour
{
    public ParticleSystem CollectEffect;
    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.name =="PlayerCapsule")
        {
            Debug.Log("collided");
            GM.updateGemCount();

            //gameObject.SetActive(false);
            Destroy(gameObject);
            CollectEffect.Play();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestructor : MonoBehaviour
{
    [SerializeField] public float delay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);    
    }

    
}

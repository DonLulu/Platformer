using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    public GameObject _object;
    
    // Start is called before the first frame update
    public void Spawn()
    {
        Instantiate(_object, transform.position, quaternion.identity);
    }
}

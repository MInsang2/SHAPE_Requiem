using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Trigger_Requiem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
#endif
}

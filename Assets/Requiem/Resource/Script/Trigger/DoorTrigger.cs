using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_Dungeon;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Door door;
    [SerializeField] KeyDoor keyDoor;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player && keyDoor.isOpened)
        {
            door.IsOpened = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)LayerName.Player)
        {
            door.IsOpened = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, gameObject.tag);
    }
#endif
}

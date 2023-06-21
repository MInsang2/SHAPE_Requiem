using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_Dungeon;

public class DoorTrigger : Trigger_Requiem
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
}

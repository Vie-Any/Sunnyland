using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsController : MonoBehaviour
{
    public void Bomb()
    {
        // update score of the player
        FindObjectOfType<PlayerController>().UpdateScore();
        Destroy(gameObject);
    }
}

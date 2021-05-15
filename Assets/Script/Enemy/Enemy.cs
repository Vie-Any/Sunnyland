using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base script of enemy
public class Enemy : MonoBehaviour
{
    // the render of current sprite
    private SpriteRenderer sprite;
    
    // the animatior of current enemy
    protected Animator animator;

    // mark the current enemy is dead or not
    protected bool isDead = false;

    // audio source object
    protected AudioSource deathAudio;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }
    
    // the enemy was hitted by player
    public void getHit()
    {
        isDead = true;
        // change the tag of current enemy so that the player will not trigger hit again
        tag = "Death";
        // change the sorting layer name of current enemy so that the player will not detect the enemy again
        sprite.sortingLayerName = "Dead";
        // change the layer of current enemy so that the enemy can not collision any other object
        gameObject.layer = LayerMask.NameToLayer("Dead");
        // play the death audio of current enemy
        deathAudio.Play();
        // trigger death animation
        animator.SetTrigger("death");
    }

    // destory current enemy from the scene
    public void death()
    {
        // destory current enemy from scene
        Destroy(gameObject);
    }
}

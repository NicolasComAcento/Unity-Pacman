using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    
    public Movement movement { get; private set; }

    public DogHome home { get; private set; }

    public DogScatter scatter { get; private set; }

    public DogChase chase { get; private set; }

    public DogFrightened frightened { get; private set; }

    public DogBehavior initialBehavior;

    public Transform target;


    public int points = 300;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<DogHome>();
        this.scatter = GetComponent<DogScatter>();
        this.chase = GetComponent<DogChase>();
        this.frightened = GetComponent<DogFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }
        if(this.initialBehavior != null)
        {
            this.initialBehavior.Enable();

        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Rogério"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().DogKnockout(this);
            } else
            {
                FindObjectOfType<GameManager>().DuckBited();
            }
        }
    }
}

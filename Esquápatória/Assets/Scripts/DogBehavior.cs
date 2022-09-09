 
using UnityEngine;

[RequireComponent(typeof(Dog))]
public abstract class DogBehavior : MonoBehaviour
{
   public Dog dog { get; private set; }
    public float duration; 
   private void Awake()
    {
        this.dog = GetComponent<Dog>();
        
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}

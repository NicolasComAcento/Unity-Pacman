 
using UnityEngine;

public class Passage : MonoBehaviour
{

   public Transform Conex�o;

   private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = this.Conex�o.position.x;
        position.y = this.Conex�o.position.y;

        other.transform.position = position;
    }
}

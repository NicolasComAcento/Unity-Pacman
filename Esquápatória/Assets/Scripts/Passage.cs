 
using UnityEngine;

public class Passage : MonoBehaviour
{

   public Transform Conexão;

   private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = this.Conexão.position.x;
        position.y = this.Conexão.position.y;

        other.transform.position = position;
    }
}

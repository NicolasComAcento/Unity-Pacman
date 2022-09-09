
using UnityEngine;

public class DogChase : DogBehavior
{
    private void OnDisable()
    {
        this.dog.scatter.Enable();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Não faz nada quando o cachorro está com medo
        if (node != null && this.enabled && !dog.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach(Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.dog.target.position - newPosition).sqrMagnitude;

                if(distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }
            this.dog.movement.SetDirection(direction);
        }

    }
}

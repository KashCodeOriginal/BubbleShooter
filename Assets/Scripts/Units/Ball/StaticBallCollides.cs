using UnityEngine;
using UnityEngine.Events;

public class StaticBallCollides : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var directionVector3 = gameObject.transform.position - col.transform.position;

        var direction = new Vector2(directionVector3.x, directionVector3.y).normalized;
        
    }
}

using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event Action OnObstacleCollision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            OnObstacleCollision?.Invoke();
            Debug.Log("Death Screen should appear now");
        }
        if (collision.gameObject.tag == "Exit")
        {
            MainMenu.ChangeScene(0);
        }
    }
}

using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int currentSceneID;
    public static event Action OnObstacleCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            OnObstacleCollision?.Invoke();

            Invoke("ChangeLevel", 2.4f);
        }
        if (collision.gameObject.tag == "Exit")
        {
            FindObjectOfType<AudioManager>().Play("NextLevel");
            currentSceneID++;
            Invoke("ChangeLevel", 0.4f);
        }
    }

    private void ChangeLevel()
    {
        SceneManager.LoadScene(currentSceneID);
    }
}

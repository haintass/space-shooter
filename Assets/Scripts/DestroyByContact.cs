using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion; //игровой объект взрыва врага
    public GameObject playerExplosion; //игровой объект взрыва корабля игрока
    public int scoreValue;
    private GameController gameController;
    public int healthHazards;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Не найден скрипт 'GameController;");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.tag == "Asteroid")
        {
            Instantiate(explosion, transform.position, transform.rotation); //создание взрыва при уничтожении врага
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation); //создание взрыва при уничтожении врага
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //создание взрыва при уничтожении корабля игрока
            Destroy(gameObject);
            gameController.GameOver(); //
        }

        if (other.tag == "Bolt")
        {
            healthHazards--;
            if (healthHazards == 0)
            {
                gameController.AddScore(scoreValue);
                Instantiate(explosion, transform.position, transform.rotation); //создание взрыва при уничтожении врага
                Destroy(gameObject);
            }
        }

        Destroy(other.gameObject);
    }
}
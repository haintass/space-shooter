using UnityEngine;
using System.Collections;

public class MoverAsteroid : MonoBehaviour
{
    Transform playerShip; // Переменная для координат объекта корабля игрока
    private float speed; // Скорость движения врага
    Vector3 delta;

    public void Start()
    {
        if (GameObject.Find("Player") == null)
        {
            return;
        }
        playerShip = GameObject.Find("Player").transform; //присваиваем "player" координаты игрока
        delta = playerShip.position - transform.position; //вычисления разницы между вражеским объектом и кораблём
        delta.Normalize();
        speed = Random.Range(4, 8);
    }

    public void Update()
    {

        float moveSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
    }
}
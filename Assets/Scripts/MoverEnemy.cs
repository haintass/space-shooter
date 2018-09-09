using UnityEngine;
using System.Collections;

public class MoverEnemy : MonoBehaviour
{

    Transform playerShip; // Переменная для координат объекта корабля игрока
    private float speed, moveSpeed; // Скорость движения врага
    private Vector3 delta;
    Vector3 speedRotate;

    private float randDistance;
    private float randDirection;
    float posX;
    float posZ;
    

    void Awake()
    {
        if (GameObject.Find("Player") != null)
        {
            playerShip = GameObject.Find("Player").transform; //присваиваем "playerShip" координаты игрока
        }
        else
            playerShip = null;
    }

    void Start()
    {
        // Задаём случайные переменные для расстояния и направления
        randDistance = Random.Range(100, 150);
        randDirection = Random.Range(0, 360);
        // Используем переменные для задания координат
        posX = transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
        posZ = transform.position.z + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
    }

    public void Update()
    {
        speed = Random.Range(3, 6);

        if (GameObject.Find("Player") == true) //Если корабль игрока не уничтожен, то враги перемещаются за ним
        {
            //движение в сторону игрока
            delta = playerShip.position - transform.position; //вычисления разницы между вражеским объектом и кораблём
            delta.Normalize();
            moveSpeed = speed * Time.deltaTime;
            transform.position += (delta * moveSpeed);

            //поворот к игроку
            Vector3 targetPoint = playerShip.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime) * Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * 0 * 2 * Time.deltaTime);
        }
        else // Если корабль игрока уничтожен, создаем случайную точку и направляем вражеский корабль в данную точку
        {
            Vector3 movementOnDot = new Vector3(posX, 0, posZ) - transform.position;
            movementOnDot.Normalize();
            moveSpeed = speed * Time.deltaTime;
            transform.position += (movementOnDot * moveSpeed);

            //Поворот в сторону точки движения
            Vector3 lookAtDot = new Vector3(posX, 0, posZ);
            Quaternion targetRotation = Quaternion.LookRotation(lookAtDot - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime) * Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * 2 * Time.deltaTime);
        }
    }
}
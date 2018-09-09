using UnityEngine;
using System.Collections;

[System.Serializable]

//создание границ упора персонажа, чтобы корабль не уходил за пределы сцены
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed; //скорость корабля игрока
    public float tilt;//наклон корабля игрока при движении
    public Boundary boundary;

    public GameObject shot;//префаб лазера
    public Transform shotSpawn;//переменная, указывающая в каких координатах появляется объект shot
    public float fireRate;

    private float nextFire;


    //функция реализации выстрелов лазером
    void Update()
    {
        //выстрел произойдет, если Input.GetButton истинно и время в игре болше чем nextFire
        if (Input.GetButton("Fire1") && Time.time > nextFire) //Fire1 задан в диспетчере ввода и используется в Input.GetButton
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }
    //функция управления кораблем
    void FixedUpdate()
    {
        var rigidbody = GetComponent<Rigidbody>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //отвечает за наклоны корабля при движении
        //rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        //не дает кораблю игрока выходить за пределы карты. Ограничение перемещения можно устанавливать в "инспекторе"
        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        Plane playerShip = new Plane(Vector3.up, transform.position);
        //определяет позицию курсора
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
        // Если луч параллелен плоскости, Raycast вернет ложное значение.
        if (playerShip.Raycast(ray, out hitdist))
        {
            // Определение целевой ротации. Это вращение, если преобразование смотрит на целевую точку.
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            // Поворот к целевой точке.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime) * Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt * 2 * Time.deltaTime);
        }
    }

}
using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    //используется для уничтожения объектов, ушедших за сцену
    void OnTriggerExit(Collider other)
    {

        Destroy(other.gameObject);
    }
}
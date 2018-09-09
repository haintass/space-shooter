using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    private bool paused = false;
    private int window = 0;
    bool fullScreens = true;

    void Update()
    {
        // Ставим игру на паузу
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
                window = 1;
            }
            else
            {
                Time.timeScale = 1;
                paused = false;
                window = 0;
            }
        }
    }
    void OnGUI()
    {
        if (window == 1) //Главное меню
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 50, 220, 120), "Пауза");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 25), "Продолжить"))
            {
                Time.timeScale = 1;
                paused = false;
                window = 0;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 5, 200, 25), "Главное меню"))
            {
                window = 3;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 35, 200, 25), "Выход из игры"))
            {
                window = 2;
            }
        }

        if (window == 2) //Подтверждение выхода
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 50, 220, 160), "Выход");
            GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height / 2, 200, 25), "Закрыть игру?");

            if (GUI.Button(new Rect(Screen.width / 2 - 105, Screen.height / 2 + 25, 100, 25), "Да"))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 5, Screen.height / 2 + 25, 100, 25), "Нет"))
            {
                window = 1;
            }
        }
        if (window == 3) //Подтверждение выхода в главное меню
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 50, 220, 160), "Выход в главное меню");
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2, 200, 25), "Выйти в главное меню?");

            if (GUI.Button(new Rect(Screen.width / 2 - 105, Screen.height / 2 + 25, 100, 25), "Да"))
            {
                Application.LoadLevel("Menu");
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 5, Screen.height / 2 + 25, 100, 25), "Нет"))
            {
                window = 1;
            }
        }
    }
}
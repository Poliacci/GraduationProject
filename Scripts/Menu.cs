using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Scene1()
    {
        SceneManager.LoadScene(1); //загружаем 0 сцену
    }
    public void Scene2()
    {
        SceneManager.LoadScene(2); //перезагрузка текущей сцены
    }

    public void Scene3()
    {
        //Application.Quit();
        SceneManager.LoadScene(3);
    }

    public void Scene4()
    {
        //Application.Quit();
        SceneManager.LoadScene(4);
    }

    public void Scene5()
    {
        //Application.Quit();
        SceneManager.LoadScene(5);
    }

    public void MenuScene()
    {
        //Application.Quit();
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }

}





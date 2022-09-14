using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [Header("Velocidad de bala")]
    public float speed = 6f;

    [Header("Tiempo de destruccion de bala")]
    public float destroyTimer = 3f;

    public bool isEnemyBullet = false;

    void Start()
    {
        //checar si se trata de la bala del enemigo
        if (isEnemyBullet)
        {
            //se agrega un valor negativo al speed
            speed *= -1f;
        }
        //invoca la destruccion de la bala
        Invoke("DestroyGameObject", destroyTimer); //invocar a la funcion de destruccion de la bala    
    }

    void Update()
    {
        moveBullet(); //llamara a la funcion que mueve la bala
    }

    void moveBullet() //funcion para mover la bala
    {
        Vector3 _temp = transform.position; //crear una variable temporal que guarda la posicion de la bala

        _temp.x += speed * Time.deltaTime; //adicion de la posicion temporal en X

        transform.position = _temp; //actializar la posicion de bala usando el vector temporal
    }

    void DestroyGameObject() //funcion para destruir la bala
    {
        Destroy(gameObject); //destruir el objeto que tiene este script
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        //vamos a checar si el objeto entrante tiene tags especificos
        if (_other.tag == "Bullet" || _other.tag == "Enemy")
        {
            //llamar a ala funcion que destrulle la bala
            DestroyGameObject();
        }
    }
}

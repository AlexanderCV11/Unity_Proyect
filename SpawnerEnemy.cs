using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [Header("Limites en Y")]
    public float minY = -4.4f; // limite inferior en Y
    public float maxY = 4.3f; //limite superioren Y

    [Header("Asignacion de enemigos")]
    public GameObject[] asteroidPrefabs; //arreglo de asteroides
    public GameObject enemyShipPrefab; //go de nave enemiga

    [Header("Timer de Generacion")]
    public float timer = 2f; //tiempo de aparicion de enemigos

    void Start()
    {
        //llamar a la funcion para generar enemigos
        //aparecen al inicio del juego
        Invoke("SpawnEnemies", timer);
    }

    void SpawnEnemies()
    {
        //crear la posicion local tomando en cuenta los limites de Y
        float _posY = Random.Range(minY, maxY);
        //Valor local que guarda la posicion del generador
        Vector3 _temp = transform.position;
        //igualar la posicion temporal en Y con la posicion local en Y
        _temp.y = _posY;
        //vamos a evaluar un rango aleatorio para generar enemigos
        if (Random.Range(0, 2) > 0)
        {
            //SI EL VALOR ES 1, instanciamos un asteroide
            Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], _temp, Quaternion.identity);
        }
        else
        {
            //si el valor es 0, instanciamos una nave enemiga
            Instantiate(enemyShipPrefab, _temp, Quaternion.Euler(0f, 0f, 90f));
        }

        //invocacion de funcion para generar enemigos usando un temporizador
        Invoke("SpawnEnemies", timer);
    }
}

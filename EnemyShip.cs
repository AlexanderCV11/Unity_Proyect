using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("Variables de enemigo")]
    public float speed = 5f; //velocidad del enemigo
    public float rotateSpeed = 50f; //velocidad de rotacion

    [Header("Propiedades de enemigo")]
    public bool canShoot; //enemigo puede disparar? Y/N
    public bool canRotate; //enemigo puee rotar? Y/N

    public bool canMove = true; //enemigo se puede mover? Y/N

    [Header("Limite de enemigo")]
    public float boundX = -10f; // limite de avance de enemigo en -x

    [Header("Asignacion")]
    public Transform attackPoint; //punto de ataque del enemigo
    public GameObject bulletPrefab; //referencia de Go de bala

    private Animator anim; //referencia componente animator del enemigo
    private AudioSource explosionSound; //referencia componente audiosource del enemigo

    private void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //si el random vale 0
        rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);

        if (canRotate) //checamos si el enemigo puede rotar
        {
            if (Random.Range(0, 2) > 0)
            {
                //si el random vale 1
                rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f); //primer valor de velocidad de rotacion

                rotateSpeed *= -1f; //cambio de sentido a la rotacion
            }
        }

        if (canShoot) //checa si el enemigo puede disparar
        {
            //si es el caso, vamos a invocar al metodo de disparo aleatorio
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }

    private void Update()
    {
        MoveEnemy(); //llamar al metodo de movimiento
        RotateEnemy(); //llamar al metodo de rotaciones de asteroides
    }

    private void MoveEnemy()//metodo de movimiento
    {
        if (canMove)//checar si el enemigo se puede mover
        {
            Vector3 _temp = transform.position; //variable que guarda la posicion del actual enemigo

            _temp.x -= speed * Time.deltaTime; //decremento de la pos X del enemigo usando el tiempo

            transform.position = _temp; //actualizar la posicion de este objeto usando el vector temp

            //validar el limite de -x de la posicion del enemigo
            if (_temp.x < boundX) //checar si el valor del _temp en x es menor al valor del limite en x
            {
                Destroy(gameObject); //destruir al gamobject que tiene este script
            }
        }
    }

    void RotateEnemy()
    {
        if (canRotate)//checar si el enemigo puede rotar
        {
            //aplicar la rotacion con respecto al mundo
            transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime), Space.World);
        }
    }

    //metodo de disparo
    void StartShooting()
    {
        //Guardar la instancia de la bala dentro de una variable
        GameObject _bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);

        //Acceder a la variable isEnemyBullet para cambiar la direccion de disparo
        _bullet.GetComponent<bulletScript>().isEnemyBullet = true;

        //checar si el enemigo puede dispara
        if (canShoot)
        {
            //se invoca este metodo usando un tiempo aleatorio de de 1 a 2S
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }

    //funcion para destruir Gameobject
    void DestroyGameObject()
    {
        //destruir el objeto que contenga este script
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        //checar si el objeto entrante tiene el tag de "Bullet"
        if (_other.tag == "Bullet")
        {
            canMove = false;
            //checamos si el enemigo puede disparar
            if (canShoot)
            {
                //el enemigo deja de disparar
                canShoot = false;
                //se deja de invocar al metodo de disparo
                CancelInvoke("StartShooting");
            }

            //llamar a la funcion de destruccion de objetos despues de 3s
            Invoke("DestroyGameObject", 0f);
        }
    }
}

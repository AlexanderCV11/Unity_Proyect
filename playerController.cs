using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Velocidad de player")]
    public float speed = 5f; //velocidad del jugador

    [Header("Limites en Y")]
    public float minY; //limites en el eje X
    public float maxY;

    [Header("Delay de disparo")]
    public float attackTimer = 0.4f; //timer de disparo


    [SerializeField]
    private GameObject playerBullet; //referencia del gameobject de la bala del jugador

    [SerializeField]
    private Transform attackPoint; //punto de origen del disparo

    private float currentAttackTimer; //tiempo de ataque actual

    private bool canAttack; //el jugador puede disparar? Y/N

    void Start()
    {
        currentAttackTimer = attackTimer; //declaracion del tiempo de ataque actual
    }

    void Update()
    {
        movePlayer(); //llamar a la funcion que mueve al jugador
        Attack(); //llamar a la funcion de disparo
    }

    void movePlayer() //funcion que mueve al jugador
    {
        if (Input.GetAxisRaw("Vertical") > 0f /*chaca si el valor del eje vertical es mayor a 0*/) //movimiento vertical
        {
            Vector3 _temp = transform.position; //crear variable temporal que guarda la posicion del player
            _temp.y += speed * Time.deltaTime; //declarar el valor en Y del vector temporal para ascender
            if (_temp.y > maxY) //limitar el movimiento superior en Y
                _temp.y = maxY;
            transform.position = _temp; //actualizar la posicion del jugador usando los valores temporales
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 _temp = transform.position; //crear variable temporal que guarda la posicion del player
            _temp.y -= speed * Time.deltaTime; //declarar el valor en Y del vector temporal para descender
            if (_temp.y < minY) //limitar el movimiento inferior en Y
                _temp.y = minY;
            transform.position = _temp; //actualizar la posicion del jugador usando los valores temporales
        }
    }

    void Attack() //funcion para ataque
    {
        attackTimer += Time.deltaTime; //addicion de tiempo de ataque
        if (attackTimer > currentAttackTimer) //checar si el tiemp de ataque es mayor al default
        {
            canAttack = true; //el jugador puede disparar
        }

        if (Input.GetKeyDown(KeyCode.Space)) //input de disparo
        {
            if (canAttack) //checar si el jugador puede atacar
            {
                canAttack = false; //el jugador ya no puede atacar, evitar spam
                attackTimer = 0.0f; //se resetea el tiempo de atque
                Instantiate(playerBullet, attackPoint.position, Quaternion.identity); //instanciar la bala
                //reproducir sonido de bala
            }
        }
    }

}




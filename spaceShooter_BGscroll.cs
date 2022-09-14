using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceShooter_BGscroll : MonoBehaviour
{
    public float scrollSpeed = 0.1f; //velocidad de scroll

    private MeshRenderer meshRenderer; //referencia del componente meshRenderer
    private float xScroll; //valor del scrolling en x

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>(); //inicializar la referencia del meshRenderer
    }

    private void Update()
    {
        scroll(); //llamar al metodo para scrollear rl background
    }

    void scroll()
    {
        xScroll = Time.time * scrollSpeed; //determinar el valor de scroll usando la velocidad y el tiempo
        Vector2 _offset = new Vector2(xScroll, 0f); //determinar el desplazamiento al scrollear

        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", _offset); //modificar textura usando el offset
    }
}

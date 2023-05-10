using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    
        public float velocidadMovimiento = 5f;
        public float limiteSuperior = 3.83f;
        public float limiteInferior = -3.83f;

        void Update()
        {
            // Mover hacia arriba al presionar el botón "Arriba"
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * velocidadMovimiento * Time.deltaTime);

                // Mantener el sprite dentro del límite superior
                if (transform.position.y > limiteSuperior)
                {
                    transform.position = new Vector3(transform.position.x, limiteSuperior, transform.position.z);
                }
            }

            // Mover hacia abajo al presionar el botón "Abajo"
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * velocidadMovimiento * Time.deltaTime);

                // Mantener el sprite dentro del límite inferior
                if (transform.position.y < limiteInferior)
                {
                    transform.position = new Vector3(transform.position.x, limiteInferior, transform.position.z);
                }
            }
        }
    
}

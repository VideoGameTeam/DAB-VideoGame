using UnityEngine;
using System.Collections;

public class SpardaShootController : MonoBehaviour {

    public float velocity;
    public float proyectileLife;
	public GameObject Explosion;

    void Start()
    {
        Destroy(gameObject, proyectileLife);
    }

	void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * velocity);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
       		Gamestate.EstadoJuego.ChangeHealth (-5);
			//Instanciar Explosion
			Instantiate (Explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);

        }
    }
}

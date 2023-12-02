using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float timer;
    private bool couvat;

    private Quaternion plusRotace  = Quaternion.Euler(0, -90, 0);
    private Quaternion plusRotace2 = Quaternion.Euler(0, -180, 0);

    public TMP_Text score;
    private int penize;

    // Start is called before the first frame update
    void Start()
    {
        couvat = false;
    }

    // Update is called once per frame
    void Update()
    {
        pridavaniPenez();

        if (couvat == true)
        {
            timer = timer + Time.deltaTime;
        }
        transform.position += transform.forward * speed * Time.deltaTime;
        if (timer > 0.2)
        {
            transform.rotation = transform.rotation * plusRotace;
            couvat = false;
            timer = 0;
        }
    }
    public void pridavaniPenez()
    {
        score.text = penize.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            couvat = true;
            transform.rotation = transform.rotation * plusRotace2;
        }
        if (collision.gameObject.CompareTag("RestartBox"))
        {
            Scene scena = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scena.name);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            penize = penize + 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Boost"))
        {
            speed = speed * 2;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Victory"))
        {
            if(penize >= 1)
            {
                SceneManager.LoadScene("Victory");
                //SceneManager.LoadScene("DalsiLevel");
            }
            else
            {
                Scene scena = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scena.name);
            }

        }
    }
}

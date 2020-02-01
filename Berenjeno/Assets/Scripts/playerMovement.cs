using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D berenjenoRB;
    public float maxVelocidad;
    Animator berenjenoAnim;
    // Voltear EL Berenjeno

    bool puedoMover = true;
    bool enSuelo = true;
    float checkRadioGround = 0.2f;
    public LayerMask capaGround;
    public Transform checkGround;
    public float jumpPower;

    bool voltearBerenjeno = true;
    SpriteRenderer berenjenoRender;
    // Start is called before the first frame update
    void Start()
    {
        berenjenoRB = GetComponent<Rigidbody2D> ();
        berenjenoRender = GetComponent<SpriteRenderer> ();
        berenjenoAnim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        //
        bool puedoMover = true;
       /* Debug.Log(puedoMover);
        Debug.Log(enSuelo);
        Debug.Log(Input.GetAxis("Jump"));*/
        if (puedoMover && enSuelo && Input.GetAxis("Jump") > 0)
        {
            Debug.Log(puedoMover);
            Debug.Log(enSuelo);
            Debug.Log(Input.GetAxis("Jump"));
            berenjenoAnim.SetBool ("is_in_ground", false);
            berenjenoRB.velocity = new Vector2(berenjenoRB.velocity.x, 0f);
            berenjenoRB.AddForce (new Vector2 (0, jumpPower),ForceMode2D.Impulse);
            enSuelo = false;
        }
        enSuelo = Physics2D.OverlapCircle (checkGround.position, checkRadioGround, capaGround);
        berenjenoAnim.SetBool("is_in_ground", enSuelo);

        float movemment = Input.GetAxis ("Horizontal");
        if (puedoMover)
        {
            if (movemment > 0 && !voltearBerenjeno)
            {
                berenjenoAnim.SetFloat("VelMovimiento", Mathf.Abs(movemment));
                Voltear ();
            }
            else if (movemment < 0 && voltearBerenjeno)
            {
                berenjenoAnim.SetFloat("VelMovimiento", Mathf.Abs(movemment));
                Voltear ();
            }
            berenjenoAnim.SetFloat("VelMovimiento", Mathf.Abs(movemment));
            //correr
            berenjenoRB.velocity = new Vector2 (movemment * maxVelocidad, berenjenoRB.velocity.y);
        }
        else
        {
            berenjenoRB.velocity = new Vector2 (0, berenjenoRB.velocity.y);
            berenjenoAnim.SetFloat("VelMovimiento", 0);
        }




    }

    void Voltear ()
    {
        voltearBerenjeno = !voltearBerenjeno;
        berenjenoRender.flipX = !berenjenoRender.flipX;
    }

    public void togglePuedeMover()
    {
        puedoMover = !puedoMover;
    }
}

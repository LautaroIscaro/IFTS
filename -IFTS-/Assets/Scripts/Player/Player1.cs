using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    private Rigidbody2D _rigid;
    private SpriteRenderer _playerSprite;
    private playerAnim _playerAnim;
 
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private float v;
    [SerializeField]
    bool isJumping = false; //Para comprobar si ya está saltando
    public float potenciaSalto; //Potencia de salto del jugador


    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<playerAnim>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        ///////////////////////////////////////////////////////////
        //                    CAMINAR
        ///////////////////////////////////////////////////////////
        float move = Input.GetAxisRaw("Horizontal");
        if (move > 0)
        {
            flip(true);
        }
        else if (move < 0)
        {
            flip(false);
        }       

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnim.Move(move);

        ///////////////////////////////////////////////////////////
        //                    SALTO
        ///////////////////////////////////////////////////////////
        v = Input.GetAxisRaw("Vertical");
        if(v >=0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                //Le aplico la fuerza de salto
                _rigid.velocity = new Vector2(_rigid.velocity.x, potenciaSalto);
                _playerAnim.Jump(true);
                isJumping = true;
            }
        }
        if (v <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                RaycastHit2D hitTerreno = Physics2D.Raycast(transform.position, Vector2.down, 15.0f, 1 << 8);               
                Debug.Log(hitTerreno.collider.name);
                if (hitTerreno.transform.gameObject.tag == "PlataformaTraspasable")
                {
                    hitTerreno.transform.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    StartCoroutine(Wait(hitTerreno));
                }
            }

        }



    }

    void flip(bool faceRigth)
    {
        if (faceRigth == true)
        {
            _playerSprite.flipX = false;
        }
        else if (faceRigth == false)
        {
            _playerSprite.flipX = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //Si el jugador colisiona con un objeto con la etiqueta suelo
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("PlataformaTraspasable") || other.gameObject.CompareTag("Trampolin"))
        {
            //Digo que no está saltando (para que pueda volver a saltar)
            isJumping = false;
            _playerAnim.Jump(false);
            //Le quito la fuerza de salto remanente que tuviera
            if (other.gameObject.CompareTag("Ground") )
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, 0);
            }
        }       


    }



    IEnumerator Wait(RaycastHit2D obj)
    {
        yield return new WaitForSeconds(1.0f);
        obj.transform.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        
       
    }


}

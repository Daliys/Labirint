using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float characterSpeed;
    public GameUICanvas uiCanvas;
    public MapGenerator mapGenerator;

    private float movementHorizontal;
    private float movementVertical;
    private Rigidbody2D rigidbody;
    Vector2 moveVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementHorizontal = Input.GetAxis("Horizontal");
        movementVertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        moveVelocity = new Vector2(movementHorizontal, movementVertical) * characterSpeed;
        if (movementHorizontal > 0) transform.transform.eulerAngles = new Vector2(0, 180);
        else transform.transform.eulerAngles = new Vector2(0, 0);

        rigidbody.MovePosition(rigidbody.position + moveVelocity * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            Destroy(collision.gameObject);

            if (Game.typeOfDifficulties == Game.TypeOfDifficulties.Speed)
            {
                Game.currestScore++;
                uiCanvas.UpdateScorePanel();
                mapGenerator.GenerateRandomGoal();

            }
            else
            {
                uiCanvas.ShowWonPanel();
            }
        }
    }
}

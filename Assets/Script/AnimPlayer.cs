using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AnimPlayer : NetworkBehaviour
{
    [SerializeField] private Animator anim;
    private float vertical;

    [Header("ModelsPlayer")]
    public GameObject model1;
    public GameObject model2;
    public GameObject model3;
    public GameObject model4;
    public GameObject model5;
    public GameObject model6;
    public GameObject model7;



    void Update()
    {
        if (isLocalPlayer == true)
        {
            // set layer

            int CharacterModel = LayerMask.NameToLayer("CharacterModel");
            model1.layer = CharacterModel;
            model2.layer = CharacterModel;
            model3.layer = CharacterModel;
            model4.layer = CharacterModel;
            model5.layer = CharacterModel;
            model6.layer = CharacterModel;
            model7.layer = CharacterModel;

            // anim var
            anim.SetFloat("Vertical", vertical);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                vertical = Input.GetAxis("Vertical");
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") != 0){
                    vertical = 2 / 2;
                }
                else if (Input.GetAxisRaw("Horizontal") != 0){
                    vertical = 2 / 2;
                }
                else if (Input.GetAxisRaw("Vertical") == 0 || Input.GetAxisRaw("Vertical") == 0){
                    vertical = 0;
                }

                
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum TypePlayer { orange, vert, rouge};

public class CarController : MonoBehaviour {

    [SerializeField] float normalSpeed = 4f, currentSpeed;
    [SerializeField] bool canJump = false;
    [SerializeField] Texture orangeTex, vertText, rougeTex;
    PlayFabManager playFabManager;

    public TypePlayer myTypePlayer;

	void Awake () {

        playFabManager = GameObject.Find("PlayFabManager").GetComponent<PlayFabManager>();

        //Playfab Manager	
        
        if(playFabManager.Player_Car=="rouge")
        {
            myTypePlayer = TypePlayer.rouge;
        }
        else if (playFabManager.Player_Car == "vert")
        {
            myTypePlayer = TypePlayer.vert;
        }
        else
        {
            myTypePlayer = TypePlayer.orange;
        }


        switch (myTypePlayer)
        {
            case TypePlayer.orange:
                currentSpeed = normalSpeed;
                canJump = false;
                GetComponent<MeshRenderer>().material.mainTexture = orangeTex;
                break;

            case TypePlayer.vert:
                currentSpeed = normalSpeed * 2;
                canJump = false;
                GetComponent<MeshRenderer>().material.mainTexture = vertText;
                break;

            case TypePlayer.rouge:
                currentSpeed = normalSpeed * 2;
                canJump = true;
                GetComponent<MeshRenderer>().material.mainTexture = rougeTex;
                break;
        }

	}

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * CrossPlatformInputManager.GetAxis("Vertical"));
        transform.Rotate(Vector3.up * Time.deltaTime * normalSpeed * 30 * CrossPlatformInputManager.GetAxis("Horizontal"));
    }

    private void FixedUpdate()
    {
        if(canJump && CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
        }
    }




}

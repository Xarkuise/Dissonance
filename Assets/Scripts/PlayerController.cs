using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{

    [Header("References")]

    public Rigidbody rb;
    public Transform head;
    public Camera camera;


    [Header("Configurations")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float itemPickupDistance;



    [Header("Camera Effects")]
    public float baseCameraHeight = .85f;

    public float walkBobbingRate = .75f;
    public float runBobbingRate = 1f;
    public float maxWalkBobbingOffset = .2f;
    public float maxRunBobbingOffset = .3f;


    [Header("Audio")]
    public AudioSource audioWalk;
    public AudioSource audioPickup;


    [Header("Runtime")]
    Vector3 newVelocity;
    bool isGrounded = false;
    bool isJumping = false;

    Transform attachedObject = null;
    float attachedDistance = 2.75f;                                  //was set to 0 before

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        //Horizontal rotation
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);

        //Run Speed and Walk Speed
        newVelocity = Vector3.up * rb.velocity.y;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;

        //Jump
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                newVelocity.y = jumpSpeed;
                isJumping = true;
            }
        }

        bool isMovingOnGround = (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f) && isGrounded;

        if (isMovingOnGround)
        {
            float bobbingRate = Input.GetKey(KeyCode.LeftShift) ? runBobbingRate : walkBobbingRate;
            float bobbingOffset = Input.GetKey(KeyCode.LeftShift) ? maxRunBobbingOffset : maxWalkBobbingOffset;
            Vector3 targetHeadPosition = Vector3.up * baseCameraHeight + Vector3.up * (Mathf.PingPong(Time.time * bobbingRate, bobbingOffset) - bobbingOffset * .5f);
            head.localPosition = Vector3.Lerp(head.localPosition, targetHeadPosition, .1f);
        }

        rb.velocity = transform.TransformDirection(newVelocity);   //Follow the head(mouse) rotation movement

        //Audio
        if (isMovingOnGround)
        {
            if (!audioWalk.isPlaying) audioWalk.Play(); // Play only if not already playing
        }
        else
        {
            audioWalk.Stop(); // Stop the audio when not moving
        }

        audioWalk.pitch = Input.GetKey(KeyCode.LeftShift) ? 1.75f : 1f;

        // Picking Items
        RaycastHit hit;
        bool cast = Physics.Raycast(head.position, head.forward, out hit, itemPickupDistance);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (attachedObject != null)
            {
                attachedObject.SetParent(null);

                if (attachedObject.GetComponent<Rigidbody>() != null)
                    attachedObject.GetComponent<Rigidbody>().isKinematic = false;

                if (attachedObject.GetComponent<Collider>() != null)
                    attachedObject.GetComponent<Collider>().enabled = true;

                attachedObject = null;
            }
            else
            {
                if (cast)
                {
                    if (hit.transform.CompareTag("pickable") || hit.transform.CompareTag("MusicBox"))
                    {
                        attachedObject = hit.transform;
                        attachedObject.SetParent(transform);

                        if (attachedObject.GetComponent<Rigidbody>() != null)
                            attachedObject.GetComponent<Rigidbody>().isKinematic = true;

                        if (attachedObject.GetComponent<Collider>() != null)
                            attachedObject.GetComponent<Collider>().enabled = false;

                        if (audioPickup != null)
                            audioPickup.PlayOneShot(audioPickup.clip);

                        // Stop the music when picking up the music box
                        AudioSource music = attachedObject.GetComponent<AudioSource>();
                        if (music != null)
                        {
                            music.Stop();
                            Debug.Log("Music stopped for: " + attachedObject.name);
                        }

                        // Reveal the hidden door (if NextLevel script is present)
                        NextLevel nextLevel = FindObjectOfType<NextLevel>();
                        if (nextLevel != null && nextLevel.hiddenDoor != null)
                        {
                            nextLevel.hiddenDoor.SetActive(true);
                            Debug.Log("Hidden door revealed after picking up the music box!");
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            isGrounded = true;
        }

        else isGrounded = false;
    }

    private void LateUpdate()
    {
        //Vertical Rotation
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;
        e.x = RestrictAngle(e.x, -85f, 85f);
        head.eulerAngles = e;

        //update the position as well as the rotation of the attached object
        if (attachedObject != null)
        {
            attachedObject.position = head.position + head.forward * attachedDistance;
            attachedObject.Rotate(transform.right * Input.mouseScrollDelta.y * 30f, Space.World); //allows objects to be rotated forwards/backwards
        }

        // Update the position and rotation of the attached object
        if (attachedObject != null)
        {
            // Set the position in front of the player with an additional height offset
            attachedObject.position = head.position + head.forward * attachedDistance + Vector3.up * 0.5f; // Adjust height here (0.5f for example)

            // Optionally rotate the object based on mouse scroll (or other input)
            attachedObject.Rotate(transform.right * Input.mouseScrollDelta.y * 30f, Space.World); // Rotate object using mouse scroll
        }
    }


    void OnCollisionStay(Collision col)                       //placed collisionstay and collision Exit before restrict angle
    {
        isGrounded = true;
        isJumping = false;
    }

    void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }

    public static float RestrictAngle(float angle, float angleMin, float angleMax)
    {
        //Angle of the Player
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        if (angle > angleMax)
            angle = angleMax;
        if (angle < angleMin)
            angle = angleMin;

        return angle;
    }


}



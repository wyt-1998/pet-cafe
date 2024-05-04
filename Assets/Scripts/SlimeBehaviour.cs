using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class SlimeBehaviour : MonoBehaviour
{
    [Header("Slime properties")]
    private float jumpingForce = 10000f;
    [SerializeField] private float size;
    private float currentSize;
    private Transform landmark_1;
    private Transform landmark_2;

    [SerializeField] private float time2Awake; //awake after x amout of second after initiate
    [SerializeField] private float awakeTimer;
    private bool isAwake = false;
    [SerializeField] private int numPickup = 0;

    [Header("Behaviour")]
    [SerializeField] private float sightDistance; //to see the player
    [SerializeField] private float interactDistance;
    private XRGeneralGrabTransformer grabTransformer;
    //getting relelivent properties

    [Header("Prefab")]
    [SerializeField] private GameObject slimePrefab;
    private GameObject playerGameobject;
    [SerializeField] private GameObject slimeBody;
    private Rigidbody rb;

    [Header("Audio")]
    [SerializeField] private float minPitch = -1.5f; // Minimum pitch value
    [SerializeField] private float maxPitch = 1.5f; // Maximum pitch value
    [SerializeField] private float pitch;
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip eatSound;
    [SerializeField] private AudioClip splitSound;
    private AudioSource audioSource;

    private float timer;
    private float timerBeforeTransform;
    private Vector3 cur_destination;
    private Vector3 velocityBeforePhysicsUpdate;
    private Material faceMaterial;

    [Header("Texture")]
    [SerializeField] private Texture idleFace;
    [SerializeField] private Texture walkFace;
    [SerializeField] private Texture jumpFace;
    [SerializeField] private Texture attackFace;
    [SerializeField] private Texture damageFace;

    // Start is called before the first frame update
    void Start()
    {
        grabTransformer = gameObject.GetComponent<XRGeneralGrabTransformer>();
        grabTransformer.allowTwoHandedScaling = false;
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        faceMaterial = slimeBody.GetComponent<Renderer>().materials[1];

        playerGameobject = GameObject.Find("PlayerPosition");
        landmark_1 = GameObject.Find("landmark_1").transform;
        landmark_2 = GameObject.Find("landmark_2").transform;

        cur_destination = GetRandomPos();
        slimeBody = this.transform.GetChild(1).gameObject;
        awakeTimer = Time.time + time2Awake;
        // when initialise set state to vulnerable and set countdown 2 second until able to move to wondering around state

        size = transform.localScale.x;
        pitch = Mathf.Lerp(minPitch, maxPitch, size);
        audioSource.pitch = pitch;
    }
    public void SetSize(float s)
    {
        size = s;
    }
    //void SizeAdjust()
    //{
    //    if (size == currentSize)
    //    {
    //        return;
    //    }
    //    float floatSize = 0.4f + (size / 15);
    //    transform.localScale = new Vector3(floatSize, floatSize, floatSize);

    //    currentSize = size;
    //    pitch = Mathf.Lerp(minPitch, maxPitch, size);
    //    audioSource.pitch = pitch;
    //}
    public void GetGrabed(SelectEnterEventArgs arg)
    {
        if (arg.interactorObject.transform == GameObject.Find("LeftHand Controller").transform
            || arg.interactorObject.transform == GameObject.Find("RightHand Controller").transform)
        {
            //Debug.Log("weeh i go grab");
            numPickup ++;
            if (numPickup > 0) { 
                ChangePitch(); 
                audioSource.PlayOneShot(grabSound); }
        }
    }

    public void GetReleased(SelectExitEventArgs arg)
    {
        if(arg.interactorObject.transform == GameObject.Find("LeftHand Controller").transform
            || arg.interactorObject.transform == GameObject.Find("RightHand Controller").transform)
        {
            //Debug.Log("weeh i go released");
            numPickup --;
            if (numPickup == 0)
            {
                ChangePitch();
                audioSource.PlayOneShot(startSound);
            }
        }
     }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Slime Food A"))
        {
            Destroy(collision.gameObject);
            SetFace(walkFace);

            ChangePitch();
            audioSource.PlayOneShot(eatSound);
            return;
        }
        if (collision.gameObject.CompareTag("Slime Food B"))
        {
            ChangePitch();
            audioSource.PlayOneShot(startSound);
            Destroy(collision.gameObject);
            SetFace(walkFace);
            if (rb.useGravity == false)
            {
                rb.useGravity = true;
            }
            else
            {
                rb.useGravity = false;
            }
            return;
        }
        // if thrown hard, change face
        if (collision.gameObject.CompareTag("Wall") && Mathf.Abs(velocityBeforePhysicsUpdate.magnitude) > 3f)
        {
            Debug.Log("ouch");
            SetFace(damageFace);
            ChangePitch();
            audioSource.PlayOneShot(damageSound);
        }
        if (isAwake)
        {
            if (collision.gameObject.CompareTag("Slime Knife"))
            {
                var newSize = transform.localScale * 0.5f;
                if (newSize.x < 0.15f)
                {
                    return;
                }
                ChangePitch();
                audioSource.PlayOneShot(splitSound);
                var slime = Instantiate(slimePrefab, gameObject.transform.position, Quaternion.identity);
                slime.transform.localScale = transform.localScale * 0.5f;
                //slime.gameObject.GetComponent<SlimeBehaviour>().SetSize(currentSize * 0.5f);
                slime = Instantiate(slimePrefab, gameObject.transform.position, Quaternion.identity);
                slime.transform.localScale = transform.localScale * 0.5f;
                //slime.gameObject.GetComponent<SlimeBehaviour>().SetSize(currentSize * 0.5f);
                Destroy(gameObject);
            }
        }
     }


    void FixedUpdate()
    {

        // Getting impact velocity
        velocityBeforePhysicsUpdate = rb.velocity;
        //Debug.Log(Mathf.Abs(velocityBeforePhysicsUpdate.magnitude));
    }
    // Update is called once per frame
    private void OnEnable()
    {
        awakeTimer = Time.time + time2Awake;
        isAwake = false;
    }
    void Update()
    {


        if (numPickup != 2)
        {
            grabTransformer.allowTwoHandedScaling = false;
        }
        else
        {
            grabTransformer.allowTwoHandedScaling = true;

        }

        if (numPickup != 0)// being picked up
        {
            gameObject.layer = 7; //ignore ray interactor
            return;
        }
        else
        {
            if (gameObject.layer != 3)
            {
                //first time it is not pickup
                awakeTimer = Time.time + time2Awake;
                gameObject.layer = 3; //slime
            }
        }

        if (Time.time > awakeTimer)
        {
            isAwake = true;
        }
        else
        {
            isAwake = false;
            return;
        }

        var dist_s2p = Vector3.Distance(playerGameobject.transform.position, gameObject.transform.position);

        // if player in slime sight, 
        if (dist_s2p < sightDistance)
        {

            // move to player
            if (dist_s2p > interactDistance)
            {
                SetFace(attackFace);
                MoveTo(playerGameobject.transform.position);

            }
            // if player in interact distance, stop moving and face player
            else
            {
                SetFace(jumpFace);
                Vector3 relativePos = playerGameobject.transform.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 2 * Time.deltaTime);
            }
       
        }
        else
        {
            MoveRandom();
        }
    }

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }
    //-------------------------------------- movement function-------------------------
    private void MoveTo(Vector3 v)
    {
        Vector3 vecDirection = playerGameobject.transform.position - transform.position;
        if (Time.time > timer)
        {
            vecDirection = Vector3.Normalize(vecDirection);
            vecDirection = new Vector3(vecDirection.x, 3f, vecDirection.z);
            rb.AddForce(vecDirection * jumpingForce);
            timer = Time.time + Random.Range(1.5f, 2.5f);
            ChangePitch();
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void MoveRandom()
    {

        //when nearly reach destination create new destination
        if (Vector3.Distance(cur_destination, transform.position) < 0.5f)
        {
            cur_destination = GetRandomPos();
        }
        Vector3 vecDirection = cur_destination - transform.position;
        if (Time.time > timer)
        {
            vecDirection = Vector3.Normalize(vecDirection);
            vecDirection = new Vector3(vecDirection.x, 3f, vecDirection.z);
            rb.AddForce(vecDirection * jumpingForce);
            timer = Time.time + Random.Range(3f, 4f);
            ChangePitch();
            audioSource.PlayOneShot(jumpSound);
        }
    }
    //get random position between 2 landmark
    public Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(landmark_1.position.x, landmark_2.position.x),0, Random.Range(landmark_1.position.z, landmark_2.position.z));
    }

    private void ChangePitch()
    {
        size = transform.localScale.x;
        pitch = Mathf.Lerp(minPitch, maxPitch, size);
        audioSource.pitch = pitch;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour
{

    public GameObject boomerang, fakeboomerang;
    public float walkSpeed = 5;
    public float dashpower;
    public float horizontalInput = 0;
    public float verticalInput = 0;

    private GameObject sprite;

    public bool travelling;

    public float boomerangpower;

    public float boomerangRange;

    RaycastHit hit;

    public GameObject tempinstan;

    Vector3 targetpos;

    public Image staminabar;

    bool return_;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        sprite = transform.FindChild("Sprite").gameObject;
        InvokeRepeating("RefillStamina", 0, .5F);
        // boomerang = this.transform.GetChild(1).FindChild("Boomerang").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput) * walkSpeed * Time.deltaTime;
        transform.Translate(direction);

        Vector2 a = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 b = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float angle = (Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 180);
        sprite.transform.rotation = Quaternion.Euler(new Vector3(90, 270, angle));

        if (travelling)
        {
            boomerang_();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && boomerang == null)
        {
            boomerang = Instantiate(tempinstan, fakeboomerang.transform.position, fakeboomerang.transform.rotation) as GameObject;
            fakeboomerang.SetActive(false);
            if (!travelling)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    targetpos = new Vector3(hit.point.x, hit.point.y + .5F, hit.point.z);
                }

                travelling = true;
            }

        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
          {
            Debug.Log("Input");
            if (staminabar.fillAmount > 0)
            {
                staminabar.fillAmount -= .5F;
                rb.AddForce(sprite.transform.right * dashpower);
      
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Input");
            if (staminabar.fillAmount > 0)
            {
                staminabar.fillAmount -= .5F;
                rb.AddForce(-sprite.transform.right * dashpower);

            }
        }
    }

    void boomerang_()
    {

      
        boomerang.transform.Rotate(0,0,0);

        //checks to see if the boomerang has reached its max range or its near the target pos, if so it returns to the player
        if (Vector3.Distance(this.transform.position, boomerang.transform.position) > boomerangRange || Vector3.Distance(boomerang.transform.position, targetpos) < 1)
        {
            return_ = true;
            boomerang.transform.LookAt(targetpos);
        }
        //resets after boomerang has returned
        if (Vector3.Distance(this.transform.position, boomerang.transform.position) < 1)
        {
            Destroy(boomerang);
            fakeboomerang.SetActive(true);
            return_ = false;
            travelling = false;

        }

      if(return_ == true)
        {
            targetpos = transform.position;
        }
        boomerang.transform.LookAt(targetpos);
        boomerang.transform.Translate(transform.forward * boomerangpower * Time.deltaTime);
    }
    void RefillStamina()
    {
        if(staminabar.fillAmount < 1)
        {
            staminabar.fillAmount += 1F * Time.deltaTime;
        }

    }
}
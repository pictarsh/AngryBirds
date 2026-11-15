using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public InputAction mousePosition, mouseAction;
    public float delay = 1f;
    public Transform anchor;
    public float maxDistance = 10f;
    
    private Rigidbody2D rb;
    private SpringJoint2D spring;
    private TrailRenderer trail;
    private bool isReady, isLaunch, isActive = true;
    private GameManager gm;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
        trail = GetComponent<TrailRenderer>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        trail.enabled = false;
    }
    
    private void Update()
    {
        float mousePressed = mouseAction.ReadValue<float>();

        if (isLaunch)
        {
            isLaunch = false;
            
            StartCoroutine(ReleaseDelay());
            
        }

        if (mousePressed > 0 && isActive)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());

            if (Vector3.Distance(anchor.position, position) > maxDistance)
            {
                rb.position = anchor.position + ((Vector3)position - anchor.position).normalized * maxDistance;
            }
            else
            {
                rb.position = position;    
            }
            
            isReady = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            
        }

        if (isReady && mousePressed < 1)
        {
            isReady = false;
            isLaunch = true;
            isActive = false;
        }
        
    }

    private IEnumerator ReleaseDelay()
    {
        yield return new WaitForSeconds(delay);
        spring.enabled = false;
        trail.enabled = true;
        
        yield return new WaitForSeconds(0.5f);
        gm.activateNextBee();
        // isReady = false;
        
    }

    private void OnEnable()
    {
        mouseAction.Enable();
        mousePosition.Enable();
    }

    private void OnDisable()
    {
        mouseAction.Disable();
        mousePosition.Disable();
    }
}

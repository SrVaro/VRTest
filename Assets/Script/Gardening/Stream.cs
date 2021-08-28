using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    [SerializeField] private ParticleSystem splashParticle = null;
    private Coroutine pourRoutine = null;
    private Vector3 targetPosition = Vector3.zero;
    [SerializeField] private GameObject waterCollider;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
    }


    // Update is called once per frame
    void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(BeginPour());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);
            yield return null;
            
        }

    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        // Close Stream Animation not working
        //while(!HasReachedPosition(0, targetPosition))
        //
        //    AnimateToPosition(0, targetPosition);
        //    AnimateToPosition(1, targetPosition);

        //    
        //}
        Destroy(gameObject);

        yield return null;
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        // Esta capa es la unica que ignoraremos, ya que interfiere con el collider para controlar que esta siendo regado
        int layer_mask = LayerMask.GetMask("Water");

        // Se pueden juntar varias Layers 
        //int layer_mask = LayerMask.GetMask("Water","Enemy","Cows");
        
        // El simbolo ~ expecifica que el raycast ignorara la capa "Water" pero no todas las demas
        Physics.Raycast(ray, out hit, 2.0f, ~layer_mask);

        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);


        waterCollider.transform.position = endPoint;


        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
        while(gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);

            splashParticle.gameObject.SetActive(isHitting);

            yield return null;
        }
    }
}

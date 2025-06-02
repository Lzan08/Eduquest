using UnityEngine;


public class FollowPath2D : MonoBehaviour
{
    [SerializeField] bool displayPathGizmos = false;
    [SerializeField] private Pathfinding2D pathfinding;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float followRange = 10f; 
    private Vector3[] path;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool hasReachedNextNode = true;
    private Vector3 lastTargetPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > followRange)
        {
           
            animator.SetBool("IsWalking", false);
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
            return;
        }

       
        animator.SetBool("IsWalking", true);

        if (hasReachedNextNode || path.Length == 0)
        {
            path = pathfinding.FindPath(transform.position, target.position);
            hasReachedNextNode = false;
            lastTargetPosition = target.position;
        }
        else if (lastTargetPosition != target.position)
        {
            path = pathfinding.FindPath(transform.position, target.position);
            hasReachedNextNode = false;
            lastTargetPosition = target.position;
        }

        if (path.Length > 0)
        {
           
            Vector3 direction = (path[0] - transform.position).normalized;

           
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);

           
            Vector3 newPosition = Vector3.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
            transform.position = newPosition;

            if (transform.position == path[0])
            {
                hasReachedNextNode = true;
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (displayPathGizmos)
        {
            if (path != null && path.Length > 0)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(path[i], pathfinding.NodeRadius);
                    if (i == 0)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }

       
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, followRange);
    }
}

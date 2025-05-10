using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float moveDuration = 1f;

    [SerializeField] private Animator anim;

    private void Awake()
    {
        instance = this;

        anim = GetComponent<Animator>();
    }

    public void MoveTo(WayPoint target)
    {
        WayPoint start = FindClosestWayPoint();
        List<Vector3> path = FindPath(start, target);
        if (path != null && path.Count > 0)
        {
            if (anim != null)
                anim.SetBool("isRunning", true);

            Vector3 direction = path[1] - path[0]; 
            FaceDirection(direction);

            transform.DOPath(path.ToArray(), path.Count * moveDuration, PathType.Linear)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (anim != null)
                        anim.SetBool("isRunning", false);
                });
        }
    }
    private void FaceDirection(Vector3 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private WayPoint FindClosestWayPoint()
    {
        WayPoint[] all = GameObject.FindObjectsOfType<WayPoint>();
        WayPoint closest = null;
        float minDist = Mathf.Infinity;

        foreach (WayPoint w in all)
        {
            float dist = Vector3.Distance(transform.position, w.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = w;
            }
        }
        return closest;
    }

    private List<Vector3> FindPath(WayPoint start, WayPoint target)
    {
        Queue<WayPoint> queue = new Queue<WayPoint>();
        Dictionary<WayPoint, WayPoint> cameFrom = new Dictionary<WayPoint, WayPoint>();
        queue.Enqueue(start);
        cameFrom[start] = null;

        while (queue.Count > 0)
        {
            WayPoint current = queue.Dequeue();
            if (current == target) break;

            foreach (WayPoint neighbor in current.neighbors)
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        // reconstruct path
        if (!cameFrom.ContainsKey(target)) return null;

        List<Vector3> path = new List<Vector3>();
        WayPoint node = target;
        while (node != null)
        {
            path.Insert(0, node.transform.position);
            node = cameFrom[node];
        }

        return path;
    }
}

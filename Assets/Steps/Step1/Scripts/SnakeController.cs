using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    public float BonesDistance;
    [Range(0, 4)]
    public float Speed = 4.0f;
    public GameObject BonePrefab;

    private Transform _transform;

    public UnityEvent OnEat;
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    private void Update()
    {
        MoveSnake(_transform.position + _transform.right * Speed * Time.deltaTime);

        float angle = Input.GetAxis("Horizontal") * 4;
        _transform.Rotate(0, angle, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {

        var sqrDistance = BonesDistance * BonesDistance;
        Vector3 previousPosition = _transform.position;

        foreach(var bone in Tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                var temp = bone.position;
                bone.position = previousPosition;
                previousPosition = temp;
            }
            else break;
        }

        _transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            var tail = Instantiate(BonePrefab);
            tail.gameObject.transform.position = new Vector3(0f,-1f,0f);
            Tails.Add(tail.transform);

            if(OnEat != null)
            {
                OnEat.Invoke();
            }
        }
    }
}

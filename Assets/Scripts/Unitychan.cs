using UnityEngine;
using UnityEngine.InputSystem;

public class Unitychan : MonoBehaviour
{
    public Vector2 Velocity;
    public Animator Animator;
    public SpriteRenderer Renderer;

    public bool isWalk = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animator.SetFloat("Velocity", Mathf.Abs(Velocity.x)*(isWalk ? 0.5f : 1));

        if(Velocity.x <= -0.1f)Renderer.flipX = true;
        else if(0.1f <= Velocity.x)Renderer.flipX = false;

        float speed = Velocity.x * 5 * (isWalk ? 0.5f : 1) * Time.deltaTime;
        transform.Translate(speed, 0, 0);
    }

    void OnMove(InputValue value)
    {
        Velocity = value.Get<Vector2>();
    }

    void OnClick(InputValue value)
    {
        isWalk = value.Get<float>() >= 0.5f;
    }
}

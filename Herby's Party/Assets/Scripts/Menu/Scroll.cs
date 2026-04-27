using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 5f;
    public float topBound = 1f;
    public float bottomBound = 1f;
    public Animator anim;
    public bool needAnim;

    private void Start()
    {
        ResetToTop();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < bottomBound) {
            ResetToTop();
        }
    }
    void ResetToTop()
    {
        transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
        if (needAnim) {
            anim.SetTrigger("New Anim");
        }

    }
}

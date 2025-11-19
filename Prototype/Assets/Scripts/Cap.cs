using UnityEngine;

public class Cap : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Throw");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
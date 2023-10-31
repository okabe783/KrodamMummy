using UnityEngine;

public class FireTest : MonoBehaviour
{
    float value = 0;

    private void OnEnable()
    {
        PlayerInput.Instance.SetInput(InputType.Attack, OnAttack);
        PlayerInput.Instance.SetInput(InputType.CancelAttack, OnCancelAttack);
    }

    void OnAttack()
    {
        value += Time.deltaTime;
        Debug.Log(value);
    }

    void OnCancelAttack()
    {
        value = 0;
    }
}

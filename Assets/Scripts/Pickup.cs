using Assets;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Pickup : MonoBehaviour
{
#pragma warning disable 649
    Animator anim;
    public Transform target;
    public Transform hand;
    public float weight = 1.0f;
#pragma warning restore 649

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        weight = anim.GetFloat(strings.IK_PICKUP);

        if (weight > 0.8)
        {
            target.parent = hand;
            target.localPosition = new Vector3(0f, 0.107f, 0.14f);
            target.localRotation = Quaternion.Euler(331f, 0, 0);
        }

        anim.SetIKPosition(AvatarIKGoal.RightHand, target.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
    }
}
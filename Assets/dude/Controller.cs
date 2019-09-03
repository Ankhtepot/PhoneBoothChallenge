using UnityEngine;
using Assets;

public class Controller : MonoBehaviour {

	float speed = 2.0F;
    float rotationSpeed = 100.0F;
    Animator anim;
    float weight = 1f;

    public static GameObject controlledBy;
    public Transform phone;
    public Transform receiver;
    public Transform hand;

	void Start ()
    {
		anim = this.GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (controlledBy != null) return;

		float translation = Input.GetAxis(strings.VERTICAL) * speed;
        float rotation = Input.GetAxis(strings.HORIZONTAL) * rotationSpeed;
        rotation *= Time.deltaTime;
        
        transform.Rotate(0, rotation, 0);

        if(translation != 0)
        {
        	anim.SetBool(strings.IS_WALKING,true);
        	anim.SetFloat(strings.SPEED, translation);
        }
        else
        {
        	anim.SetBool(strings.IS_WALKING, false);
        	anim.SetFloat(strings.SPEED, 0);
        }	
    }

    private void OnAnimatorIK(int layerIndex)
    {
        
        weight = anim.GetFloat(strings.IK_PICKUP);

        if (weight > 0.7 && anim.GetBool(strings.IS_PHONING))
        {
            phone.parent = hand;
            phone.localPosition = new Vector3(-0.024f, 0.089f, 0.023f);
            phone.localRotation = Quaternion.Euler(-67.388f, -9.869f, 81.121f);
        }
        else if(weight > 0.7 && !anim.GetBool(strings.IS_PHONING))
        {
            phone.parent = receiver;
            phone.localPosition = Vector3.zero;
            phone.localRotation = Quaternion.identity;
        }

        anim.SetIKPosition(AvatarIKGoal.RightHand, receiver.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
    }
}

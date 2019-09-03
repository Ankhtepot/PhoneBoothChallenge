using Assets;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class AnswerTelephone : MonoBehaviour
{
#pragma warning disable 649
    public GameObject character;
    public GameObject anchor;
    [SerializeField] float arrivedOffset = 0.65f;
    bool isWalkingTowards = false;
    bool isPhoning = false;
    Animator anim;
#pragma warning restore 649

    void Start()
    {
        anim = character.GetComponent<Animator>();
    }

    void Update()
    {
        if(isWalkingTowards)
        {
            AutoWalkTowards();
        }
    }

    private void FixedUpdate()
    {
        AnimLerp();
    }
    private void OnMouseDown()
    {
        if(!isPhoning)
        {
            anim.SetFloat(strings.SPEED, 1);
            anim.SetBool(strings.IS_WALKING, true);
            isWalkingTowards = true;
            Controller.controlledBy = this.gameObject;
        }
        else
        {
            anim.SetBool(strings.IS_PHONING, false);
            isWalkingTowards = false;
            isPhoning = false;
            Controller.controlledBy = null;
        }
    }
    private void AutoWalkTowards()
    {
        Vector3 targetDir;
        targetDir = new Vector3(anchor.transform.position.x - character.transform.position.x,
            0f,
            anchor.transform.position.z - character.transform.position.z);

        Quaternion rotation = Quaternion.LookRotation(targetDir);
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rotation, 0.1f);

        if(Vector3.Distance(character.transform.position, anchor.transform.position) < arrivedOffset)
        {
            anim.SetBool(strings.IS_PHONING, true);
            anim.SetBool(strings.IS_WALKING, false);

            character.transform.rotation = anchor.transform.rotation;

            isWalkingTowards = false;
            isPhoning = true;
        }
    }

    void AnimLerp()
    {
        if (!isPhoning) return;

        if(Vector3.Distance(character.transform.position, anchor.transform.position) > 0.2f)
        {
            character.transform.rotation = Quaternion.Lerp(character.transform.rotation,
                anchor.transform.rotation,
                Time.deltaTime * 0.5f);

            character.transform.position = Vector3.Lerp(character.transform.position,
                anchor.transform.position,
                Time.deltaTime * 0.5f);
        }
        else
        {
            character.transform.position = anchor.transform.position;
            character.transform.rotation = anchor.transform.rotation;
        }
    }
}
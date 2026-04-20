using UnityEngine;

public class CharacterRestrictionController : MonoBehaviour
{
    private void FixedUpdate()
    {
        var restrictions = CarActions.instance.carRestrictions;

        if (transform.position.x > restrictions.rightRestriction.position.x ||
            transform.position.x < restrictions.leftRestriction.position.x ||
            transform.position.y > restrictions.upRestriction.position.y ||
            transform.position.y < restrictions.downRestriction.position.y)
        {
            transform.position = restrictions.characterRespawnPoint.position;
        }
    }
}

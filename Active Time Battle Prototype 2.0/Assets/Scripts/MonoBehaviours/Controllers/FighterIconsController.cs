using UnityEngine;

namespace MonoBehaviours.Controllers
{
    [RequireComponent(typeof(FighterController))]
    public class FighterIconsController : MonoBehaviour
    {
        public GameObject groundCircle;
        public GameObject headArrow;

        private FighterController _fighter;

        private void Start() => _fighter = GetComponent<FighterController>();

        public void HandleActivePlayerFighter(FighterController fighter)
        {
            if (fighter == _fighter)
                groundCircle.SetActive(true);
        }

        public void HandleTargetFighterHoverEnter(FighterController fighter)
        {
            if (fighter == _fighter)
                headArrow.SetActive(true);
        }

        public void HandleTargetFighterHoverLeave(FighterController fighter)
        {
            if (fighter == _fighter)
                headArrow.SetActive(false);
        }

        public void HandleFighterDie(FighterController fighter)
        {
            if (fighter == _fighter)
                groundCircle.SetActive(false);
        }
    }
}
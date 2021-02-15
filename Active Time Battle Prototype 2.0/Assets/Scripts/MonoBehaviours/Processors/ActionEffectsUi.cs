using System.Globalization;
using MonoBehaviours.Controllers;
using ScriptableObjects.Refs;
using TMPro;
using UnityEngine;

namespace MonoBehaviours.Processors
{
    public class ActionEffectsUi : MonoBehaviour
    {
        public GameObject arcingTextPrefab;
        public ColorRef healingColor;
        public ColorRef damagingColor;

        public void OnFighterDamaged(FighterController fighter, float effect)
        {
            var text = Instantiate(arcingTextPrefab);
            text.transform.position = fighter.transform.position;
            text.GetComponent<TextMeshPro>().color = damagingColor.Value;
            text.GetComponent<TextMeshPro>().text = effect.ToString(CultureInfo.CurrentCulture);
        }

        public void OnFighterHealed(FighterController fighter, float effect)
        {
            var text = Instantiate(arcingTextPrefab);
            text.transform.position = fighter.transform.position;
            text.GetComponent<TextMeshPro>().color = damagingColor.Value;
            text.GetComponent<TextMeshPro>().text = effect.ToString(CultureInfo.CurrentCulture);
        }
    }
}
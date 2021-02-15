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

        public void OnFighterDamaged(FighterController fighter, float effect) =>
            SpawnArcingText(fighter, effect, damagingColor.Value);

        public void OnFighterHealed(FighterController fighter, float effect) =>
            SpawnArcingText(fighter, effect, healingColor.Value);

        private void SpawnArcingText(FighterController fighter, float effect, Color color)
        {
            var text = Instantiate(arcingTextPrefab);
            text.transform.position = fighter.transform.position;
            text.GetComponent<TextMeshPro>().color = color;
            text.GetComponent<TextMeshPro>().text = effect.ToString(CultureInfo.CurrentCulture);
        }
    }
}
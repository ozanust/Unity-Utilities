using System;
using UnityEngine;

namespace Utilities
{
	public class ObjectFader {

		public static float planeFadeinTimer = 0;
		public static ObjectFader instance;

		/// <summary>
		/// Fades in the specified object.
		/// </summary>
		/// <param name="objectGO">Object to fade.</param>
		/// <param name="fadeMultiplier">Change this value to adjust fading speed.</param>
		public static void FadeIn(GameObject objectGO, float fadeMultiplier){
			planeFadeinTimer += Time.deltaTime;
			Color planeColor = objectGO.gameObject.GetComponent<MeshRenderer> ().material.color;
			objectGO.gameObject.GetComponent<MeshRenderer> ().material.color = new Color (planeColor.r, planeColor.g, planeColor.b, Mathf.Lerp (0, 1f, planeFadeinTimer * fadeMultiplier));
		}

		/// <summary>
		/// Fades out the specified object.
		/// </summary>
		/// <param name="objectGO">Object to fade.</param>
		/// <param name="fadeMultiplier">Change this value to adjust fading speed.</param>
		public static void FadeOut(GameObject objectGO, float fadeMultiplier){
			planeFadeinTimer += Time.deltaTime;
			Color planeColor = objectGO.gameObject.GetComponent<MeshRenderer> ().material.color;
			objectGO.gameObject.GetComponent<MeshRenderer> ().material.color = new Color (planeColor.r, planeColor.g, planeColor.b, Mathf.Lerp (1f, 0, planeFadeinTimer * fadeMultiplier));
			//hebe
		}
	}

}
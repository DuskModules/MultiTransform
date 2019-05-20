using System;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules.MultiTransform {

  /// <summary> A referencable float value for size </summary>
  [Serializable]
  public class MultiScaleValue {
		/// <summary> How much scale to use </summary>
		public float size;
  }

	/// <summary> Script handling multiple scale values, allowing them to be independantly adjusted without conflict </summary>
	public class MultiScale {

		/// <summary> All sizes to multiply by </summary>
		private List<MultiScaleValue> sizes;

		/// <summary> Current scale value </summary>
		public float scale { get; private set; }
		/// <summary> Target transform to influence </summary>
		public Transform target { get; private set; }
		/// <summary> Original scale </summary>
		public Vector3 original { get; private set; }

		/// <summary> Creates and sets the target </summary>
		public MultiScale(Transform target) {
			this.target = target;
			original = target.localScale;
			scale = 1;
			Update();
		}

		/// <summary> Adds scale size to use </summary>
		/// <param name="scale"> Scale to apply </param>
		/// <returns> The ScaleSize reference </returns>
		public MultiScaleValue AddNewScale(float scale = 1) {
			if (sizes == null) sizes = new List<MultiScaleValue>();
			MultiScaleValue size = new MultiScaleValue { size = scale };
			sizes.Add(size);
			Update();
			return size;
		}

		/// <summary> Removes the given scale size </summary>
		/// <param name="size"> The size to remove </param>
		public void RemoveScale(MultiScaleValue size) {
			if (sizes == null) return;
			if (sizes.Contains(size)) {
				sizes.Remove(size);
				Update();
			}
		}

		/// <summary> Updates the scale value </summary>
		public void Update() {
			if (sizes == null) return;
			float newScale = 1;
			for (int i = 0; i < sizes.Count; i++) {
				newScale *= sizes[i].size;
			}

			if (newScale != scale) {
				scale = newScale;
				target.localScale = original * scale;
			}
		}
	}
}
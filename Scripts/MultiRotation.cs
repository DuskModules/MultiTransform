using System;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules.MultiTransform {
	
	/// <summary> A referencable quaternion value for rotation </summary>
	[Serializable]
	public class MultiRotationValue {
		/// <summary> How much rotation to use </summary>
		public Quaternion rotation;
	}

	/// <summary> Script that handles multiple rotation values, allowing them to be indepenently adjusted. </summary>
	public class MultiRotation {

		/// <summary> All rotations to multiply by </summary>
		private List<MultiRotationValue> quaternions;

		/// <summary> Current rotation value </summary>
		public Quaternion rotation { get; private set; }
		/// <summary> Target transform to influence </summary>
		public Transform target { get; private set; }
		/// <summary> Original rotation </summary>
		public Quaternion original { get; private set; }

		/// <summary> Creates and sets the target </summary>
		public MultiRotation(Transform target) {
			this.target = target;
			original = target.localRotation;
			rotation = Quaternion.identity;
			Update();
		}
		
		/// <summary> Adds quaternion rotation to use </summary>
		/// <returns> The RotationQuaternion reference </returns>
		public MultiRotationValue AddNewRotation() {
			return AddNewRotation(Quaternion.identity);
		}

		/// <summary> Adds quaternion rotation to use </summary>
		/// <param name="rotation"> Rotation to apply </param>
		/// <returns> The RotationQuaternion reference </returns>
		public MultiRotationValue AddNewRotation(Quaternion rotation) {
			if (quaternions == null) quaternions = new List<MultiRotationValue>();
			MultiRotationValue quaternion = new MultiRotationValue { rotation = rotation };
			quaternions.Add(quaternion);
			return quaternion;
		}
		/// <summary> Removes the given quaternion rotation </summary>
		/// <param name="rotation"> The quaternion to remove </param>
		public void RemoveRotation(MultiRotationValue rotation) {
			if (quaternions.Contains(rotation))
				quaternions.Remove(rotation);
		}

		/// <summary> Updates the rotation value </summary>
		public void Update() {
			if (quaternions == null) return;
			Quaternion newRotation = Quaternion.identity;
			for (int i = 0; i < quaternions.Count; i++) {
				newRotation *= quaternions[i].rotation;
			}
			if (newRotation != rotation) {
				rotation = newRotation;
				target.localRotation = original * rotation;
			}
		}
	}
}
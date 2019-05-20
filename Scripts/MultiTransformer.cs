using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules.MultiTransform {

	/// <summary> Script that wraps a MultiPosition, MultiRotation and MultiScale into a MonoBehaviour. </summary>
	public class MultiTransformer : MonoBehaviour {

    /// <summary> Multi position value </summary>
    public MultiPosition multiPosition { get; private set; }
    /// <summary> Multi rotation value </summary>
    public MultiRotation multiRotation { get; private set; }
    /// <summary> Multi scale value </summary>
    public MultiScale multiScale { get; private set; }
    
    /// <summary> Adds position to use </summary>
    /// <returns> The MultiPositionValue reference </returns>
    public MultiPositionValue AddNewPosition() {
      if (multiPosition == null) multiPosition = new MultiPosition(transform);
      return multiPosition.AddNewPosition();
    }
    /// <summary> Adds position to use </summary>
    /// <param name="rotation"> position to apply </param>
    /// <returns> The MultiPositionValue reference </returns>
    public MultiPositionValue AddNewPosition(Vector3 position) {
      if (multiPosition == null) multiPosition = new MultiPosition(transform);
      return multiPosition.AddNewPosition(position);
    }
    /// <summary> Removes the given position </summary>
    /// <param name="position"> The position to remove </param>
    public void RemovePosition(MultiPositionValue position) {
      if (multiPosition == null) multiPosition = new MultiPosition(transform);
      multiPosition.RemovePosition(position);
    }

    /// <summary> Adds quaternion rotation to use </summary>
    /// <returns> The RotationQuaternion reference </returns>
    public MultiRotationValue AddNewRotation() {
      if (multiRotation == null) multiRotation = new MultiRotation(transform);
      return multiRotation.AddNewRotation();
    }
    /// <summary> Adds quaternion rotation to use </summary>
    /// <param name="rotation"> Rotation to apply </param>
    /// <returns> The RotationQuaternion reference </returns>
    public MultiRotationValue AddNewRotation(Quaternion quaternion) {
      if (multiRotation == null) multiRotation = new MultiRotation(transform);
      return multiRotation.AddNewRotation(quaternion);
    }
    /// <summary> Removes the given quaternion rotation </summary>
    /// <param name="quaternion"> The quaternion to remove </param>
    public void RemoveRotation(MultiRotationValue quaternion) {
      if (multiRotation == null) multiRotation = new MultiRotation(transform);
      multiRotation.RemoveRotation(quaternion);
    }

    /// <summary> Adds scale size to use </summary>
    /// <param name="scale"> Scale to apply </param>
    /// <returns> The ScaleSize reference </returns>
    public MultiScaleValue AddNewScale(float scale = 1) {
      if (multiScale == null) multiScale = new MultiScale(transform);
      return multiScale.AddNewScale(scale);
		}
		/// <summary> Removes the given scale size </summary>
		/// <param name="size"> The size to remove </param>
		public void RemoveScale(MultiScaleValue size) {
      if (multiScale == null) multiScale = new MultiScale(transform);
      multiScale.RemoveScale(size);
		}

		// On update.
		private void LateUpdate() {
      if (multiPosition != null) multiPosition.Update();
      if (multiRotation != null) multiRotation.Update();
      if (multiScale != null) multiScale.Update();
		}
	}
}
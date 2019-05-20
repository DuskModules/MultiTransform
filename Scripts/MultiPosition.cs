using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules.MultiTransform {

  /// <summary> A referencable float value for position </summary>
  [Serializable]
  public class MultiPositionValue {
    /// <summary> Position value to use </summary>
    public Vector3 position;
  }

  /// <summary> Script handling multiple position values, allowing them to be independantly adjusted without conflict </summary>
  public class MultiPosition {

    /// <summary> All positions to add by </summary>
    private List<MultiPositionValue> positions;

    /// <summary> Current position value </summary>
    public Vector3 position { get; private set; }
    /// <summary> Target transform to influence </summary>
    public Transform target { get; private set; }
    /// <summary> Original position </summary>
    public Vector3 original { get; private set; }

    /// <summary> Creates and sets the target </summary>
    public MultiPosition(Transform target) {
      this.target = target;
      original = target.localPosition;
      position = Vector3.zero;
      Update();
    }

    /// <summary> Adds position to use </summary>
    /// <returns> The MultiPositionValue reference </returns>
    public MultiPositionValue AddNewPosition() {
      return AddNewPosition(Vector3.zero);
    }

    /// <summary> Adds position to use </summary>
    /// <param name="value"> Vector to apply </param>
    /// <returns> The MultiPositionValue reference </returns>
    public MultiPositionValue AddNewPosition(Vector3 value) {
      if (positions == null) positions = new List<MultiPositionValue>();
      MultiPositionValue pos = new MultiPositionValue { position = value };
      positions.Add(pos);
      Update();
      return pos;
    }

    /// <summary> Removes the given position </summary>
    /// <param name="pos"> The vector to remove </param>
    public void RemovePosition(MultiPositionValue pos) {
      if (positions == null) return;
      if (positions.Contains(pos)) {
        positions.Remove(pos);
        Update();
      }
    }

    /// <summary> Updates the position value </summary>
    public void Update() {
      if (positions == null) return;
      Vector3 newPosition = Vector3.zero;
      for (int i = 0; i < positions.Count; i++) {
        newPosition += positions[i].position;
      }

      if (newPosition != position) {
        position = newPosition;
        target.localPosition = original + position;
      }
    }
  }
}
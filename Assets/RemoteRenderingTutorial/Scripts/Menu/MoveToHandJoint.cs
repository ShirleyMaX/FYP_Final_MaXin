﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

/// <summary>
/// Moves object to palm over time.
/// </summary>
public class MoveToHandJoint : MonoBehaviour
{
    Vector3 _goalPosition = Vector3.zero;

    #region Serialized Fields
    [SerializeField]
    [Tooltip("The hand joint to track.")]
    private TrackedHandJoint joint;

    /// <summary>
    /// The hand joint to track.
    /// </summary>
    public TrackedHandJoint Joint
    {
        get => joint;
        set => joint = value;
    }

    [SerializeField]
    [Tooltip("The offset from the hand joint, where this game object will be placed.")]
    private Vector3 jointOffset = Vector3.up * 0.1f;

    /// <summary>
    /// The offset from the hand joint, where this game object will be placed.
    /// </summary>
    public Vector3 JointOffset
    {
        get => jointOffset;
        set => jointOffset = value;
    }

    [SerializeField]
    [Tooltip("This helps determine when the palm is facing upwards.")]
    private PalmAngleEvents palmEvents = null;

    /// <summary>
    /// This helps determine when the palm is facing upwards.
    /// </summary>
    public PalmAngleEvents PalmEvents
    {
        get => palmEvents;
    }

    [SerializeField]
    [Tooltip("The line going from hand to menu.")]
    private MenuToHandTether tether;

    /// <summary>
    /// The line going from hand to menu.
    /// </summary>
    public MenuToHandTether Tether
    {
        get => tether;
        set => tether = value;
    }

    [SerializeField]
    [Tooltip("Should this object be oriented to the given hand joint.")]
    private bool orientToJoint = false;

    /// <summary>
    /// Should this object be oriented to the given hand joint.
    /// </summary>
    public bool OrientToJoint
    {
        get => orientToJoint;
        set => orientToJoint = value;
    }

    [SerializeField]
    [Tooltip("The amount of smooth to apply to position updates.")]
    private float lerpAmount = 0.2f;

    /// <summary>
    /// The amount of smooth to apply to position updates.
    /// </summary>
    public float LerpAmount
    {
        get => lerpAmount;
        set => lerpAmount = value;
    }
    #endregion Serialized Fields

    #region MonoBehavior Functions
    private void Update()
    {
        palmEvents = gameObject.EnsureComponent<PalmAngleEvents>();
        UpdatePosition(true);
    }
    #endregion MonoBehavior Functions

    #region Public Functions
    public void MoveToHandJointNow()
    {
        UpdatePosition(false);
    }
    #endregion Public Functions

    #region Private Functions
    private void UpdatePosition(bool smooth)
    {
        Handedness facingUpHand;
        if (palmEvents.TryGetPalm(out facingUpHand))
        {
            Vector3 palmToMidTip = GetJointPos(TrackedHandJoint.MiddleTip, facingUpHand) - GetJointPos(TrackedHandJoint.Palm, facingUpHand);
            _goalPosition = GetJointPos(joint, facingUpHand) + (Vector3.up * jointOffset.y) + (palmToMidTip.normalized * jointOffset.z);
            gameObject.transform.position = smooth ? Vector3.Lerp(gameObject.transform.position, _goalPosition, lerpAmount) : _goalPosition;
            tether.HandPosition = GetJointPos(joint, facingUpHand);
            if (orientToJoint)
            {
                Vector3 rotation;
                if (palmEvents.TryGetPalmRotation(facingUpHand, out rotation))
                {
                    gameObject.transform.eulerAngles = rotation;
                }
            }
        }
    }

    private Vector3 GetJointPos(TrackedHandJoint joint, Handedness hand)
    {
        MixedRealityPose handJoint;
        if (HandJointUtils.TryGetJointPose(joint, hand, out handJoint))
        {
            return handJoint.Position;
        }
        else
        {
            return gameObject.transform.position;
        }
    }
    #endregion Private Functions
}

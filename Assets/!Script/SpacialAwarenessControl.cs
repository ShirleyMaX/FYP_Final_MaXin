using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacialAwarenessControl : MonoBehaviour
{
    public void StopObserving()
    {
        // Get the first Mesh Observer available, generally we have only one registered
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();

        // Suspends observation of spatial mesh data
        observer.Suspend();
        // Set to not visible
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;

        Debug.Log("Suspended");
    }
}

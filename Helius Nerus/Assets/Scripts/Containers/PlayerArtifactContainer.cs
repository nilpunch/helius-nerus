using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerArtifactContainer : GenericContainer<PlayerArtifactIconDesc, ArtifactType, IPlayerArtifact>
{
    private List<Type> _artifactTypes = null;
    private List<IPlayerArtifact> _artifacts = null;

    public override PlayerArtifactIconDesc GetValueByKey(ArtifactType key)
    {
        for (int i = 0; i < _allModifiers.Count; ++i)
        {
            if (_allModifiers[i].Modifier == key)
                return _allModifiers[i];
        }
        return null;
    }

    protected override IPlayerArtifact GetArtifact(ArtifactType key)
    {
        return _artifacts[(int)key].Clone();
    }

    protected override void PreCookTypes()
    {
        _artifactTypes.Clear();
        _artifacts.Clear();
        foreach (ArtifactType type in (ArtifactType[])Enum.GetValues(typeof(ArtifactType)))
        {
            Type ctype = Type.GetType(type.ToString());
#if UNITY_EDITOR
            if (ctype == null)
            {
                Debug.LogError("ArtifactsCollection handle wrong artifact name: " + type.ToString());
                Debug.Break();
            }
#endif
            _artifactTypes.Add(ctype);
            _artifacts.Add((IPlayerArtifact)Activator.CreateInstance(ctype));
        }
    }
}
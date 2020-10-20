using System.Collections.Generic;
using UnityEngine;
using System;

public enum ArtifactType
{
	InvincibilityArtifact,
}

public class ArtifactsCollection
{
	public static ArtifactsCollection Instance { get; private set; } = null;

    public static IPlayerArtifact GetRandomPlayerArtifact()
    {
        int rand = UnityEngine.Random.Range(0, Instance._artifacts.Count);
        return Instance._artifacts[rand].Clone();
    }
	public static Type GetTypeByEnum(ArtifactType type)
	{
		return Instance._artifactsTypes[(int)type];
	}
	public static IPlayerArtifact GetArtifactByEnum(ArtifactType type)
	{
		return Instance._artifacts[(int)type].Clone();
	}
	public IPlayerArtifact this[ArtifactType type]
	{
		get => _artifacts[(int)type].Clone();
	}

	private List<Type> _artifactsTypes = new List<Type>();
	private List<IPlayerArtifact> _artifacts = new List<IPlayerArtifact>();


	public void Init()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			return;
		}

        PreCookTypes();
	}

	private void PreCookTypes()
	{
		_artifactsTypes.Clear();
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
			_artifactsTypes.Add(ctype);
			_artifacts.Add((IPlayerArtifact)Activator.CreateInstance(ctype));
		}
	}
}
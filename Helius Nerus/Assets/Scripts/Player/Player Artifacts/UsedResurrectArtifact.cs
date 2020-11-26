public class UsedResurrectArtifact : PlayerArtifact
{
    public override string MyEnumName => "UsedResurrectArtifact";

    public override ArtifactType MyEnum => ArtifactType.UsedResurrectArtifact;

    public override PlayerArtifact Clone()
    {
        return (UsedResurrectArtifact)this.MemberwiseClone();
    }
}

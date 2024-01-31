namespace FileOps.Core.Features.Parse;

[Flags]
public enum PathRules
{
    Unspecified = 0,
    UseForSource,
    UseForTarget,
    UseForBoth = UseForSource | UseForTarget
}

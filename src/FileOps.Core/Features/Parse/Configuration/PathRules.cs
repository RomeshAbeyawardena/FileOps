namespace FileOps.Core;

[Flags]
public enum PathRules
{
    Unspecified = 0,
    UseForSource,
    UseForTarget,
    UseForBoth = UseForSource | UseForTarget
}

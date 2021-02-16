using System;
using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
[GitHubActions("Package",
    GitHubActionsImage.WindowsLatest,
    AutoGenerate = true,
    OnPushBranches = new[] {"main", "develop"},
    OnPullRequestBranches = new[] {"develop"},
    ImportGitHubTokenAs = "GITHUB_TOKEN",
    ImportSecrets = new[] {"NUGET_API_KEY"},
    InvokedTargets = new[] {nameof(Clean), nameof(GithubPush), nameof(NugetPush)})]
class Build : NukeBuild
{
    /// Support plugins are available for:
    /// - JetBrains ReSharper        https://nuke.build/resharper
    /// - JetBrains Rider            https://nuke.build/rider
    /// - Microsoft VisualStudio     https://nuke.build/visualstudio
    /// - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Clean,
        x => x.GithubPush,
        x => x.NugetPush);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [GitVersion(Framework = "netcoreapp3.1")] readonly GitVersion GitVersion;

    [Solution] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath TestsDirectory => RootDirectory / "tests";

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore()
                .SetProperty("ContinuousIntegrationBuild", IsCiBuild()));
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetOutputDirectory(ArtifactsDirectory)
                .EnableNoBuild()
                .CombineWith(GetPackageProjects(), (s, p) => s
                    .SetProject(p)));
        });

    Target GithubPush => _ => _
        .DependsOn(Pack)
        .OnlyWhenDynamic(() => IsCiBuild())
        .Executes(() =>
        {
            const string githubPackageSource = @"https://nuget.pkg.github.com/G18SSY/index.json";
            const string githubTokenVariable = "GITHUB_TOKEN";

            PushNuGetPackages(githubPackageSource, githubTokenVariable);
        });

    Target NugetPush => _ => _
        .DependsOn(Pack)
        .OnlyWhenDynamic(() => IsCiBuild() && IsStableOrRelease())
        .Executes(() =>
        {
            const string nugetOrgPackageSource = @"https://api.nuget.org/v3/index.json";
            const string nugetOrgApiKeyVariable = "NUGET_API_KEY";

            PushNuGetPackages(nugetOrgPackageSource, nugetOrgApiKeyVariable);
        });

    void PushNuGetPackages(string githubPackageSource, string githubTokenVariable)
    {
        IReadOnlyCollection<AbsolutePath> packages = ArtifactsDirectory.GlobFiles("*.nupkg");

        DotNetNuGetPush(s => s
            .SetSource(githubPackageSource)
            .SetApiKey(Environment.GetEnvironmentVariable(githubTokenVariable))
            .CombineWith(packages, (s, p) => s
                .SetTargetPath(p)));
    }

    static bool IsCiBuild()
        => GitHubActions.Instance != null;

    bool IsStableOrRelease()
        => GitVersion.PreReleaseLabel.Equals("rc", StringComparison.OrdinalIgnoreCase) ||
           string.IsNullOrEmpty(GitVersion.PreReleaseLabel);

    IEnumerable<AbsolutePath> GetPackageProjects()
        => SourceDirectory.GlobFiles("**/**.csproj");
}
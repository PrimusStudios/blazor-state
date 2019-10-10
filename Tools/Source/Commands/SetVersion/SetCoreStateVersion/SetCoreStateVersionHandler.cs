namespace Tools.Commands.SetVersion
{
  using System.IO;
  using System.Text.RegularExpressions;
  using System.Threading;
  using System.Threading.Tasks;
  using MediatR;
  using Tools.Services;

  internal class SetCoreStateVersionHandler : IRequestHandler<SetCoreStateVersionRequest>
  {
    private string BasePath { get; set; }
    private GitService GitService { get; }
    private YmlTools YmlTools { get; }

    public SetCoreStateVersionHandler(GitService aGitService, YmlTools aYmlTools)
    {
      GitService = aGitService;
      YmlTools = aYmlTools;
    }

    public Task<Unit> Handle(SetCoreStateVersionRequest aSetCoreStateVersionRequest, CancellationToken aCancellationToken)
    {
      DirectoryInfo gitDirectory = GitService.GitRootDirectoryInfo();
      BasePath = gitDirectory.FullName;
      UpdateAzurePipeLinesYml(aSetCoreStateVersionRequest, @"\Build\blazor-state.yml");
      UpdateVersionPrefix(aSetCoreStateVersionRequest);
      UpdatePackageVersion(aSetCoreStateVersionRequest);
      UpdatePackageReference(aSetCoreStateVersionRequest);

      return Unit.Task;
    }

    private void UpdateAzurePipeLinesYml(SetCoreStateVersionRequest aSetCoreStateVersionRequest, string aRelativePath)
    {
      YmlTools.UpdateAzurePipeLinesYml(
        aSetCoreStateVersionRequest.Major,
        aSetCoreStateVersionRequest.Minor,
        aSetCoreStateVersionRequest.Patch,
        aRelativePath
        );
    }

    private void UpdatePackageReference(SetCoreStateVersionRequest aSetCoreStateVersionRequest)
    {
      string filename = $@"{BasePath}\source\TimeWarp.AspNetCore.Blazor.Templates\content\TimeWarp.BlazorHosted-CSharp\Source\BlazorHosted-CSharp.Client\BlazorHosted-CSharp.Client.csproj";
      string regex = @"<PackageReference Include=""Blazor-State"" Version="".+"" />";
      string replacement = $@"<PackageReference Include=""Blazor-State"" Version=""{ aSetCoreStateVersionRequest.Major}.{aSetCoreStateVersionRequest.Minor}.{aSetCoreStateVersionRequest.Patch}"" />";
      string text = File.ReadAllText(filename);
      text = Regex.Replace(text, regex, replacement);
      File.WriteAllText(filename, text, System.Text.Encoding.UTF8);
    }

    private void UpdatePackageVersion(SetCoreStateVersionRequest aSetCoreStateVersionRequest)
    {
      string filename = $@"{BasePath}\source\CoreState\CoreState.csproj";
      string regex = @"<PackageVersion>.+</PackageVersion>";
      string replacement = $@"<PackageVersion>{aSetCoreStateVersionRequest.Major}.{aSetCoreStateVersionRequest.Minor}.{aSetCoreStateVersionRequest.Patch}</PackageVersion>";
      string text = File.ReadAllText(filename);
      text = Regex.Replace(text, regex, replacement);
      File.WriteAllText(filename, text, System.Text.Encoding.UTF8);
    }

    private void UpdateVersionPrefix(SetCoreStateVersionRequest aSetCoreStateVersionRequest)
    {
      string filename = $@"{BasePath}\source\Directory.Build.props";
      string regex = @"<VersionPrefix>.+</VersionPrefix>";
      string replacement = $@"<VersionPrefix>{aSetCoreStateVersionRequest.Major}.{aSetCoreStateVersionRequest.Minor}.{aSetCoreStateVersionRequest.Patch}</VersionPrefix>";
      string text = File.ReadAllText(filename);
      text = Regex.Replace(text, regex, replacement);
      File.WriteAllText(filename, text, System.Text.Encoding.UTF8);
    }
  }
}
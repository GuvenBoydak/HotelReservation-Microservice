using System.Diagnostics;

namespace UnitTest.Helpers;

public class DockerHelper
{
    public static void StopContainers()
    {
        var process = Process.Start("docker-compose", "down");
        process?.WaitForExit();
    }

    public static void StartContainers()
    {
        StopContainers();
        Process.Start("docker-compose", "up -d");
    }
}
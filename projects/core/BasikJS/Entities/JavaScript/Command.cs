using CliWrap;

namespace BasikJS.Entities.JavaScript
{
    public class CommandResult
    {
        public required byte[] Stdout { get; set; }
        public required byte[] Stderr { get; set; }

        public async Task<bool> SaveOut(string path)
        {
            await File.WriteAllBytesAsync(path, Stdout);
            return true;
        }

        public async Task<bool> SaveErr(string path)
        {
            await File.WriteAllBytesAsync(path, Stderr);
            return true;
        }
    }

    public class Command(string workingDirectory = "./")
    {
        public List<object> Records { get; } = new();
        private readonly string _workingDirectory = workingDirectory;

        public async Task<CommandResult> Run(string command, string[]? arguments = null)
        {
            using var stdOutBuffer = new MemoryStream();
            using var stdErrBuffer = new MemoryStream();

            var result = await Cli.Wrap(command)
                .WithArguments(arguments ?? [])
                .WithWorkingDirectory(_workingDirectory)
                .WithStandardOutputPipe(PipeTarget.ToStream(stdOutBuffer))
                .WithStandardErrorPipe(PipeTarget.ToStream(stdErrBuffer))
                .WithValidation(CommandResultValidation.None)
                .ExecuteAsync();

            var commandResult = new CommandResult
            {
                Stderr = stdErrBuffer.ToArray(),
                Stdout = stdOutBuffer.ToArray()
            };

            return commandResult;
        }

        public static Command Create(string workingDirectory = "./")
        {
            return new Command(workingDirectory);
        }
    }
}

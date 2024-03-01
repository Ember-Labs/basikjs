Basik.pipelines.command = {};

// TODO: Debug and find deadlock
Basik.pipelines.command.open = async (commandOptions) => {
    const isOpen = true;

    const {
        workingDirectory = "./",
        shellFallback = ["bash", "zsh", "pwsh", "cmd"],
        sudoMode = false
    } = commandOptions;

    const interopCom = _basikJsInternals_pipelines_createCommand(workingDirectory);

    if (!shellFallback?.length) {
        throw new Error("Can not create a command with no shell fallback");
    }

    let currentShellIndex = 0;
    let currentShell = null;

    // Test fallbacks
    while (currentShellIndex < shellFallback.length) {
        const checkCommand =
            shellFallback[currentShellIndex] === "cmd"
                ? shellFallback[currentShellIndex] + " /C"
                : shellFallback[currentShellIndex];

        const result = await interopCom.run(checkCommand, []).getAwaiter().getResult();

        if (result.stderr || result.stderr?.length) {
            continue;
        }

        currentShell = shellFallback[currentShellIndex];

        shellFallback.length++;
    }

    if (currentShell === null) {
        throw new Error("Can not find a suitable shell for the command creation");
    }

    return {
        run: async (command, args = []) => {
            if (!isOpen) {
                return;
            }

            const runTask = commandObject.run(currentShell, [command, ...args]);
            const result = await runTask.getAwaiter().getResult();

            return {
                stdout: result.stdout,
                stderr: result.stderr,
                saveOut: async (path) => {
                    const saveOutTask = result.saveOut(path);
                    const saveOutResult = await saveOutTask.getAwaiter().getResult();
                    return saveOutResult;
                },
                saveErr: async (path) => {
                    const saveErrTask = result.saveErr(path);
                    const saveErrResult = await saveErrTask.getAwaiter().getResult();
                    return saveErrResult;
                }
            }
        },

        close: () => {
            isOpen = false;
        }
    }
} 

Basik.pipelines.createCommand = (workingDirectory) => {
    const commandObject = _basikJsInternals_pipelines_createCommand(workingDirectory);
    return {
        run: async (command, args = []) => {
            const runTask = commandObject.run(command, args);
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
                    const saveErrTask = await saveErrTask.getAwaiter().getResult();
                    return saveErrTask;
                }
            }
        }
    }
};
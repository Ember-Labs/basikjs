Basik.pipelines.createCommand = (workingDirectory) => {
    const commandObject = _basikJsInternals_pipelines_createCommand(workingDirectory);
    return {
        run: async (command, args = []) => {
            const result = await commandObject.run(command, args);
            return {
                stdout: result.stdout,
                stderr: result.stderr,
                saveOut: async (path) => await result.saveOut(path),
                saveErr: async (path) => await result.saveErr(path)
            }
        }
    }
};